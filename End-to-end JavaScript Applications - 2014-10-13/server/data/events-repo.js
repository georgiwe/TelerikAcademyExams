var Event = require('mongoose').model('Event');
var Promise = require('bluebird');

module.exports = {
	add: function (event) {
    	var promise = new Promise(function (resolve, reject) {
            Event.create(event, function (err, createdEvent) {
                if (err) {
                    reject(err);
                    return;
                }

                resolve(createdEvent);
            });
        });

        return promise;
	},

    remove: function (id) {
    	var promise = new Promise(resolve, reject);
    	
        Event.findOne({ _id: id }).remove().exec(function (err) {
        	if (err) {
        		reject(err);
        		return;
        	}

        	resolve();
        });
    },

    getById: function (id) {
    	var promise = new Promise(resolve, reject);

    	Event.findById(id, function (err, event) {
    		if (err) {
    			reject(err);
    		}

    		resolve(event);
    	});

    	return promise;
    },

    getByCategory: function (categoryName, page, pageSize) {
        page = page || 0;
        pageSize = pageSize || 10;

        var promise = new Promise(function (resolve, reject) {
            Event.find({ category: categoryName })
                .sort({ date: 1 })
                .skip(page * pageSize)
                .limit(pageSize)
                .exec(function (err, events) {
                    if (err) {
                        reject(err);
                        return;
                    }

                    resolve(events);
                });
        });

        return promise;
    },

    getPassed: function (page, pageSize) {
        page = page || 0;
        pageSize = pageSize || 10;

        var promise = new Promise(function (resolve, reject) {
            Event.find()
                .where('date').gt(new Date())
                .sort({ date: 1 })
                .skip(page * pageSize)
                .limit(pageSize)
                .exec(function (err, events) {
                    if (err) {
                        reject(err);
                        return;
                    }

                    resolve(events);
                });
        });

        return promise;
    }
};