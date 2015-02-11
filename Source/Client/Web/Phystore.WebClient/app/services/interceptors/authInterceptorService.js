﻿app.service('authInterceptorService', ['$q', '$location', 'localStorageService', function ($q, $location, localStorageService) {

    var request = function (config) {
        config.headers = config.headers || {};

        var authData = localStorageService.get('authorizationData');
        if (authData) {
            config.headers.Authorization = 'Bearer ' + authData.token;
        }

        return config;
    }

    var responseError = function (rejection) {
        if (rejection.status === 401) {
            $location.path('/login');
        }
        return $q.reject(rejection);
    }

    this.request = request;
    this.responseError = responseError;
}]);