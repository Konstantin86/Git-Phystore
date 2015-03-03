app.controller('confirmController', function ($scope, $location, $timeout, msgConst, statusService) {

    statusService.success(msgConst.SIGNUP_SUCCESS_CONFIRM);

    var timer = $timeout(function () {
        $timeout.cancel(timer);
        $location.path('/login');
    }, 5000);
});