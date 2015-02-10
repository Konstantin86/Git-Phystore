app.controller('accountController', function ($scope, $location, authService, workoutService) {

    $scope.formData = {
        userName: authService.authData.userName,
        firstName: "test",
    };

    //$scope.userName = authService.authData.userName;
    //$scope.message = "This is gonna be your profile page...";
    $scope.personalizationPanel = {
        "title": "Personalization",
        "body": "Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus terry richardson ad squid. 3 wolf moon officia aute, non cupidatat skateboard dolor brunch."
    };

    $scope.securityPanel = {
        "title": "Security",
        "body": "Change password."
    };

    $scope.activePanel = 0;

    // TODO implement yes/no buttons (yes will make xhr request for user deletion) 
    $scope.deleteUserModal =
        {
            title: "Delete Confirmation",
            content: "Are you sure you want to delete account from system?"
        };
});