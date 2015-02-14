var appConfig = (function () {

    var instance;

    function init() {

        //var serviceHostUri = 'http://jiraplexapi.azurewebsites.net/'
        var serviceHostUri = 'http://localhost:62749/';

        var getServiceHostUri = function () {
            return serviceHostUri;
        };

        return {
            getServiceSignalrUri: function () {
                return getServiceHostUri() + 'signalr/';
            },

            getServiceApiUri: function () {
                return getServiceHostUri() + 'api/';
            },

            getServiceUri: function () {
                return getServiceHostUri();
            }
        };
    };

    return {
        getInstance: function () {

            if (!instance) {
                instance = init();
            }

            return instance;
        }
    };
})();