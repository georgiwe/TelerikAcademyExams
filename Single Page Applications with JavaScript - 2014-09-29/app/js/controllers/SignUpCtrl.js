'use strict';

app.controller('SignUpCtrl', ['$scope', '$location', 'auth', 'notifier', function($scope, $location, auth, notifier) {

    $scope.user = {
        email: '',
        password: '',
        confirmPassword: '',
        isDriver: false
    };

    $scope.signup = function(user) {

        auth.signup(user).then(function() {
            notifier.success('Registration successful!');
            $location.path('/');
        })
    }
}]);