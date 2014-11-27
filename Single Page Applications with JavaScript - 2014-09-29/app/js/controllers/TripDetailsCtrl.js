'use strict';

app.controller('TripDetailsCtrl', ['$scope', 'data', '$routeParams', 'notifier', '$route', function ($scope, data, $routeParams, notifier, $route) {
    $scope.trip = {};
    $scope.joinTrip = joinTrip;
    var tripId = $routeParams.id;

    data.getTripDetails(tripId)
        .then(function (data) {
            $scope.trip = data;
        }, function (error) {
            notifier.error(error.message);
        });

    function joinTrip() {
        var trip = $scope.trip;

        data.joinTrip(trip.id)
            .then(function (data) {
                notifier.success('Successfully joined the trip to ' + trip.to);
                $scope.$apply();
                $route.reload();
                console.log(data);
            }, function (error) {
                notifier.error(error.message);
            });
    }
}]);