app.service('workoutService', function ($http, $q) {

    var serviceBaseUri = appConfig.getInstance().getServiceUri();
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