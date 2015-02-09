﻿var app = angular.module('PhystoreApp', ['ngRoute', 'ngAnimate', 'LocalStorageModule', 'mgcrea.ngStrap', 'mgcrea.ngStrap.modal', 'angular-loading-bar']);

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

    $routeProvider.when("/workouts", {
        controller: "workoutsController",
        templateUrl: "/app/views/workouts.html"
    });

    $routeProvider.otherwise({ redirectTo: "/home" });
});

app.run(['authService', function (authService) {
    authService.init();
}]);