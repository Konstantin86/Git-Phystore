/// <reference path="~/scripts/angular.min.js"/>

/// <reference path="~/app/app.js"/>
/// <reference path="~/app/const/appConst.js"/>
/// <reference path="~/app/services/statusService.js"/>

"use strict";

app.controller("homeController", function ($scope, msgConst, statusService, appConst) {

    statusService.clear();

    $scope.welcomeText = msgConst.HOME_WELCOME;
    $scope.welcomeHeader = msgConst.HOME_WELCOME_HEADER;

    var hostUri = appConst.hostBase;

    var slides = $scope.slides = [];

    slides.push({
        image: hostUri + "content/images/1-set-your-goals.jpg",
        text: "Set your goals..."
    });

    slides.push({
        image: hostUri + "content/images/2-create-workout-plan.jpg",
        text: "Create workout plan..."
    });

    slides.push({
        image: hostUri + "content/images/3-work-hard.jpg",
        text: "Work hard..."
    });

    slides.push({
        image: hostUri + "content/images/4-take-suggestions-from-community.jpg",
        text: "Suggest with community..."
    });

    slides.push({
        image: hostUri + "content/images/5-do-something-every-day.jpg",
        text: "Do something every day..."
    });

    slides.push({
        image: hostUri + "content/images/6-achieve-your-goals.jpg",
        text: "Achieve your goals..."
    });

    slides.push({
        image: hostUri + "content/images/7-share-your-success.jpg",
        text: "Share your success..."
    });

    slides.push({
        image: hostUri + "content/images/8-observe-places.jpg",
        text: "Observe Places..."
    });
    
    $scope.myInterval = 5000;
});