var User = require('mongoose').model('User');
var Promise = require('bluebird');

module.exports = {
    add: function(user) {
    	var promise = new Promise(function (resolve, reject) {
            // var dbUser = new User(user);
            // add dbuser to create()
            
            User.create(user, function (err, createdUser) {
                if (err) {
                    reject(err);
                    return;
                }

                resolve(createdUser);
            });
        });

        return promise;
    },

    remove: function (id) {
    	var promise = new Promise(resolve, reject);
    	
        User.findOne({ _id: id }).remove().exec();
    },

    getById: function (id) {
        var promise = new Promise(function (resolve, reject) {            
            User.findById(id, function (err, user) {
                if (err) {
                    reject(err);
                }

                resolve(user);
            });
        });

        return promise;
    },

    update: function (id, newUserData) {
        var promise = new Promise(function (resolve, reject) {
            User.findById(id, function (err, user) {
                
                for (var field in newUserData) {
                    user.profiles[field] = newUserData[field];
                }

                if (newUserData.imageUrl) {
                    user.imageUrl = newUserData.imageUrl;
                }

                if (newUserData.phoneNumber) {
                    user.phoneNumber = newUserData.phoneNumber;
                }

                user.save(function (err) {
                    if (err) {
                        reject(err);
                        return;
                    }

                    resolve(user);
                });
            });
        });

        return promise;
    }
};