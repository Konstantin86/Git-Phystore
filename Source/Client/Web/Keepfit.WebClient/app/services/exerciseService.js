/// <reference path="~/scripts/angular.min.js"/>

/// <reference path="~/app/app.js"/>
/// <reference path="~/app/const/appConst.js"/>

"use strict";

app.service("exerciseService", function ($resource, appConst) {

    var resource = $resource(appConst.serviceBase + "/:action", { action: "api/exercise" },
    {
    });

    this.resource = resource;
});