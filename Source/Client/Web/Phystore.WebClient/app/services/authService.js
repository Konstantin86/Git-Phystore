/// <reference path="~/scripts/angular.min.js"/>
/// <reference path="~/scripts/angular-local-storage.js"/>
/// <reference path="~/app/app.js"/>
/// <reference path="~/app/const/appConst.js"/>

"use strict";

app.service("authService", function ($http, $resource, $q, localStorageService, appConst) {

    var resource = $resource(appConst.serviceBase + "/:action", { action: "api/account" },
    {
        token: {
            method: "POST",
            headers: { "Content-Type": "application/x-www-form-urlencoded" },
            params: { action: "token" }
        }
    });

    var userData = { isAuth: false, userName: "", firstName: "", lastName: "", sex: "", joinDate: null, birthDate: null, country: "", city: "", photoPath: "" };
    var externalAuthData = { provider: "", userName: "", email: "", externalAccessToken: "" };
    var securityData = { oldPassword: "", password: "", confirmPassword: "" };

    var logout = function () {
        localStorageService.remove("authorizationData");
        userData.isAuth = false;
        userData.userName = "";
    };

    var saveAuthData = function(accessToken, userName) {
        localStorageService.set("authorizationData",
        {
            token: accessToken,
            userName: userName
        });

        userData.isAuth = true;
        userData.userName = userName;
    };

    var login = function (credentials) {
        var deferred = $q.defer();

        var onLoginSucceed = function (response) {
            saveAuthData(response.access_token, credentials.userName);

            $http.get(appConst.serviceBase + "api/account/user").success(function (response) {
                userData.firstName = response.firstName;
                userData.lastName = response.lastName;
                userData.sex = response.sex;
                userData.birthDate = response.birthDate;
                userData.joinDate = response.joinDate;
                userData.country = response.country;
                userData.city = response.city;
                userData.photoPath = appConst.cdnMediaBase + response.photoPath + "?width=" + appConst.userAvatarWidth;
            });

            deferred.resolve(response);
        };

        var onLoginFailed = function (response) {
            logout();
            deferred.reject(response);
        };

        resource.token("grant_type=password&username=" + credentials.userName + "&password=" + credentials.password, onLoginSucceed, onLoginFailed);

        return deferred.promise;
    };

    var init = function () {
        var authorizationData = localStorageService.get('authorizationData');
        if (authorizationData) {
            userData.isAuth = true;
            userData.userName = authorizationData.userName;

            $http.get(appConst.serviceBase + 'api/account/user').success(function (response) {
                userData.firstName = response.firstName;
                userData.lastName = response.lastName;
                userData.sex = response.sex;
                userData.birthDate = response.birthDate;
                userData.joinDate = response.joinDate;
                userData.country = response.country;
                userData.city = response.city;
                userData.photoPath = appConst.cdnMediaBase + response.photoPath + "?width=" + appConst.userAvatarWidth;
            });
        }
    };

    var update = function () {
        var deferred = $q.defer();

        $http.post(appConst.serviceBase + 'api/account/update', userData).success(function (response) {
            deferred.resolve(response);
        }).error(function (err) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var changePassword = function () {
        var deferred = $q.defer();

        $http.post(appConst.serviceBase + 'api/account/changepassword', securityData).success(function (response) {
            deferred.resolve(response);
        }).error(function (err) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var deleteUser = function () {
        var deferred = $q.defer();

        $http.delete(appConst.serviceBase + 'api/account/user').success(function (response) {
            logout();
            deferred.resolve(response);
        }).error(function (err) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var registerExternal = function (registerExternalData) {
        var deferred = $q.defer();

        $http.post(appConst.serviceBase + 'api/account/registerexternal', registerExternalData).success(function (response) {
            saveAuthData(response.access_token, response.userName);
            deferred.resolve(response);
        }).error(function (err) {
            logout();
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var obtainAccessToken = function (externalData) {
        var deferred = $q.defer();

        $http.get(appConst.serviceBase + 'api/account/ObtainLocalAccessToken', { params: { provider: externalData.provider, externalAccessToken: externalData.externalAccessToken } }).success(function (response) {
            saveAuthData(response.access_token, response.userName);
            deferred.resolve(response);
        }).error(function (err) {
            logout();
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var getAuthHeader = function () {
        return "Bearer " + localStorageService.get("authorizationData").token;
    };

    var setPhotoPath = function (photoPath) {
        userData.photoPath = appConst.cdnMediaBase + photoPath + "?width=" + appConst.userAvatarWidth;
    };

    var getPhotoPath = function () {
        return userData.photoPath;
    };

    var sendPassword = function (email) {
        var deferred = $q.defer();

        $http.get(appConst.serviceBase + "api/account/recoverPassword", { params: { email: email } }).success(function (response) {
            deferred.resolve(response);
        }).error(function (err) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    this.update = update;
    this.registerExternal = registerExternal;
    this.deleteUser = deleteUser;
    this.changePassword = changePassword;
    this.login = login;
    this.logout = logout;
    this.init = init;
    this.authData = userData;
    this.setPhotoPath = setPhotoPath;
    this.getPhotoPath = getPhotoPath;
    this.getAuthHeader = getAuthHeader;
    this.securityData = securityData;
    this.externalAuthData = externalAuthData;
    this.obtainAccessToken = obtainAccessToken;
    this.sendPassword = sendPassword;
    this.account = resource;
});