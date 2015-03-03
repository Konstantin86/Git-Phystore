app.service('exerciseService', function ($resource, appConst) {

    var serviceBaseUri = appConst.serviceBase;

    var resource = $resource(serviceBaseUri + 'api/exercise',
    {
        //action: 'get'
    });

    this.resource = resource;
});