app.controller('loginController', function ($scope, $location, authService) {
    $scope.formData = {
        userName: "",
        password: ""
    };

    $scope.message = "";

    $scope.login = function () {
        authService.login($scope.formData).then(function (response) {
            $location.path('/workouts');
        },
         function (err) {
             $scope.message = err.error_description;
         });
    };
});