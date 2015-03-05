/// <reference path="~/scripts/angular.min.js"/>

/// <reference path="~/app/app.js"/>
/// <reference path="~/app/const/appConst.js"/>

"use strict";

app.service('workoutService', function ($http, $q, appConst) {

    var serviceBaseUri = appConst.serviceBase;
    var getWorkouts = function() {

        var deferred = $q.defer();

        return $http.get(serviceBaseUri + 'api/workout').success(function (response) {
            deferred.resolve(response);
        }).error(function (err) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    this.getWorkouts = getWorkouts;
});