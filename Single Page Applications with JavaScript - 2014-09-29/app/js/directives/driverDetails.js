'use strict';

app.directive('driverDetails', function () {
    return {
        scope: false,
        transclude: true,
        templateUrl: 'views/directives/driverDetailsTemplate.html',
        replace: true
    };
});