'use strict';

app.filter('driversOnlyUserTripsFilter', function () {
    var filter = function (trip) {
        // this filters user's trips,
        // by checking the trip property
        return trip.isMine;
    }

    return filter;
});