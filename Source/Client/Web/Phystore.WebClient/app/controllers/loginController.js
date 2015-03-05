/// <reference path="~/scripts/angular.min.js"/>
/// <reference path="~/app/app.js"/>
/// <reference path="~/app/const/appConst.js"/>
/// <reference path="~/app/const/msgConst.js"/>
/// <reference path="~/app/services/authService.js"/>
/// <reference path="~/app/services/statusService.js"/>
/// <reference path="~/app/utils/system/system-ns.js" />
/// <reference path="~/app/utils/system/system-string.js" />
"use strict";

app.controller("loginController", function ($scope, $location, authService, statusService, appConst, msgConst) {

    statusService.clear();

    $scope.formData = { userName: "", password: "" };

    $scope.login = function () {
        authService.login($scope.formData).then(function () {
            $location.path('/workouts');
        }, function (err) {
            var error = err ? err.error_description : "";
            statusService.error(error);
        });
    };

    $scope.forgotPasswordModal =
    {
        title: "Password recovery",
        editable: true,
        input: "",
        content: msgConst.LOGIN_PWD_RECOVERY_INSTRUCTIONS,
        yes: function () {
            var modal = this;
            authService.sendPassword(modal.input).then(function () {
                modal.$hide();
                statusService.success(system.string.format(msgConst.LOGIN_PWD_RECOVERY_LINK_SENT_FORMAT, modal.input));
            }, function (err) {
                modal.$hide();
                statusService.error(err);
            });

        }
    };

    $scope.authExternalProvider = function (provider) {
        var redirectUri = location.protocol + "//" + location.host + "/authcomplete.html";
        var serviceBaseUri = appConst.serviceBase;
        var externalProviderUrl = serviceBaseUri + "api/account/externalLogin?provider=" + provider + "&response_type=token&client_id=" + "Keepfit" + "&redirect_uri=" + redirectUri;

        window.$windowScope = $scope;
        window.open(externalProviderUrl, "Authenticate Account", "location=0,status=0,width=600,height=750");
    };

    $scope.authCompletedCB = function (fragment) {
        $scope.$apply(function () {
            if (fragment.haslocalaccount === "False") {
                authService.logOut();

                authService.externalAuthData = {
                    provider: fragment.provider,
                    userName: fragment.external_user_name,
                    email: fragment.email,
                    externalAccessToken: fragment.external_access_token
                };

                $location.path("/associate");
            }
            else {
                var externalData = { provider: fragment.provider, externalAccessToken: fragment.external_access_token };
                authService.obtainAccessToken(externalData).then(function () {
                    $location.path("/workouts");
                }, function (err) {
                    statusService.error(err.error_description);
                });
            }
        });
    }
});