app.controller('signupController', function ($scope, $location, $timeout, constMessage, authService) {

    $scope.success = false;
    $scope.message = "";

    $scope.submitted = false;

    $scope.registration = {
        userName: "",
        email: "",
        password: "",
        roleName: "user",
        confirmPassword: ""
    };

    $scope.signUp = function () {
        $scope.submitted = true;

        authService.register($scope.registration).then(function (response) {
            $scope.success = true;
            $scope.confirmationMessage = system.string.format(constMessage.SIGNUP_SUCCESS_FORMAT, $scope.registration.userName, $scope.registration.email);
        },
         function (response) {
             var errors = [];
             for (var key in response.data.modelState) {
                 for (var i = 0; i < response.data.modelState[key].length; i++) {
                     errors.push(response.data.modelState[key][i]);
                 }
             }
             $scope.message = "Failed to register user due to:" + errors.join(' ');
         });
    };
});