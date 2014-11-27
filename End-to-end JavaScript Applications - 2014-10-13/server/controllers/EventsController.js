var data = require('../data'),
	categories = require('../data/constants/categories')
	seasons = require('../data/constants/seasons'),
	initiatives = require('../data/constants/initiatives'),
	parseInitiatives = require('../utilities/initiatives-parser')(initiatives, seasons),
	restrictions = require('../data/constants/restrictions'),

module.exports = {
	getCreateEvent: function (req, res, next) {
		data.events.getPassed()
			.then(function (events) {
				console.log(events);
			});

		res.render('events/create', {
			categories: categories,
			restrictions: restrictions,
			initiatives: initiatives,
			seasons: seasons,
		});
	},
	postCreateEvent: function (req, res, next) {
		var eventData = req.body;
		var user = req.user;

		var type = getTypeData(eventData);

		eventData.creatorPhone = user.phoneNumber;
		eventData.creatorName = user.fullname;
		eventData.restriction = eventData.type;
		eventData.type = type;

		console.log(eventData);
		console.log(eventData.date);

		data.events.add(eventData)
		.then(function (event) {
				// res.redirect('/success');
				res.redirect('/');
			}, function (err) {
				console.log(err);
				res.redirect('/error');
			});
	},
	getUpcoming: function (req, res, next) {
		var eventsByCategories = {};

		data.events.getByCategory('Academy initiative')
			.then(function (events) {
				eventsByCategories.academyInitiatives = events;
			})
			.then(function () {
				data.events.getByCategory('Study arrangement')
					.then(function (events) {
						eventsByCategories.studyArrangements = events;
					});
			})
			.then(function () {
				data.events.getByCategory('Free time')
					.then(function (events) {
						eventsByCategories.freeTime = events;

						res.render('events/upcoming', {
							events: {
								academyInitiatives: eventsByCategories.academyInitiatives,
								studyArrangements: eventsByCategories.studyArrangements,
								freeTime: eventsByCategories.freeTime,
							}
						});
					});
			});
	}
};

function getTypeData (eventData) {
	var initiative = eventData.initrest;
	var season = eventData.seasonrest;

	return {
		initiative: initiative,
		season: season
	};
}

function typeDataIsValid (argument) {
	// everything validates in database.
	// if i have the time, i'll do it here too.
}