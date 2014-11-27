'use strict';

app.controller('DriversCtrl', ['$scope', 'data', 'notifier', 'authorization', function($scope, data, notifier, authorization) {

    $scope.isLoggedIn = authorization.getAuthorizationHeader();
    $scope.filter = filter;
    $scope.page = 1;
    $scope.username = '';
    $scope.next = next;
    $scope.prev = prev;
    $scope.more = true;
    $scope.noResults = false;

    filter();

    function next() {
        $scope.page++;
        filter();
    }

    function prev() {
        if ($scope.page > 1) $scope.page--;
        filter();
    }

    function filter() {
        $scope.noResults = false;

        data.getDrivers($scope.username, $scope.page)
            .then(function (data) {
                $scope.drivers = data;
                if (!data.length) {
                    $scope.noResults = true;
                }
            }, function (error) {
                notifier.error(error.message);
            });
    }
}]);