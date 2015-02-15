var appConfig = (function () {

    var instance;

    function init() {

        //var serviceHostUri = 'http://jiraplexapi.azurewebsites.net/'
        var serviceHostUri = 'http://localhost:62749/';
        var hostUri = 'http://localhost:62891/';

        var getServiceHostUri = function () {
            return serviceHostUri;
        };

        var getHostUri = function() {
            return hostUri;
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
            },

            getHostUri: function () {
                return getHostUri();
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