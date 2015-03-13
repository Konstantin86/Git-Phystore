/// <reference path="~/scripts/angular.min.js"/>
/// <reference path="~/app/app.js"/>
/// <reference path="~/app/services/exerciseService.js"/>

"use strict";

app.controller("exerciseController", function ($scope, $location, $routeParams, exerciseService) {

    var id = $routeParams.id;

    $scope.exercise = {
        name: "",
        description: "",
        category: ""
    }

    if (id) {
        exerciseService.resource.get({ id: id }, function(result) {
            if (result) {
                $scope.exercise = result;
            } else {
            }
        });
    }

    $scope.ok = function() {
        exerciseService.resource.save($scope.exercise, function(response) {
            if (response) {
                $location.path("/exercises");
                // TODO if everything is ok redirect to exercises view $location.path('/exercises');
            } else {
            }
        });
    };
});