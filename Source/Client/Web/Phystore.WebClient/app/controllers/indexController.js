app.controller('indexController', function ($scope, $location, authService, statusService) {

    $scope.state = statusService.state;

    $scope.logOut = function () {
        authService.logOut();
        $location.path('/home');
    }

    $scope.authData = authService.authData;

    $scope.closeAlert = function () {
        statusService.clear();
    };
});