﻿app.service('exerciseService', function ($resource) {

    var serviceBaseUri = appConfig.getInstance().getServiceUri();

    var resource = $resource(serviceBaseUri + 'api/exercise:action',
    {
        action: 'get'
    });

    this.resource = resource;
});