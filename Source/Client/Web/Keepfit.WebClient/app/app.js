/// <reference path="~/scripts/angular.min.js"/>

"use strict";

var app = angular.module("KeepfitApp", ["ngRoute", "ngResource", "ngAnimate", "ngSanitize", "ui.bootstrap", "ui.bootstrap.tpls", "flow", "LocalStorageModule", "mgcrea.ngStrap", "angular-loading-bar", "cfp.loadingBar", "infinite-scroll"]);

app.run(["authService", function (authService) {
    authService.init();
}]);