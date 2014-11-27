var mongoose = require('mongoose');

module.exports.init = function() {
    var commentSchema = mongoose.Schema({
        author: {
            type: mongoose.Schema.ObjectId,
            ref: 'User',
            required: true
        },
        event: {
            type: mongoose.Schema.ObjectId,
            ref: 'Event',
            required: true
        },
        text: { type: String, required: true }
    });

    var Comment = mongoose.model('Comment', commentSchema);
};