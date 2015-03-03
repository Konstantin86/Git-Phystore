app.controller('signupController', function ($scope, $location, $timeout, msgConst, authService, statusService) {

    statusService.clear();

    $scope.success = false;
    $scope.submitted = false;

    $scope.formData = {
        userName: "",
        email: "",
        password: "",
        roleName: "user",
        confirmPassword: ""
    };

    $scope.signUp = function () {
        $scope.submitted = true;

        authService.register($scope.formData).then(function (response) {
            $scope.success = true;
            statusService.success(system.string.format(msgConst.SIGNUP_SUCCESS_FORMAT, $scope.formData.userName, $scope.formData.email));
        },
         function (response) {

             var error = "";

             if (response.data && response.data.modelState) {
                 var errors = [];
                 for (var key in response.data.modelState) {
                     for (var i = 0; i < response.data.modelState[key].length; i++) {
                         errors.push(response.data.modelState[key][i]);
                     }
                 }

                 error = "Failed to register user due to: " + errors.join(' ');
             }

             statusService.error(error);
         });
    };
});