﻿/// <reference path="~/scripts/angular.min.js"/>
/// <reference path="~/app/app.js"/>

"use strict";

app.service("statusService", function (cfpLoadingBar) {
    // statuses valid values: success, warning, danger, info
    var state = { message: "", status: "success" };

    var set = function (msg, stat) {
        cfpLoadingBar.complete();
        state.message = msg;
        state.status = stat;
    };

    var success = function (msg) {
        set(msg, "success");
    };

    var warning = function (msg) {
        set(msg, "warning");
    };

    var error = function (msg) {
        set(msg || "Unable to connect to service", "danger");
    };

    var loading = function (msg) {
        set(msg, "info");
        cfpLoadingBar.start();
    };

    var clear = function () {
        state.message = "";
    };
    
    this.set = set;
    this.state = state;
    this.success = success;
    this.loading = loading;
    this.warning = warning;
    this.error = error;
    this.clear = clear;
});