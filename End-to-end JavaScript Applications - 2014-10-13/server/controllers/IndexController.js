var data = require('../data');

module.exports = {
	getEvents: function (req, res, next) {
		
		data.events.getPassed()
			.then(function (events) {
				res.render('index', {
					events: events
				});
			});
	}
};