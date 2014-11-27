var mongoose = require('mongoose'),
    encryption = require('../../utilities/encryption');

module.exports.init = function(config) {
    var userSchema = mongoose.Schema({
        username: { type: String, required: true, unique: true },
        firstname: { type: String, required: true },
        lastname: { type: String, required: true },
        phoneNumber: String,
        email: { type: String, required: true },
        initiatives: { type: [String], required: true },
        seasons: { type: [String], required: true },
        profiles: {
            fb: String,
            google: String,
            twitter: String,
            linkedin: String
        },
        salt: String,
        hashPass: { type: String, set: hashPassword, required: true},
        points: {
            organisation: Number,
            venue: Number
        },
        imagePath: { type: String, default: config.defaultUserImage, required: true },
        imageUrl: { type: String, default: config.defaultUserImageUrl }
    });

    userSchema.virtual('profileImage').get(function () {
        if (this.imageUrl) {
            return this.imageUrl;
        } else {
            return this.imagePath;
        }
    });

    userSchema.virtual('profileImage').set(function (newImageUrl) {
        this.imageUrl = newImageUrl;
    });

    userSchema.virtual('fullname').get(function () {
        return this.firstname + ' ' + this.lastname;
    });

    userSchema.path('username').validate(function (username) {
        var lengthIsCorrect = 6 >= username.length && username.length <= 20;
        var regExp = /[^a-z\. 0-9]/ig;
        var charsAreCorrect = !regExp.test(username);

        return lengthIsCorrect && charsAreCorrect;
    });

    userSchema.method({
        authenticate: function(password) {
            if (encryption.generateHashedPassword(this.salt, password) === this.hashPass) {
                return true;
            }
            else {
                return false;
            }
        }
    });

    var User = mongoose.model('User', userSchema);

    function hashPassword (password) {
        var salt = encryption.generateSalt();
        var hashedPass = 
        encryption.generateHashedPassword(salt, password);
        this.salt = salt;
        return hashedPass;
    }
};