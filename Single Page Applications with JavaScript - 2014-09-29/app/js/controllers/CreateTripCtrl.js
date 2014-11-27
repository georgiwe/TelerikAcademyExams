'use strict';

app.controller('CreateTripCtrl', ['$scope', 'data', 'notifier', '$location', function ($scope, data, notifier, $location) {

    window.scope = $scope;
    $scope.now = Date();

    data.getCities()
        .then(function (data) {
            $scope.cities = data;
        });

    $scope.create = create;

    function create(form) {
        if (form.$invalid){
            notifier.error('You have filled one or more fields in an invalid way');
            return;
        }

        if ($scope.newTrip.from === $scope.newTrip.to) {
            notifier.error('Must travel to another city');
            return;
        }

        if (!$scope.newTrip.from || !$scope.newTrip.to) {
            notifier.error('Please select two cities');
            return;
        }

        var tripdata = {
            "from": $scope.newTrip.from,
            "to": $scope.newTrip.to,
            "availableSeats": $scope.newTrip.seats,
            "departureTime": $scope.newTrip.departure
        };

        data.createTrip(tripdata)
            .then(function (data) {
                notifier.success('Trip successfully created');
                $location.path('/trips');
                console.log(data);
            }, function (error) {
                notifier.error(error.message);
            });
    }
}]);