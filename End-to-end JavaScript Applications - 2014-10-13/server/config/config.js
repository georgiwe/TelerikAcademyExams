var path = require('path');
var rootPath = path.normalize(__dirname + '/../../');

module.exports = {
    development: {
        rootPath: rootPath,
        db: 'mongodb://localhost:27017/academyevents',
        port: process.env.PORT || 1234,
        seed: false,
        defaultUserImage: '../../public/img/telerik-ninja.png',
        defaultUserImageUrl: 'http://www.nakov.com/wp-content/uploads/2011/09/image43.png'
    }
};