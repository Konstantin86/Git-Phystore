'use strict';
app.controller('associateController', ['$scope', '$location', '$timeout', 'authService', function ($scope, $location, $timeout, authService, statusService) {

    statusService.clear();
    $scope.submitted = false;

    $scope.registerData = {
        userName: authService.externalAuthData.userName,
        email: authService.externalAuthData.email,
        provider: authService.externalAuthData.provider,
        externalAccessToken: authService.externalAuthData.externalAccessToken
    };

    $scope.registerExternal = function () {
        $scope.submitted = true;

        authService.registerExternal($scope.registerData).then(function (response) {
            statusService.success("User has been registered successfully, you will be redicted to workouts page in 2 seconds.");
            startTimer();
        }, function (response) {
              var errors = [];
              for (var key in response.modelState) {
                  errors.push(response.modelState[key]);
              }
              statusService.error("Failed to register user due to:" + errors.join(' '));
          });
    };

    var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $location.path('/workouts');
        }, 2000);
    }
}]);