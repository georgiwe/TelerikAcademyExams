(function () {
    require.config({
        paths: {
            'jquery': "libs/jquery-1.11.1.min",
            // cryptoJS is an encrition framework and this is the google cdn for it
            'crypto': "http://crypto-js.googlecode.com/svn/tags/3.1.2/build/rollups/sha1",
            'handlebars': "libs/handlebars-v1.3.0",
            'underscore': "libs/underscore",
            'engine': "modules/engine",
            'uimodule': "modules/ui-module",
            'requestmodule': "modules/request-module",
        },
    });

    require(['engine'], function (engine) {
        engine.start();
    });
})();