'use strict';

app.factory('data', ['$http', '$q', 'baseServiceUrl', 'identity', 'authorization', function ($http, $q, baseUrl, identity, authorization) {
    return {
        getStats: getStats,
        getCities: getCities,
        getTop10trips: getTop10trips,
        getTop10drivers: getTop10drivers,
        getDrivers: getDriversByPageAndUsername,
        createTrip: postTrip,
        getTripDetails: getTripDetails,
        joinTrip: joinTrip,
        getTrips: getTrips,
        getDriverDetails: getDriverDetails
    };

    function getDriverDetails(driverId) {
        var url = baseUrl + '/api/drivers/' + driverId;
        return getRequest(url);
    }

    function joinTrip(tripId) {
        var deferred = $q.defer();
        var url = baseUrl + '/api/trips/' + tripId;
        var header = authorization.getAuthorizationHeader();

        $http({method: 'PUT', url: url, headers: {
            'Content-Type': 'application/json',
            'authorization': header ? header.Authorization : null
        }})
            .success(function (data, status, headers, config) {
                deferred.resolve(data);
            })
            .error(function (data, status, headers, config) {
                deferred.reject(data);
            });

        return deferred.promise;
    }

    function getTrips(tripsData) {
//        var queryUrl = baseUrl + '/api/trips?page=' + page;
//        if (username) {
//            queryUrl += '&username=' + username;
//        }
//
//        var tripsData = {
//
//        };

        var header = authorization.getAuthorizationHeader();
        var isLoggedIn = identity.isAuthenticated();

        if (!isLoggedIn || isLoggedIn) { // remove the isLoggedIn part when finished implementing functionality
            debugger;
            return getTop10trips();
        }

        $http({method: 'GET', url: url, headers: {
            'Content-Type': 'application/json', // change to  x-www-form-urlencoded
            'authorization': header ? header.Authorization : null
        }})
            .success(function (data, status, headers, config) {
                deferred.resolve(data);
            })
            .error(function (data, status, headers, config) {
                deferred.reject(data);
            });

        return deferred.promise;
    }

    function getDriversByPageAndUsername(username, page) {

        var queryUrl = baseUrl + '/api/drivers?page=' + page;
        if (username) {
            queryUrl += '&username=' + username;
        }

        var isLoggedIn = identity.isAuthenticated();

        if (!isLoggedIn) {
            return getTop10drivers();
        }

        var dataToGet = {
            page: page,
            username: username
        };

        return getRequest(queryUrl, dataToGet);
    }

    function getTripsByPageAnd() {

    }

    function getTripDetails(tripId) {
        var url = baseUrl + '/api/trips/' + tripId;
        return getRequest(url);
    }

    function postTrip(tripData) {
        debugger;
        var url = baseUrl + '/api/trips';
        return postRequest(url, tripData);
    }

    function postRequest(url, sendData) {
        var deferred = $q.defer();
        var header = authorization.getAuthorizationHeader();

        $http({ method: 'POST', url: url, headers: {
                'Content-Type': 'application/json', // json?
                'authorization': header ? header.Authorization : null
            },
                data: sendData}
        )
            .success(function (data, status, headers, config) {
                deferred.resolve(data);
            })
            .error(function (data, status, headers, config) {
                deferred.reject(data);
            });

        return deferred.promise;
    }

    function getStats() {
        var url = baseUrl + '/api/stats';
        return getRequest(url);
    }

    function getCities() {
        var url = baseUrl + '/api/cities';
        return getRequest(url);
    }

    function getTop10trips() {
        var url = baseUrl + '/api/trips';
        return getRequest(url);
    }

    function getTop10drivers() {
        var url = baseUrl + '/api/drivers';
        return  getRequest(url);
    }

    function getRequest(url, dataToGet) {
        var deferred = $q.defer();
        var header = authorization.getAuthorizationHeader();
        dataToGet = dataToGet || {};


        $http({ method: 'GET', url: url, headers: {
            'Content-Type': 'application/x-www-form-urlencoded',
            'authorization': header ? header.Authorization : null
        }})
            .success(function (data, status, headers, config) {
                deferred.resolve(data);
            })
            .error(function (data, status, headers, config) {
                deferred.reject(data);
            });

        return deferred.promise;
    }
}]);