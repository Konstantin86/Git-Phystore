app.service('authService', ['$http', '$q', 'localStorageService', function ($http, $q, localStorageService) {

    var serviceBaseUri = appConfig.getInstance().getServiceUri();

    var _authData = { isAuth: false, userName: "" };

    var _register = function (registration) {
        _logOut();

        return $http.post(serviceBaseUri + 'api/account/register', registration).then(function (response) {
            return response;
        });
    };

    var _login = function (loginData) {

        var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

        var deferred = $q.defer();

        $http.post(serviceBaseUri + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {
            localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName });

            _authData.isAuth = true;
            _authData.userName = loginData.userName;

            deferred.resolve(response);
        }).error(function (err, status) {
            _logOut();
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _logOut = function () {
        localStorageService.remove('authorizationData');

        _authData.isAuth = false;
        _authData.userName = "";
    };

    var _init = function () {

        var authorizationData = localStorageService.get('authorizationData');
        if (authorizationData) {
            _authData.isAuth = true;
            _authData.userName = authorizationData.userName;
        }

    }

    this.register = _register;
    this.login = _login;
    this.logOut = _logOut;
    this.init = _init;
    this.authData = _authData;
}]);