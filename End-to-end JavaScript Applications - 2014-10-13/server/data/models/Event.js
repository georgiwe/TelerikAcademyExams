var mongoose = require('mongoose');
var categories = require('../constants/categories');
var initiatives = require('../constants/initiatives');
var seasons = require('../constants/seasons');

module.exports.init = function() {
    var eventSchema = mongoose.Schema({
    	title: { type: String, required: true },
    	desc: { type: String, required: true },
    	location: { type: String, required: true },
    	category: { type: String, required: true },
    	type: {
    		initiative: String,
    		season: String
    	},
    	creatorPhone: { type: String, required: true },
    	creatorName: { type: String, required: true },
    	comments: {
    		type: [{
    			username: String,
    			text: String
    		}],
    	},
    	joinedUsers: [mongoose.model('User')],
        evaluations: [ { organization: Number, venue: Number } ],
        restriction: { type: String, required: true },
        dateCreated: { type: Date, default: new Date(), required: true },
        date: { type: Date, required: true }
    });

    // eventSchema.path('evaluations').validate(function (categoryName) {

    // });

    eventSchema.virtual('score').get(function () {
        var score = 0;

        this.evaluations.forEach(function (eval) {
            score += eval.venue + eval.organizationn;
        });

        return score;
    });

    eventSchema.path('date').validate(function (date) {
        return (new Date()) < date;
    });

    eventSchema.path('category').validate(function (categoryName) {
        if (categories.indexOf(categoryName) !== -1) {
            return true;
        }

        return false;
    });

    eventSchema.path('type.initiative').validate(function (initiative) {
    	var validInitiative = initiatives.indexOf(initiative) !== -1;
    	return validInitiative;
    });

    eventSchema.path('type.season').validate(function (season) {
    	var validSeason = seasons.indexOf(season) !== -1;
    	return validSeason;
    });

    var Event = mongoose.model('Event', eventSchema);
};