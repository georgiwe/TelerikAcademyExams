define(['jquery', 'handlebars'], function ($) {

    var postTemplate = '{{#each this}} <div class="post-container"> <span class="post-id">{{id}}</span> <span class="post-title">{{title}}</span> <span class="post-date">{{postDate}}</span> <span class="post-userid">{{user.id}}</span> <span class="post-username">{{user.username}}</span> </div> {{/each}}';
    var $outputContainer;

    function initialize(outputContainerSelector) {
        outputContainerSelector = outputContainerSelector || '#chat-window';
        $outputContainer = $(outputContainerSelector);
    }

    function display(posts) {
        var template = Handlebars.compile(postTemplate);
        $outputContainer.html(template(posts));
    }

    return {
        init: initialize,
        output: display,
    };
});