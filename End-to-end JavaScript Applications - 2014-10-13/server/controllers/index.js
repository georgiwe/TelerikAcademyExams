var UsersController = require('./UsersController');
var EventsController = require('./EventsController');
var ErrorsController = require('./ErrorsController');
var IndexController = require('./IndexController');

module.exports = {
    users: UsersController,
    events: EventsController,
    errors: ErrorsController,
    index: IndexController,
};