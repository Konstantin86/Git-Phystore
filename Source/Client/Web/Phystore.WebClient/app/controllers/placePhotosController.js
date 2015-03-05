/// <reference path="~/scripts/angular.min.js"/>

/// <reference path="~/app/app.js"/>

"use strict";

app.controller("placePhotosController", function ($scope, $modalInstance, venueName, venuePhotos) {

    $scope.venueName = venueName;
    $scope.venuePhotos = venuePhotos;

    $scope.close = function () {
        $modalInstance.dismiss("cancel");
    };

    $scope.buildVenuePhoto = function (photo) {
        return photo.prefix + "256x256" + photo.suffix;
    };
});