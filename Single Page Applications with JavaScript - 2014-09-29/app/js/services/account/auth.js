'use strict';

app.factory('auth', ['$http', '$q', 'identity', 'authorization', 'baseServiceUrl', '$route', function($http, $q, identity, authorization, baseServiceUrl, $route) {
    var usersApi = baseServiceUrl + '/api/users'

    return {
        signup: function(user) {
            var deferred = $q.defer();

            $http.post(usersApi + '/register', user)
                .success(function() {
                    deferred.resolve();
                }, function(response) {
                    deferred.reject(response);
                });

            return deferred.promise;
        },
        login: function(user){
            var deferred = $q.defer();
            user['grant_type'] = 'password';
            $http.post(usersApi + '/login', 'username=' + user.username + '&password=' + user.password + '&grant_type=password', { headers: {'Content-Type': 'application/x-www-form-urlencoded'} })
                .success(function(response) {
                    if (response["access_token"]) {
                        identity.setCurrentUser(response);
                        $route.reload();
                        deferred.resolve(true);
                    }
                    else {
                        deferred.resolve(false);
                    }
                });

            return deferred.promise;
        },
        logout: function() {
            var deferred = $q.defer();

            var headers = authorization.getAuthorizationHeader();
            $http.post(usersApi + '/logout', {}, { headers: headers })
                .success(function() {
                    identity.setCurrentUser(undefined);
                    $route.reload();
                    deferred.resolve();
                });

            return deferred.promise;
        },
        isAuthenticated: function() {
            if (identity.isAuthenticated()) {
                return true;
            }
            else {
                return $q.reject('not authorized');
            }
        }
    }
}])