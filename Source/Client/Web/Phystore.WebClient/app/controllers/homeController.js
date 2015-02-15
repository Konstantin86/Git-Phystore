app.controller('homeController', function ($scope, constMessage) {
    $scope.welcomeText = constMessage.HOME_WELCOME;
    $scope.welcomeHeader = constMessage.HOME_WELCOME_HEADER;

    var hostUri = appConfig.getInstance().getHostUri();

    //$scope.myInterval = 5000;

    //var slides = $scope.slides = [];

    //Aivazovsky_Park.jpg
    //slides.push({
    //    image: hostUri + 'content/images/1.jpg',
    //    text: 'aaa'
    //});

    //slides.push({
    //    image: hostUri + 'content/images/0001.jpg',
    //    text: 'bbb'
    //});

        $scope.myInterval = 5000;
        var slides = $scope.slides = [];
        $scope.addSlide = function () {
            var newWidth = 600 + slides.length + 1;
            slides.push({
                image: 'http://placekitten.com/' + newWidth + '/300',
                text: ['More', 'Extra', 'Lots of', 'Surplus'][slides.length % 4] + ' ' +
                  ['Cats', 'Kittys', 'Felines', 'Cutes'][slides.length % 4]
            });
        };
        for (var i = 0; i < 4; i++) {
            $scope.addSlide();
        }
});