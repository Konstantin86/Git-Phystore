/// <reference path="~/scripts/angular.min.js"/>

/// <reference path="~/app/app.js"/>
/// <reference path="~/app/services/authService.js"/>
/// <reference path="~/app/services/statusService.js"/>

"use strict";

app.controller("indexController", function ($scope, $location, authService, statusService) {

    $scope.state = statusService.state;

    $scope.logout = function () {
        authService.logout();
        $location.path("/home");
    }

    $scope.authData = authService.authData;

    $scope.closeAlert = function () {
        statusService.clear();
    };
});