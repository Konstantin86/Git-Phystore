var app = angular.module('PhystoreApp', ['ngRoute', 'ngAnimate', 'ngSanitize', 'LocalStorageModule', 'mgcrea.ngStrap', 'mgcrea.ngStrap.helpers.dateParser', 'angular-loading-bar']);

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

    $routeProvider.otherwise({ redirectTo: "/home" });
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.init();
}]);

