/// <reference path="~/scripts/angular.min.js"/>

/// <reference path="~/app/app.js"/>
/// <reference path="~/app/const/appConst.js"/>

"use strict";

app.config(function ($routeProvider) {
    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "/app/views/home.html"
    });

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "/app/views/login.html"
    });

    $routeProvider.when("/signup", {
        controller: "signupController",
        templateUrl: "/app/views/signup.html"
    });

    $routeProvider.when("/confirm", {
        controller: "confirmController",
        templateUrl: "/app/views/confirm.html"
    });

    $routeProvider.when("/account", {
        controller: "accountController",
        templateUrl: "/app/views/account.html"
    });

    $routeProvider.when("/workouts", {
        controller: "workoutsController",
        templateUrl: "/app/views/workouts.html"
    });

    $routeProvider.when("/associate", {
        controller: "associateController",
        templateUrl: "/app/views/associate.html"
    });

    $routeProvider.when("/places", {
        controller: "placesController",
        templateUrl: "/app/views/places.html"
    });

    $routeProvider.when("/exercises", {
        controller: "exercisesController",
        templateUrl: "/app/views/exercises.html"
    });

    $routeProvider.when("/exercise", {
        controller: "exerciseController",
        templateUrl: "/app/views/exercise.html"
    });

    $routeProvider.when("/exercise:id", {
        controller: "exerciseController",
        templateUrl: "/app/views/exercise.html"
    });

    $routeProvider.otherwise({ redirectTo: "/home" });
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.config(["flowFactoryProvider", "appConst", function (flowFactoryProvider, appConst) {
    flowFactoryProvider.defaults = {
        target: appConst.serviceBase + "api/blob/upload",
        testChunks: false,
        permanentErrors: [404, 500, 501],
        maxChunkRetries: 1,
        chunkRetryInterval: 5000,
        simultaneousUploads: 4,
        singleFile: true
    };

    flowFactoryProvider.on("catchAll", function() {
        console.log("catchAll", arguments);
    });
}]);