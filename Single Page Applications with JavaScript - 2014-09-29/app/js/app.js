'use strict';

var app = angular.module('myApp', ['ngRoute', 'ngResource', 'ngCookies']).
    config(['$routeProvider', function($routeProvider) {
        $routeProvider
            .when('/', {
                templateUrl: 'views/partials/home.html',
                controller: 'HomeCtrl'
            })
            .when('/register', {
                templateUrl: 'views/partials/register.html',
                controller: 'SignUpCtrl'
            })
            .when('/trips', {
                templateUrl: 'views/partials/trips.html',
                controller: 'TripsCtrl'
            })
            .when('/drivers', {
                templateUrl: 'views/partials/drivers.html',
                controller: 'DriversCtrl'
            })
            .when('/createtrip', {
                templateUrl: 'views/partials/createTrip.html',
                controller: 'CreateTripCtrl'
            })
            .when('/unauthorized', {
                templateUrl: 'views/partials/unauthorized.html',
                controller: 'UnauthorizedCtrl'
            })
            .when('/trips/:id', {
                templateUrl: 'views/partials/tripdetails.html',
                controller: 'TripDetailsCtrl'
            })
            .when('/drivers/:id', {
                templateUrl: 'views/partials/driverdetails.html',
                controller: 'DriverDetailsCtrl'
            })
            .otherwise({ redirectTo: '/' });
    }])
    .value('toastr', toastr)
    .constant('baseServiceUrl', 'http://spa2014.bgcoder.com');

app.run(function ($rootScope, $location, notifier, identity) {
    var blacklisted = [
        '/createtrip'
//        '/register',
//        '/',
//        '/trips',
//        '/drivers'
    ];

    $rootScope.$on('$routeChangeStart', function (scope, next, current) {
//        debugger;
        if (!next.$$route) {
            return;
        }
        var nextRoute = next.$$route.originalPath;

//        console.log(nextRoute);

        if (blacklisted.indexOf(nextRoute) !== -1 &&
            !identity.isAuthenticated()) {
            notifier.error('You must be logged in to visit this page');
            $location.path('/unauthorized');
        }
    });
});

//app.run(function ($http, authorization) {
//    $http.defaults.headers.common.Authorization = authorization.getAuthorizationHeader().Authorization;
//});