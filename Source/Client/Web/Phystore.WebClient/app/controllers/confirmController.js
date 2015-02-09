app.controller('confirmController', function ($scope, $location, $timeout, constMessage) {
    $scope.confirmationMessage = constMessage.SIGNUP_SUCCESS_CONFIRM;
    var timer = $timeout(function () {
        $timeout.cancel(timer);
        $location.path('/login');
    }, 5000);
});