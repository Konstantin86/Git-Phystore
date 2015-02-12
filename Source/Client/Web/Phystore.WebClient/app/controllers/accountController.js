app.controller('accountController', function ($scope, $location, authService, workoutService) {

    $scope.updateStatus = "";
    $scope.changePasswordStatusMessage = "";
    $scope.formData = authService.authData;

    $scope.securityFormData = authService.securityData;

    $scope.personalizationPanelTitle = "Personalization";

    $scope.securityPanel = {
        "title": "Security",
        "body": "Change password."
    };

    $scope.activePanel = 0;

    // TODO implement yes/no buttons (yes will make xhr request for user deletion) 
    $scope.deleteUserModal =
    {
        title: "Delete Confirmation",
        content: "Are you sure you want to delete your account from system?",
        deleteUser: function () {
            var modal = this;
            authService.deleteUser().then(function (response) {
                modal.$hide();
                $location.path('/home');
            },
         function (err) {
             this.$hide();
             $scope.updateSuccess = false;
             $scope.updateStatus = err;
         });

        }
    };

    $scope.update = function () {
        authService.update().then(function (response) {
            $scope.updateSuccess = true;
            $scope.updateStatus = "User profile is updated successfully";
        },
         function (err) {
             $scope.updateSuccess = false;
             $scope.updateStatus = err;
         });
    };

    $scope.changePassword = function () {
        authService.changePassword().then(function (response) {
            $scope.changePasswordSuccess = true;
            $scope.changePasswordStatusMessage = "User password has been changed successfully";
        },
         function (err) {
             $scope.changePasswordSuccess = false;
             $scope.changePasswordStatusMessage = err;
         });
    };
});