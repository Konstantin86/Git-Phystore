app.controller('accountController', function ($scope, $http, $location, authService, workoutService) {

    $scope.updateStatus = "";
    $scope.changePasswordStatusMessage = "";
    $scope.formData = authService.authData;
    $scope.photoPath = authService.getPhotoPath();

    $scope.securityFormData = authService.securityData;

    $scope.personalizationPanelTitle = "Personalization";

    $scope.securityPanel = {
        "title": "Security",
        "body": "Change password."
    };

    $scope.photoWidth = 360;

    $scope.activePanel = 0;

    // TODO kb - ng-flow upload file by a click http://stackoverflow.com/questions/25740110/flow-js-upload-file-on-click (not automatically)
    // TODO kb - angular $http customize headers
    // TODO kb - js string contains var s = "foo"; alert(s.indexOf("oo") > -1);

    $scope.onFileAdded = function (file, event) {
        //event.preventDefault();
        //$scope.testImg = file;
        //var serviceBaseUri = appConfig.getInstance().getServiceUri();
        //$http.post(serviceBaseUri + 'api/account/photo', file).then(function (response) {
        //    return response;
        //});
    };

    $scope.onFilesAdded = function (files, event) {
        var flow = this.$flow;
        $scope.testImg = files[0];
        var authHeaderData = authService.getAuthHeader();
        flow.defaults.headers.Authorization = authHeaderData;
        flow.opts.headers.Authorization = authHeaderData;
    };

    $scope.onFilesSubmitted = function (files, event) {
        //var flow = this.$flow;
        //$scope.testImg = files[0];
        //var authHeaderData = authService.getAuthHeader();
        //flow.defaults.headers.Authorization = authHeaderData;
        //flow.opts.headers.Authorization = authHeaderData;
        //flow.upload();
    };

    $scope.onUploadSuccess = function (file, message) {
        var path = "http://az725561.vo.msecnd.net/media/" + message.split('"').join('') + "?width=360";
        authService.setPhotoPath(path);
        $scope.photoPath = authService.getPhotoPath();
        var s = 5;
    }

    //$scope.$on('flow::fileAdded', function (event, $flow, flowFile) {
    //    event.preventDefault();//prevent file from uploading
    //});

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