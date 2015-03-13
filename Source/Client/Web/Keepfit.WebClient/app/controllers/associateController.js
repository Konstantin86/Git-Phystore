/// <reference path="~/scripts/angular.min.js"/>
/// <reference path="~/app/app.js"/>
/// <reference path="~/app/const/msgConst.js"/>
/// <reference path="~/app/services/authService.js"/>
/// <reference path="~/app/services/statusService.js"/>
/// <reference path="~/app/utils/system/system-ns.js" />
/// <reference path="~/app/utils/system/system-string.js" />
"use strict";

app.controller("associateController", function ($scope, $location, $timeout, msgConst, errorService, authService, statusService) {
    statusService.clear();

    $scope.submitted = false;
    $scope.registerData = authService.externalAuthData;

    var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $location.path("/account");
        }, 2000);
    }

    $scope.registerExternal = function () {
        $scope.submitted = true;

        authService.registerExternal($scope.registerData).then(function () {
            statusService.success(msgConst.ACCOUNT_ASSOCIATE_SUCCESS);
            startTimer();
        }, function (response) {
            statusService.error(errorService.parseFormResponse(response));
        });
    };
});