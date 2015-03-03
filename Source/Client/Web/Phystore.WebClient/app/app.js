var app = angular.module("PhystoreApp", ["ngRoute", "ngResource", "ngAnimate", "ngSanitize", "ui.bootstrap", "ui.bootstrap.tpls", "flow", "LocalStorageModule", "mgcrea.ngStrap", "mgcrea.ngStrap.helpers.dateParser", "angular-loading-bar", "infinite-scroll"]);

app.run(["authService", function (authService) {
    authService.init();
}]);

