'use strict';

app.controller('TripsCtrl', ['$scope', 'data', 'notifier', 'authorization', function($scope, data, notifier, authorization) {

    debugger;

    $scope.isLoggedIn = authorization.getAuthorizationHeader();
    $scope.filter = filter;

    filter();

    function filter() {
        data.getTrips()
            .then(function (data) {
                $scope.trips = data;
            });
    }

    // THIS IS THE WORKING CODE. COMMENT OUT EVERYTHING ABOVE AND
    // UNCOMMENT THE FOLLOWING


//    data.getTop10trips()
//        .then(function (data) {
//            $scope.trips = data;
//        });
}]);