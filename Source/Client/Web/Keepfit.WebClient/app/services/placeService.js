﻿/// <reference path="~/scripts/angular.min.js"/>
/// <reference path="~/scripts/angular-resource.js"/>

/// <reference path="~/app/app.js"/>

"use strict";

app.service("placeService", function ($resource) {

    var requestParms = {
        clientId: "OV5CAPBY3QJF251J0TAOOWOBSNLDHWRYP04YB5ABYDJI1SAS",
        clientSecret: "2F0DJR0NJKCC3HD1XJVRJBBWH0AV5JKFAPJ5GIAMI3GLBEIF",
        version: "20150122"
    }

    var apiUri = "https://api.foursquare.com/v2/venues/:action";

    var catApi = "https://api.foursquare.com/v2/venues/categories";

    var catResource = $resource(catApi,
    {
        client_id: requestParms.clientId,
        client_secret: requestParms.clientSecret,
        v: requestParms.version
    });

    var resource = $resource(apiUri,
    {
        action: "explore",
        client_id: requestParms.clientId,
        client_secret: requestParms.clientSecret,
        v: requestParms.version,
        venuePhotos: "1",
        callback: "JSON_CALLBACK"
    },
    {
        get: { method: "JSONP" }
    });

    this.resource = resource;
    this.catResource = catResource;
});