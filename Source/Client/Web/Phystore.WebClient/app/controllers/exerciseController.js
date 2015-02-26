app.controller('exerciseController', function ($scope, exerciseService) {
    exerciseService.resource.get({ id: 44 }, function (result) {
        if (result) {
        }
        else {
        }
    });
});