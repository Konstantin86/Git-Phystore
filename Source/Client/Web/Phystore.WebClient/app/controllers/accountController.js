/// <reference path="~/scripts/angular.min.js"/>

/// <reference path="~/app/app.js"/>
/// <reference path="~/app/const/appConst.js"/>
/// <reference path="~/app/const/msgConst.js"/>
/// <reference path="~/app/services/authService.js"/>
/// <reference path="~/app/services/workoutService.js"/>
/// <reference path="~/app/services/statusService.js"/>

"use strict";

app.controller("accountController", function ($scope, $http, $location, authService, appConst, msgConst, workoutService, statusService) {

    statusService.clear();

    $scope.formData = authService.authData;
    $scope.securityFormData = authService.securityData;
    $scope.photoWidth = appConst.userAvatarWidth;

    // TODO kb - ng-flow upload file by a click http://stackoverflow.com/questions/25740110/flow-js-upload-file-on-click (not automatically)
    // TODO kb - angular $http customize headers
    // TODO kb - js string contains var s = "foo"; alert(s.indexOf("oo") > -1);

    $scope.onFilesAdded = function (files) {
        var flow = this.$flow;
        $scope.testImg = files[0];
        var authHeaderData = authService.getAuthHeader();
        flow.defaults.headers.Authorization = authHeaderData;
        flow.opts.headers.Authorization = authHeaderData;
    };

    $scope.onUploadSuccess = function (file, message) {
        var path = message.split('"').join('');
        authService.setPhotoPath(path);
    }

    $scope.deleteUserModal =
    {
        title: "Delete Confirmation",
        content: msgConst.ACCOUNT_DELETE,
        yes: function () {
            var modal = this;
            authService.deleteUser().then(function () {
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
        authService.update().then(function () {
            statusService.success(msgConst.ACCOUNT_UPDATE_SUCCESS);
        }, function (err) {
             statusService.error(err);
         });
    };

    $scope.changePassword = function () {
        authService.changePassword().then(function () {
            statusService.success(msgConst.ACCOUNT_PWD_CHANGE_SUCCESS);
        }, function (err) {
            statusService.error(err);
        });
    };
});