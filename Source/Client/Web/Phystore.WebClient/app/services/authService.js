app.service('authService', ['$http', '$q', 'localStorageService', function ($http, $q, localStorageService) {

    var serviceBaseUri = appConfig.getInstance().getServiceUri();

    var authData = {
            isAuth: false,
            userName: "",
            firstName: "",
            lastName: "",
            sex: "",
            joinDate: null,
            birthDate: null,
            country: "",
            city: "",
        };

    var register = function (registration) {
        logOut();

        return $http.post(serviceBaseUri + 'api/account/create', registration).then(function (response) {
            return response;
        });
    };

    var login = function (loginData) {

        var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

        var deferred = $q.defer();

        $http.post(serviceBaseUri + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {
            localStorageService.set('authorizationData',
                {
                    token: response.access_token,
                    userName: loginData.userName
                });

            authData.isAuth = true;
            authData.userName = loginData.userName;

            var userProfileData = $http.get(serviceBaseUri + 'api/account/user').success(function (response) {
                authData.firstName = response.firstName;
                authData.lastName = response.lastName;
                authData.sex = response.sex;
                authData.birthDate = response.birthDate;
                authData.joinDate = response.joinDate;
                authData.country = response.country;
                authData.city = response.city;
            });

            deferred.resolve(response);
        }).error(function (err) {
            logOut();
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var logOut = function () {
        localStorageService.remove('authorizationData');

        authData.isAuth = false;
        authData.userName = "";
    };

    var init = function () {
        var authorizationData = localStorageService.get('authorizationData');
        if (authorizationData) {
            authData.isAuth = true;
            authData.userName = authorizationData.userName;

            var userProfileData = $http.get(serviceBaseUri + 'api/account/user').success(function (response) {
                authData.firstName = response.firstName;
                authData.lastName = response.lastName;
                authData.sex = response.sex;
                authData.birthDate = response.birthDate;
                authData.joinDate = response.joinDate;
                authData.country = response.country;
                authData.city = response.city;
            });
        }
    };

    var update = function () {
        var deferred = $q.defer();

        return $http.post(serviceBaseUri + 'api/account/update', authData).success(function (response) {
            deferred.resolve(response);
        }).error(function (err) {
            logOut();
            deferred.reject(err);
        });

        return deferred.promise;
    };

    this.register = register;
    this.update = update;
    this.login = login;
    this.logOut = logOut;
    this.init = init;
    this.authData = authData;
}]);