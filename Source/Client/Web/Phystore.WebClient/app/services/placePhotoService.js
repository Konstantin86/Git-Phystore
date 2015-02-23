//Get your client ID and secrent by visiting https://developer.foursquare.com
//Thise are demo client Id & secret.

app.service('placePhotosService', function ($resource) {

    var requestParms = {
        clientId: "OV5CAPBY3QJF251J0TAOOWOBSNLDHWRYP04YB5ABYDJI1SAS",
        clientSecret: "2F0DJR0NJKCC3HD1XJVRJBBWH0AV5JKFAPJ5GIAMI3GLBEIF",
        version: "20150122"
    }

    var requestUri = 'https://api.foursquare.com/v2/venues/:venueId/:action';
    var resource = $resource(requestUri,
    {
        action: 'photos',
        client_id: requestParms.clientId,
        client_secret: requestParms.clientSecret,
        v: requestParms.version,
        limit: '9',
        callback: 'JSON_CALLBACK'
    },
    {
        get: { method: 'JSONP' }
    });

    this.resource = resource;
});