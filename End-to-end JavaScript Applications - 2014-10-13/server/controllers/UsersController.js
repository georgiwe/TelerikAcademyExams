var encryption = require('../utilities/encryption'),
data = require('../data'),
seasons = require('../data/constants/seasons'),
initiatives = require('../data/constants/initiatives'),
parseInitiatives = require('../utilities/initiatives-parser')(initiatives, seasons);

var CONTROLLER_NAME = 'users';

module.exports = {
    getRegister: function(req, res, next) {
        res.render(CONTROLLER_NAME + '/register');
    },
    postRegister: function(req, res, next) {
        var inputData = req.body;

        if (inputData.password != inputData.confirmPassword) {
            req.session.error = 'Passwords do not match!';
            res.redirect('/register');
        }
        else {
            var newUser = {
                username: inputData.username,
                hashPass: inputData.password,
                firstname: inputData.firstname,
                lastname: inputData.lastname,
                phoneNumber: inputData.phone,
                email: inputData.email,
                initiatives: parseInitiatives(inputData.initiatives, 0),
                seasons: parseInitiatives(inputData.initiatives, 1),
                profiles: {
                    fb: inputData.fb,
                    google: inputData.google,
                    twitter: inputData.twitter,
                    linkedin: inputData.linkedin
                }
            };

            data.users.add(newUser)
            .then(function (user) {
                res.redirect(CONTROLLER_NAME + '/login');
            }, function (err) {
                res.redirect('/error');
            });
        }
    },
    getLogin: function(req, res, next) {
        res.render(CONTROLLER_NAME + '/login');
    },
    getProfile: function (req, res, next) {
        var userProfiles = req.user.profiles;

        res.render(CONTROLLER_NAME + '/profile', {
            profileImage: req.user.profileImage,
            phoneNumber: req.user.phoneNumber,
            fb: userProfiles.fb,
            twitter: userProfiles.twitter,
            linkedin: userProfiles.linkedin,
            google: userProfiles.google
        });
    },
    postProfile: function (req, res, next) {
        var newUserData = req.body;

        data.users.update(req.user._id, newUserData)
            .then(function (user) {
                res.redirect('/');
            }, function (err) {
                res.redirect('/error');
            });
    }
};