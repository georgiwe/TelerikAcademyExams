'use strict';

app.factory('authorization', ['identity', function(identity) {
    return {
        getAuthorizationHeader: function() {
            var currUser = identity.getCurrentUser();

            if (!currUser) {
                return undefined;
            }

            return {
                'Authorization': 'Bearer ' + currUser['access_token']
            }
        }
    }
}]);