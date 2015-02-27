app.controller('exercisesController', function ($scope, $filter, $location, exerciseService) {

    $scope.filter = "";

    $scope.exercises = [];
    $scope.filteredExercises = [];

    $scope.pageSize = 10;
    $scope.page = 0;

    $scope.busy = false;

    init();

    function filterData(input) {
        $scope.filteredExercises = $filter("exerciseNameDescriptionFilter")($scope.exercises, input);
    }

    function createWatche() {
        $scope.$watch("filter", function (input) {
            filterData(input);
        });
    }

    function init() {
        createWatche();
    }

    function get() {
        if ($scope.busy) return;
        $scope.busy = true;

        var offset = ($scope.pageSize) * ($scope.page - 1);

        exerciseService.resource.query({ take: $scope.pageSize, skip: offset }, function (result) {
            if (result) {
                $scope.exercises = $scope.exercises.concat(result);
                filterData('');
            }
            else {
                $scope.exercises = [];
            }

            $scope.busy = false;
        });
    };

    $scope.search = function () {
        $scope.page = 1;
        $scope.exercises = [];
        $scope.filteredExercises = [];
        get();
    };

    $scope.loadPage = function () {
        $scope.page++;
        get();
    };

    $scope.edit = function (id) {
        $location.path('/exercise').search({ id: id });
    };

    $scope.add = function () {
        $location.path('/exercise');
    };
});