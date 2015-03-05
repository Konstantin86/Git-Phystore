/// <reference path="~/scripts/angular.min.js"/>

/// <reference path="~/app/app.js"/>
/// <reference path="~/app/const/appConst.js"/>

"use strict";

app.service("exerciseService", function ($resource, appConst) {

    var serviceBaseUri = appConst.serviceBase;

    var resource = $resource(serviceBaseUri + 'api/exercise',
    {
        //action: 'get'
    });

    this.resource = resource;
});