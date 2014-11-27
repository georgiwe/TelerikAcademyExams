$.fn.gallery = function (cols) {
    var defaultColsNumber = 4;
    cols = cols || defaultColsNumber;

    //cols = 5;

    var $galleryContainer = this;
    $galleryContainer.addClass('gallery');
    var $allImgs = $galleryContainer.find('.image-container img');
    var totalImagesCount = $allImgs.length;
    var $galleryList = $galleryContainer.find('.gallery-list');
    var $selectedDiv = $galleryContainer.find('.selected');
    var $divSelected = $galleryContainer.find('.selected').hide();
    var $allImgContainers = $galleryContainer.find('.image-container');
    var $firstImgContainer = $allImgContainers.first();
    var oneContainerWidth = $firstImgContainer.first().outerWidth();
    var picMargin = parseFloat($firstImgContainer.css('margin-left'));
    // maybe adjust for single side margin
    this.width(oneContainerWidth * cols + cols * picMargin * 2);

    var visiblePrevImg = this.find('.selected .previous-image img');
    var visibleNextImg = this.find('.selected .next-image img');
    var visibleCurrImg = this.find('.selected .current-image img');

    $galleryList.on('click', '.image-container img', onImagesClick);

    $selectedDiv.on('click', 'img', onEnlargedClick);


    function onImagesClick() {
        $galleryList.off('click');
        var $this = $(this);
        visibleCurrImg.attr('src', $this.attr('src'));
        // I believe this to be faster than 
        // querying the DOM for data-id or whatever
        var prevImgSrc = $this.parent().prev().children('img').attr('src');
        var nextImgSrc = $this.parent().next().children('img').attr('src');

        if (!prevImgSrc) {
            prevImgSrc = $galleryContainer
                .find('.gallery-list .image-container:last-child img')
                .attr('src');
        }

        if (!nextImgSrc) {
            nextImgSrc = $firstImgContainer
                .children('img')
                .attr('src');
        }

        var clickedDataInfo = $this.data('info');

        visibleNextImg.attr('src', nextImgSrc);
        visibleNextImg.data('info', (clickedDataInfo + 1) % totalImagesCount);
        visiblePrevImg.attr('src', prevImgSrc);
        visiblePrevImg.data('info', (clickedDataInfo - 1 <= 0) ? totalImagesCount : clickedDataInfo - 1);

        $divSelected.show();
        $galleryList.addClass('blurred').addClass('disabled-background');
    }

    function onEnlargedClick() {
        $this = $(this);
        if ($this.attr('id') === 'current-image') {
            $galleryList.on('click', '.image-container img', onImagesClick);
            $selectedDiv.hide();
            $galleryList.removeClass('blurred').removeClass('disabled-background');
        } else if ($this.attr('id') === 'previous-image') {

            slideBack();
        } else if ($this.attr('id') === 'next-image') {

            slideForward();
        }
    }

    function slideForward() {
        visiblePrevImg.attr('src', visibleCurrImg.attr('src'));
        visibleCurrImg.attr('src', visibleNextImg.attr('src'));

        var rightImgInfo = visibleNextImg.data('info') + 1;
        if (rightImgInfo > 12) {
            rightImgInfo = 1;
        }

        var neededImgForNextSlot = $allImgs.filter('img[data-info=' + rightImgInfo + ']');

        visibleNextImg.attr('src', neededImgForNextSlot.attr('src'));
        visibleNextImg.data('info', rightImgInfo);

        var leftImgInfo = rightImgInfo - 2;
        if (leftImgInfo < 0) {
            leftImgInfo = totalImagesCount - 2;
        }

        visiblePrevImg.data('info', leftImgInfo);
    }

    function slideBack() {
        visibleNextImg.attr('src', visibleCurrImg.attr('src'));
        visibleCurrImg.attr('src', visiblePrevImg.attr('src'));

        var leftImgInfo = visiblePrevImg.data('info');

        (leftImgInfo > 1) ? leftImgInfo-- : leftImgInfo = totalImagesCount;

        var neededImgForPrevSlot = $allImgs.filter('img[data-info=' + leftImgInfo + ']');

        visiblePrevImg.attr('src', neededImgForPrevSlot.attr('src'));
        visiblePrevImg.data('info', leftImgInfo);

        var rightImgInfo = (leftImgInfo + 2) % totalImagesCount;

        visibleNextImg.data('info', rightImgInfo);
    }
};