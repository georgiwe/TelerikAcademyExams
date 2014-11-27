var usersRepo = require('./users-repo');
var eventsRepo = require('./events-repo');

module.exports = {
	users: usersRepo,
	events: eventsRepo
};