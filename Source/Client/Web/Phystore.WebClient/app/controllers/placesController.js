app.controller('placesController', function ($scope, placeService, placePhotosService, $uimodal, $filter) {

    $scope.exploreNearby = "New York";
    $scope.exploreQuery = "";
    $scope.filterValue = "";

    $scope.places = [];
    $scope.filteredPlaces = [];
    $scope.filteredPlacesCount = 0;

    //paging
    $scope.totalRecordsCount = 0;
    $scope.pageSize = 10;
    $scope.page = 0;

    $scope.busy = false;

    init();

    function filterPlaces(filterInput) {
        $scope.filteredPlaces = $filter("placeNameCategoryFilter")($scope.places, filterInput);
        $scope.filteredPlacesCount = $scope.filteredPlaces.length;
    }

    function createWatche() {
        $scope.$watch("filterValue", function (filterInput) {
            filterPlaces(filterInput);
        });
    }

    function init() {
        createWatche();
        //getPlaces();
    }

    function getPlaces() {

        if ($scope.busy) return;
        $scope.busy = true;

        var offset = ($scope.pageSize) * ($scope.page - 1);

        placeService.resource.get({ near: $scope.exploreNearby, query: $scope.exploreQuery, limit: $scope.pageSize, offset: offset }, function (placesResult) {

            if (placesResult.response.groups) {
                $scope.places = $scope.places.concat(placesResult.response.groups[0].items);
                $scope.totalRecordsCount = placesResult.response.totalResults;
                filterPlaces('');
            }
            else {
                $scope.places = [];
                $scope.totalRecordsCount = 0;
            }

            $scope.busy = false;
        });
    };

    $scope.doSearch = function () {

        $scope.page = 1;
        getPlaces();
    };

    $scope.myPagingFunction = function () {
        $scope.page++;
        getPlaces();
    };

    $scope.pageChanged = function (page) {

        $scope.page = page;
        getPlaces();
    };

    $scope.buildCategoryIcon = function (icon) {

        return icon.prefix + '44' + icon.suffix;
    };

    $scope.buildVenueThumbnail = function (photo) {

        return photo.items[0].prefix + '128x128' + photo.items[0].suffix;
    };

    $scope.showVenuePhotos = function (venueId, venueName) {

        placePhotosService.resource.get({ venueId: venueId }, function (photosResult) {

            var modalInstance = $uimodal.open({
                templateUrl: 'app/views/modal/placePhotos.html',
                controller: 'placePhotosController',
                resolve: {
                    venueName: function () {
                        return venueName;
                    },
                    venuePhotos: function () {
                        return photosResult.response.photos.items;
                    }
                }
            });

            modalInstance.result.then(function () {
                //$scope.selected = selectedItem;
            }, function () {
                //alert('Modal dismissed at: ' + new Date());
            });

        });
    };
});