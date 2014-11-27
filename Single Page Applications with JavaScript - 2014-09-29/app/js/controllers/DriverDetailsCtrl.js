'use strict';

app.controller('DriverDetailsCtrl', ['$scope', 'data', '$routeParams', 'notifier',function ($scope, data, $routeParams, notifier) {
    $scope.driver = {};
    $scope.trips = [];

    data.getDriverDetails($routeParams.id)
        .then(function (data) {
            $scope.driver = data;
            $scope.trips = data.trips;
            console.log(data);
        }, function (error) {
            notifier.error(error.message);
        });
}]);