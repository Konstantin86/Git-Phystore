﻿/// <reference path="~/scripts/angular.min.js"/>
/// <reference path="~/app/app.js"/>
/// <reference path="~/app/const/msgConst.js"/>
/// <reference path="~/app/services/authService.js"/>
/// <reference path="~/app/services/statusService.js"/>
/// <reference path="~/app/utils/system/system-ns.js" />
/// <reference path="~/app/utils/system/system-string.js" />
"use strict";

app.controller("signupController", function ($scope, $location, $timeout, msgConst, errorService, authService, statusService) {
    statusService.clear();

    $scope.success = false;
    $scope.submitted = false;

    $scope.formData = { userName: "", email: "", password: "", confirmPassword: "" };

    $scope.signUp = function () {
        $scope.submitted = true;
        authService.logout();
        authService.account.save($scope.formData, onSignupSucceed, onSignupFailed);

        function onSignupSucceed() {
            $scope.success = true;
            statusService.success(system.string.format(msgConst.SIGNUP_SUCCESS_FORMAT, $scope.formData.userName, $scope.formData.email));
        }

        function onSignupFailed(response) {
            statusService.error(errorService.parseFormResponse(response));
        }
    };
});