app.controller('loginController', function ($scope, $location, authService) {
    $scope.formData = {
        userName: "",
        password: ""
    };

    $scope.message = "";

    $scope.login = function () {
        authService.login($scope.formData).then(function (response) {
            $location.path('/workouts');
        },
         function (err) {
             $scope.message = err.error_description;
         });
    };

    $scope.authExternalProvider = function (provider) {

        var redirectUri = location.protocol + '//' + location.host + '/authcomplete.html';

        var serviceBaseUri = appConfig.getInstance().getServiceUri();

        var externalProviderUrl = serviceBaseUri + "api/account/externalLogin?provider=" + provider
                                                                    + "&response_type=token&client_id=" + 'Keepfit'
                                                                    + "&redirect_uri=" + redirectUri;
        window.$windowScope = $scope;

        var oauthWindow = window.open(externalProviderUrl, "Authenticate Account", "location=0,status=0,width=600,height=750");
    };

    $scope.authCompletedCB = function (fragment) {

        $scope.$apply(function () {

            if (fragment.haslocalaccount == 'False') {

                authService.logOut();

                authService.externalAuthData = {
                    provider: fragment.provider,
                    userName: fragment.external_user_name,
                    externalAccessToken: fragment.external_access_token
                };

                $location.path('/associate');

            }
            else {
                //Obtain access token and redirect to orders
                var externalData = { provider: fragment.provider, externalAccessToken: fragment.external_access_token };
                authService.obtainAccessToken(externalData).then(function (response) {

                    $location.path('/workouts');

                },
             function (err) {
                 $scope.message = err.error_description;
             });
            }

        });
    }
});