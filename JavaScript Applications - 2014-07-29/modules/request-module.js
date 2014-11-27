define(['jquery'], function ($) {

    function registerUser(url, userData) {
        return $.ajax({
            url: url,
            type: 'POST',
            data: userData,
        });
    }

    function logUserIn(url, userData) {
        return $.ajax({
            url: url,
            type: 'POST',
            data: userData
        });
    }

    function logUserOut(url, userData) {
        return $.ajax({
            url: url,
            type: 'PUT',
            headers: {
                'x-sessionkey': userData.sessionKey,
            },
        });
    }

    function getAllPosts(url) {
        return $.ajax({
            url: url,
            type: 'GET',
        });
    }

    function postPost(url, postData, userSessionKey) {
        return $.ajax({
            url: url,
            type: 'POST',
            data: postData,
            headers: {
                'x-sessionkey': userSessionKey,
            }
        });
    }

    return {
        registerUser: registerUser,
        logUserIn: logUserIn,
        logUserOut: logUserOut,
        getAllPosts: getAllPosts,
        postPost: postPost,
    };
});