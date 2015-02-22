app.controller('accountController', function ($scope, $http, $location, authService, appConstants, workoutService, statusService) {

    statusService.clear();

    $scope.formData = authService.authData;
    $scope.securityFormData = authService.securityData;

    //$scope.photoPath = authService.getPhotoPath();

    $scope.photoWidth = appConstants.userAvatarWidth;

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
        var path = message.split('"').join('');
        authService.setPhotoPath(path);
        //$scope.photoPath = authService.getPhotoPath();
    }

    $scope.deleteUserModal =
    {
        title: "Delete Confirmation",
        content: "Are you sure you want to delete your account from system?",
        yes: function () {
            var modal = this;
            authService.deleteUser().then(function (response) {
                modal.$hide();
                $location.path('/home');
            },
         function (err) {
             this.$hide();
             statusService.error(err);
         });

        }
    };

    $scope.update = function () {
        authService.update().then(function (response) {
            statusService.success("User profile is updated successfully");
        }, function (err) {
             statusService.error(err);
         });
    };

    $scope.changePassword = function () {
        authService.changePassword().then(function (response) {
            statusService.success("User password has been changed successfully");
        }, function (err) {
            statusService.error(err);
        });
    };
});