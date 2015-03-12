/// <reference path="~/scripts/angular.min.js"/>
/// <reference path="~/app/app.js"/>
/// <reference path="~/app/const/appConst.js"/>
/// <reference path="~/app/const/msgConst.js"/>
/// <reference path="~/app/services/authService.js"/>
/// <reference path="~/app/services/workoutService.js"/>
/// <reference path="~/app/services/statusService.js"/>

"use strict";

app.controller("accountController", function ($scope, $location, cfpLoadingBar, authService, appConst, msgConst, workoutService, statusService) {

    statusService.clear();

    $scope.formData = authService.authData;
    $scope.securityFormData = authService.securityData;
    $scope.photoWidth = appConst.userAvatarWidth;

    $scope.onFilesAdded = function (files) {
        var flow = this.$flow;
        $scope.testImg = files[0];
        var authHeaderData = authService.getAuthHeader();
        flow.defaults.headers.Authorization = authHeaderData;
        flow.opts.headers.Authorization = authHeaderData;
    };

    $scope.onUploadProgress = function (file) {
        statusService.set("Uploading photo...", "info");
        cfpLoadingBar.start();
    };

    $scope.onUploadSuccess = function (file, message) {
        statusService.success("User photo is updated successfully");
        cfpLoadingBar.complete();
        authService.setPhoto(message.split('"').join(''));
    };

    $scope.deleteUserModal =
    {
        title: "Delete Confirmation",
        content: msgConst.ACCOUNT_DELETE,
        yes: function () {
            var modal = this;
            authService.account.delete({}, function () {
                modal.$hide();
                $location.path("/home");
                authService.logout();
            }, function (err) {
             this.$hide();
             statusService.error(err);
         });
        }
    };

    $scope.update = function () {
        authService.account.update($scope.formData, function () {
            statusService.success(msgConst.ACCOUNT_UPDATE_SUCCESS);
        }, function (err) {
             statusService.error(err);
         });
    };

    $scope.changePassword = function () {
        authService.account.updatePassword($scope.securityFormData, function () {
            statusService.success(msgConst.ACCOUNT_PWD_CHANGE_SUCCESS);
        }, function (err) {
            statusService.error(err);
        });
    };
});