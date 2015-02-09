app.controller('homeController', function ($scope, constMessage) {
    $scope.welcomeText = constMessage.HOME_WELCOME;
    $scope.welcomeHeader = constMessage.HOME_WELCOME_HEADER;
});