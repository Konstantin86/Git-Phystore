app.controller('loginController', function ($scope, $location, authService, statusService) {

    statusService.clear();

    $scope.formData = {
        userName: "",
        password: ""
    };

    $scope.login = function () {
        authService.login($scope.formData).then(function (response) {
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
        content: "Provide your e-mail address below and click 'Yes'",
        yes: function () {
            var modal = this;
            authService.sendPassword(modal.input).then(function (response) {
                modal.$hide();
                statusService.success(system.string.format("Password recovery link has been just sent to {0}.", modal.input));
            }, function (err) {
                modal.$hide();
             statusService.error(err);
         });

        }
    };

    $scope.authExternalProvider = function (provider) {

        var redirectUri = location.protocol + '//' + location.host + '/authcomplete.html';

        var serviceBaseUri = appConfig.getInstance().getServiceUri();

        var externalProviderUrl = serviceBaseUri + "api/account/externalLogin?provider=" + provider
                                                                    + "&response_type=token&client_id=" + 'Keepfit'
                                                                    + "&redirect_uri=" + redirectUri;
        window.$windowScope = $scope;

        var oauthWindow = window.open(externalProviderUrl, "Authenticate Account", "location=0,status=0,width=600,height=750");
    };

    $scope.authCompletedCB = function (fragment) {

        $scope.$apply(function () {

            if (fragment.haslocalaccount == 'False') {

                authService.logOut();

                authService.externalAuthData = {
                    provider: fragment.provider,
                    userName: fragment.external_user_name,
                    email: fragment.email,
                    externalAccessToken: fragment.external_access_token
                };

                $location.path('/associate');

            }
            else {
                //Obtain access token and redirect to orders
                var externalData = { provider: fragment.provider, externalAccessToken: fragment.external_access_token };
                authService.obtainAccessToken(externalData).then(function (response) {
                    $location.path('/workouts');
                }, function (err) {
                    statusService.error(err.error_description);
                });
            }

        });
    }
});