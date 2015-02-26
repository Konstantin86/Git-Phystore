﻿app.controller('exerciseController', function ($scope, $routeParams, exerciseService) {

    //http://stackoverflow.com/questions/21256947/passing-id-into-url-for-individual-view-in-angular-js
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
                // TODO if everything is ok redirect to exercises view $location.path('/exercises');
            } else {
            }
        });
    };
});