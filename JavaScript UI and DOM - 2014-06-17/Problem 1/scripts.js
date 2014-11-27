function createImagesPreviewer(selector, items) {
    var container = document.querySelector(selector);
    container.style.width = '1100px';

    var frag = document.createDocumentFragment();
    var leftDiv = document.createElement('div');
    leftDiv.style.width = '800px';
    leftDiv.style.height = '550px';
    leftDiv.style.display = 'inline-block';
    leftDiv.style.overflow = 'hidden';
    leftDiv.style.cssFloat = 'left';
    frag.appendChild(leftDiv);

    var rightDiv = document.createElement('div');
    rightDiv.style.width = '250px';
    rightDiv.style.height = '550px';
    rightDiv.style.display = 'inline-block';
    rightDiv.style.cssFloat = 'right';
    rightDiv.style.overflowY = 'scroll';
    frag.appendChild(rightDiv);

    var filterDiv = document.createElement('div');
    filterDiv.style.height = '50px';
    rightDiv.appendChild(filterDiv);

    var h4heading = document.createElement('h4');
    h4heading.style.margin = '0';
    h4heading.style.textAlign = 'center';
    h4heading.innerText = 'Filter'

    var filterHeading = document.createElement('h4');
    filterHeading.style.margin = '0';
    filterHeading.style.textAlign = 'center';
    filterHeading.innerText = 'Filter';
    filterHeading.style.fontWeight = 'normal';
    filterDiv.appendChild(filterHeading);

    var filterInput = document.createElement('input');
    filterInput.type = 'text';
    filterInput.style.margin = '0';
    filterInput.style.marginLeft = '10px';
    filterInput.style.display = 'block';
    filterInput.style.width = '210px';
    filterInput.style.height = '25px';
    filterDiv.appendChild(filterInput);

    var picDivHeading = h4heading.cloneNode(true);
    picDivHeading.setAttribute('data-type', 'picDivHeading');
    picDivHeading.innerText = 'Cats';

    var picDiv = document.createElement('div');
    picDiv.setAttribute('data-type', 'picDiv');
    picDiv.appendChild(picDivHeading);
    picDiv.style.backgroundColor

    var img = document.createElement('img');
    img.setAttribute('data-type', 'rightDivPic');
    img.src = 'images/cats.jpg';
    img.style.width = '90%';
    img.style.height = '90%';
    img.style.borderRadius = '5%';
    img.style.marginLeft = '12px';
    picDiv.appendChild(img);

    var leftPicDiv = picDiv.cloneNode(true);
    leftPicDiv.style.width = '100%';
    leftPicDiv.style.height = '100%';
    leftPicDiv.firstChild.style.fontSize = '40px';
    leftPicDiv.firstChild.style.margin = '30px 0';
    leftPicDiv.lastChild.style.height = '78%';
    leftPicDiv.lastChild.style.width = '78%';
    leftPicDiv.lastChild.style.marginLeft = '75px';
    leftDiv.appendChild(leftPicDiv);

    var allCreatedPicDivs = [];

    for (var i = 0; i < items.length; i++) {
        var currItem = items[i];

        var currPicDiv = picDiv.cloneNode(true);
        var currHeading = currPicDiv.firstChild;
        var currImg = currPicDiv.lastChild;

        currHeading.innerText = currItem.title;
        currImg.src = currItem.url;

        allCreatedPicDivs.push(currPicDiv);

        rightDiv.appendChild(currPicDiv);
    }

    rightDiv.onmouseover = onRightDivMouseOver;
    rightDiv.onmouseout = onRightDivMouseOut;
    rightDiv.onclick = onRightPicDivClick;
    filterInput.onkeyup = onFilterChange;

    var lastSelectedPicDiv;

    container.appendChild(frag);

    function onRightDivMouseOver(e) {
        var targetTypeAttribute = e.target.getAttribute('data-type');
        if (targetTypeAttribute === 'picDiv' &&
            e.target !== lastSelectedPicDiv) {
            changeStylesToHovered(e.target);
        } else if ((targetTypeAttribute === 'picDivHeading' ||
                   targetTypeAttribute === 'rightDivPic') &&
                   e.target.parentElement !== lastSelectedPicDiv) {
            changeStylesToHovered(e.target.parentElement);
        }
    }

    function onRightDivMouseOut(e) {
        var targetTypeAttribute = e.target.getAttribute('data-type');
        if (targetTypeAttribute === 'picDiv' &&
            e.target !== lastSelectedPicDiv) {
            changeStylesToDefault(e.target);
        } else if ((targetTypeAttribute === 'picDivHeading' ||
                   targetTypeAttribute === 'rightDivPic') &&
                   e.target.parentElement !== lastSelectedPicDiv) {
            changeStylesToDefault(e.target.parentElement);
        }
    }

    function changeStylesToHovered(elemToChange) {
        elemToChange.style.backgroundColor = '#bbb';
    }

    function changeStylesToDefault(elemToChange) {
        elemToChange.style.backgroundColor = 'transparent';
    }

    function onRightPicDivClick(e) {
        var targetTypeAttribute = e.target.getAttribute('data-type');

        if (lastSelectedPicDiv) {
            changeStylesToDefault(lastSelectedPicDiv);
        }

        if (targetTypeAttribute === 'picDiv') {
            changeLeftDivContents(e.target);
            changeStylesToHovered(e.target);
        } else if (targetTypeAttribute === 'picDivHeading' ||
                   targetTypeAttribute === 'rightDivPic') {
            changeLeftDivContents(e.target.parentElement);
            changeStylesToHovered(e.target.parentElement);
        }
    }

    function changeLeftDivContents(changeToThisPicDiv) {
        var leftPic = leftPicDiv.lastChild;
        var leftPicHeading = leftPicDiv.firstChild;

        var heading = changeToThisPicDiv.firstChild;
        var img = changeToThisPicDiv.lastChild;

        leftPic.src = img.src;
        leftPicHeading.innerText = heading.innerText;
        lastSelectedPicDiv = changeToThisPicDiv;
    }

    function onFilterChange() {
        var currValue = filterInput.value;

        for (var i = 0; i < allCreatedPicDivs.length; i++) {
            var currDiv = allCreatedPicDivs[i];
            var currDivImg = currDiv.firstChild;

            if (currDiv.innerText.toLowerCase().indexOf(currValue) === -1) {
                currDiv.style.display = 'none';
            } else {
                currDiv.style.display = 'block';
            }
        }
    }
}