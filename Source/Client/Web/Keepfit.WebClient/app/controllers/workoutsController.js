/// <reference path="~/scripts/angular.min.js"/>
/// <reference path="~/app/app.js"/>
/// <reference path="~/app/services/authService.js"/>
/// <reference path="~/app/services/workoutService.js"/>
/// <reference path="~/app/services/statusService.js"/>

"use strict";

app.controller("workoutsController", function ($scope, $location, authService, workoutService, statusService) {
    statusService.clear();

    $scope.userName = "testName";
    $scope.message = "This is gonna be your workouts list...";

    workoutService.workout.get({}, function (response) {
        $scope.message = response.data;
    }, function (err) {
        $scope.message = err.error_description;
    });
});