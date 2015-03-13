/// <reference path="~/scripts/angular.min.js"/>
/// <reference path="~/app/app.js"/>
/// <reference path="~/app/const/appConst.js"/>

"use strict";

app.service("workoutService", function ($resource, $q, appConst) {
    var resource = $resource(appConst.serviceBase + "/:action", { action: "api/workout" },
    {
    });

    this.workout = resource;
});