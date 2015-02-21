app.controller('workoutsController', function ($scope, $location, authService, workoutService, statusService) {

    statusService.clear();

    $scope.userName = "testName";
    $scope.message = "This is gonna be your workouts list...";

    workoutService.getWorkouts().then(function (response) {
        $scope.message = response.data;
    }, function (err) {
        $scope.message = err.error_description;
    });
});