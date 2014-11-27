define(['jquery', 'uimodule', 'requestmodule', 'underscore', 'crypto'], function ($, uiModule, reqModule, _) {

    function start() {
        var baseUrl = 'http://localhost:3000',
            postCache,
            loggedInUserString = 'loggedInUser',
            $loginButt = $('#login-butt'),
            $registerButt = $('#register-butt'),
            $passInput = $('#pass-input'),
            $usernameInput = $('#username-input'),
            $msgInput = $('#msg-input'),
            $sendMsgButt = $('#sendmsg-butt'),
            $logoutButt = $('#logout-butt'),
            $titleInput = $('#msg-title-input'),
            $loginWrapper = $('#login-wrapper'),
            $msgWrapper = $('#msg-wrapper'),
            $filterButt = $('#filter-butt'),
            $byUserInput = $('#filter-user-input'),
            $byPatternInput = $('#filter-pattern-input'),
            $orderByInp = $('#sortby-select'),
            $ascendingInp = $('#asc-inp'),
            $sortButt = $('#sort-butt'),
            $announcementsContainer = $('#announcements'),
            $postCountInput = $('#post-count-input'),
            $prevPageButt = $('#prev-page-butt'),
            $nextPageButt = $('#next-page-butt'),
            currPage = 1;

        uiModule.init();
        displayAllPosts();
        switchLoginAndMsgForms();

        $prevPageButt.on('click', function () {
            currPage--;
            if (currPage <= 0) {
                currPage = 1;
            }

            displayPosts(postCache);
        });

        $nextPageButt.on('click', function () {
            currPage++;

            if (currPage >= postCache.length) {
                currPage = postCache.length;
            }

            displayPosts(postCache);
        });

        $sortButt.on('click', function () {
            sortAndApplyFilterToPosts();
        });

        $filterButt.on('click', function () {
            var byUser = $byUserInput.val().toLowerCase();
            var byPattern = $byPatternInput.val().toLowerCase();
            byPattern = byPattern.replace(/ +/g, '%20');

            displayFilteredPosts(byUser, byPattern);
            sortAndApplyFilterToPosts();
        });

        $registerButt.on('click', function () {
            var userData = getUserDataFromForm();
            registerUser(userData);
        });

        $loginButt.on('click', function () {
            var userData = getUserDataFromForm();
            logUserIn(userData);
        });

        $logoutButt.on('click', function () {
            //var userDataString = sessionStorage.getItem(loggedInUserString);
            var userData = getCurrentLoggedInUserData();

            logUserOut(userData);
        });

        $sendMsgButt.on('click', function () {
            var title = $titleInput.val();
            var body = $msgInput.val();
            var postData = {
                title: title,
                body: body,
            };

            postPost(postData);
        });

        function sortAndApplyFilterToPosts() {
            var orderBy = $orderByInp.val();
            var sortAscending = $ascendingInp.val() === 'asc';

            applyCurrSort(orderBy, sortAscending);
        }

        function applyCurrSort(orderBy, sortAscending) {

            if (postCache && postCache.length) {
                postCache.sort(function (post1, post2) {
                    if (orderBy === 'title') {
                        var title1 = post1.title.toLowerCase();
                        var title2 = post2.title.toLowerCase();

                        if (title1 < title2) {
                            return -1;
                        }
                        if (title2 < title1) {
                            return 1;
                        }

                        return 0;
                    } else {
                        var date1 = post1.postDate;
                        var date2 = post2.postDate;

                        if (date1 < date2) {
                            return -1;
                        }
                        if (date2 < date1) {
                            return 1;
                        }

                        return 0;
                    }
                });

                if (!sortAscending) {
                    postCache.reverse();
                }

                displayPosts(postCache);
            }
        }

        function postPost(postData) {
            var postUrl = baseUrl + '/post';
            var userData = getCurrentLoggedInUserData();
            var isLoggedIn = checkIfLoggedIn(userData);

            if (!isLoggedIn) {
                outputError('Log in first!');
                return;
            }

            reqModule.postPost(postUrl, postData, userData.sessionKey)
                .then(function (data) {
                    //console.log('success make post');
                    //console.log(data);
                    postCache.push(data);
                    outputSuccess('Post created!');
                    displayPosts(postCache);
                }, function (error) {
                    console.log('fail make post');
                    outputError(error.responseJSON.message);
                });
        }

        function displayAllPosts(url) {
            var postsUrl = url || (baseUrl + '/post');

            reqModule.getAllPosts(postsUrl)
                .then(function (data) {
                    console.log('success get posts');
                    console.log(data);
                    displayPosts(data);
                    postCache = data;
                }, function (error) {
                    console.log('fail get posts');
                    outputError(error.responseJSON.message);
                });
        }

        function displayFilteredPosts(userFilter, patternFilter) {
            var filterUrl = baseUrl + '/post';

            if (patternFilter) {
                filterUrl += '?pattern=' + patternFilter;
            }

            if (userFilter) {
                if (patternFilter) {
                    filterUrl += '&user=' + userFilter;
                } else {
                    filterUrl += '?user=' + userFilter;
                }
            }

            displayAllPosts(filterUrl);
        }

        function displayPosts(posts) {
            countPerPage = parseInt($postCountInput.val());
            countPerPage = countPerPage || posts.length;

            var postsToOutput = _.chain(posts)
                .first(countPerPage * currPage)
                .last(countPerPage)
                .value();

            uiModule.output(postsToOutput);
        }

        function registerUser(userData) {
            var regUrl = baseUrl + '/user';

            reqModule.registerUser(regUrl, userData)
                .then(function (data) {
                    console.log('reg user success: ');
                    //console.log(data);
                    //console.log(userData.authCode);
                    outputSuccess('You have registered!');
                }, function (error) {
                    console.log('reg user fail');
                    outputError(error.responseJSON.message);
                });
        }

        function logUserIn(userData) {
            var loginUrl = baseUrl + '/auth';
            var isLoggedIn = checkIfLoggedIn(userData);

            if (!isLoggedIn) {
                outputError('Log in first!');
                return;
            }

            reqModule.logUserIn(loginUrl, userData)
                .then(function (data) {
                    console.log('success log in');
                    //console.log(data);
                    //console.log(userData.authCode);
                    updateSessionStorage(userData, data);
                    switchLoginAndMsgForms();
                    outputSuccess('You have logged in!');
                }, function (error) {
                    console.log('fail log in');
                    outputError(error.responseJSON.message);
                });
        }

        function logUserOut(userData) {
            var logoutUrl = baseUrl + '/user';
            var isLoggedIn = checkIfLoggedIn(userData);

            if (!isLoggedIn) {
                outputError('Log in first!');
                return;
            }

            reqModule.logUserOut(logoutUrl, userData)
                .then(function (data) {
                    console.log('success log out');
                    //console.log(data);
                    sessionStorage.removeItem(loggedInUserString);
                    switchLoginAndMsgForms();
                    outputSuccess('You have logged out!');
                }, function (error) {
                    console.log('error on log out');
                    outputError(error.responseJSON.message);
                });
        }

        function getUserDataFromForm() {
            var username = $usernameInput.val();
            validateUsername(username);
            var pass = $passInput.val();
            validatePass(pass)
            var authCode = CryptoJS.SHA1(username + pass).toString();

            return {
                username: username,
                pass: pass,
                authCode: authCode,
            };
        }

        function validateUsername(username) {
            if (typeof username !== 'string' ||
                    username.length > 40 ||
                    username.length < 6) {
                outputError('Invalid username!');
                throw new Error('Invalid username.');

            }
        }

        function validatePass(pass) {
            // same validation, for lack of better
            // maybe i'll change it if i have time
            if (typeof pass !== 'string' ||
                    pass.length > 40 ||
                    pass.length < 6) {
                outputError('Invalid password!');
                throw new Error('Invalid password.');
            }
        }

        function getCurrentLoggedInUserData() {
            var userDataStr = sessionStorage.getItem(loggedInUserString);
            var userData = JSON.parse(userDataStr);
            return userData;
        }

        function updateSessionStorage(userData, responseData) {
            sessionStorage.removeItem(loggedInUserString);
            sessionStorage.setItem(loggedInUserString, JSON.stringify({
                username: userData.username,
                sessionKey: responseData.sessionKey,
            }));
        }

        function outputError(msg) {
            //console.log('not implemented - output an error');
            //console.log(msg);
            $announcementsContainer
                .css({ backgroundColor: 'red', color: 'white' })
                .text(msg)
                .fadeIn(1000)
                .fadeOut(1300);
        }

        function outputSuccess(msg) {
            //console.log('not implemented - output success');
            //console.log(msg);
            $announcementsContainer
                .css({ backgroundColor: 'green', color: 'white' })
                .text(msg)
                .hide(1300)
                .fadeIn(1000)
                .fadeOut(1300);
        }

        function switchLoginAndMsgForms() {
            //$loginWrapper.is(':visible')
            if (sessionStorage.getItem(loggedInUserString)) {
                $loginWrapper.hide();
                $msgWrapper.show();
            } else {
                $msgWrapper.hide();
                $loginWrapper.show();
            }
        }

        function checkIfLoggedIn(userData) {
            return !!userData;
        }
    }

    return {
        start: start,
    };
});