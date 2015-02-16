app.controller('homeController', function ($scope, constMessage) {
    $scope.welcomeText = constMessage.HOME_WELCOME;
    $scope.welcomeHeader = constMessage.HOME_WELCOME_HEADER;

    var hostUri = appConfig.getInstance().getHostUri();

    var slides = $scope.slides = [];

    slides.push({
        image: hostUri + 'content/images/1-set-your-goals.jpg',
        text: 'Set your goals...'
    });

    slides.push({
        image: hostUri + 'content/images/2-create-workout-plan.jpg',
        text: 'Create workout plan...'
    });

    slides.push({
        image: hostUri + 'content/images/3-work-hard.jpg',
        text: 'Work hard...'
    });

    slides.push({
        image: hostUri + 'content/images/4-take-suggestions-from-community.jpg',
        text: 'Suggest with community...'
    });

    slides.push({
        image: hostUri + 'content/images/5-do-something-every-day.jpg',
        text: 'Do something every day...'
    });

    slides.push({
        image: hostUri + 'content/images/6-achieve-your-goals.jpg',
        text: 'Achieve your goals...'
    });

    slides.push({
        image: hostUri + 'content/images/7-share-your-success.jpg',
        text: 'Share your success...'
    });

    slides.push({
        image: hostUri + 'content/images/8-observe-places.jpg',
        text: 'Observe Places...'
    });
    
        $scope.myInterval = 5000;
        //var slides = $scope.slides = [];
        //$scope.addSlide = function () {
        //    var newWidth = 600 + slides.length + 1;
        //    slides.push({
        //        image: 'http://placekitten.com/' + newWidth + '/300',
        //        text: ['More', 'Extra', 'Lots of', 'Surplus'][slides.length % 4] + ' ' +
        //          ['Cats', 'Kittys', 'Felines', 'Cutes'][slides.length % 4]
        //    });
        //};
        //for (var i = 0; i < 4; i++) {
        //    $scope.addSlide();
        //}
});