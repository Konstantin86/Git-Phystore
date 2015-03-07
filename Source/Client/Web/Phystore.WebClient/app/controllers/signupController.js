/// <reference path="~/scripts/angular.min.js"/>
/// <reference path="~/app/app.js"/>
/// <reference path="~/app/const/msgConst.js"/>
/// <reference path="~/app/services/authService.js"/>
/// <reference path="~/app/services/statusService.js"/>
/// <reference path="~/app/utils/system/system-ns.js" />
/// <reference path="~/app/utils/system/system-string.js" />
"use strict";

app.controller("signupController", function ($scope, $location, $timeout, msgConst, authService, statusService) {

    statusService.clear();

    $scope.success = false;
    $scope.submitted = false;

    $scope.formData = {
        userName: "",
        email: "",
        password: "",
        roleName: "user",
        confirmPassword: ""
    };

    $scope.signUp = function () {
        $scope.submitted = true;
        authService.logout();
        authService.account.save($scope.formData, onSignupSucceed, onSignupFailed);

        function onSignupSucceed() {
            $scope.success = true;
            statusService.success(system.string.format(msgConst.SIGNUP_SUCCESS_FORMAT, $scope.formData.userName, $scope.formData.email));
        }

        function onSignupFailed(response) {
            var error = "";

            if (response.data && response.data.modelState) {
                var errors = [];
                for (var key in response.data.modelState) {
                    if (response.data.modelState.hasOwnProperty(key)) {
                        for (var i = 0; i < response.data.modelState[key].length; i++) {
                            errors.push(response.data.modelState[key][i]);
                        }
                    }
                }

                error = system.string.format(msgConst.SIGNUP_FAIL_FORMAT, errors.join(" "));
            }

            statusService.error(error);
        }
    };
});