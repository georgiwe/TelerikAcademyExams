'use strict';

app.controller('HomeCtrl', ['$scope', 'data', 'notifier', function ($scope, data, notifier) {

    window.scope = $scope; // for testing
//    $scope.drivers = {
//        name: '',
//        numberOfUpcomingTrips: 5,
//        numberOfTotalTrips: 6
//    };

    populateStats();
    populateTrips();
    populateDrivers();

    function populateStats() {
        data.getStats()
            .then(function (data) {
                $scope.stats = data;
            });
    }

    function populateTrips() {
        data.getTop10trips()
            .then(function (data) {
//                console.log(data);
                $scope.trips = data;
            });
    }

    function populateDrivers() {
        data.getTop10drivers()
            .then(function (data) {
//                console.log(data);
                $scope.drivers = data;
            });
    }
}]);