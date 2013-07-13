$(document).ready(function () {

    var iFrames = $('.frameWithBody');

    function iResize() {
        for (var i = 0, j = iFrames.length; i < j; i++) {
            iFrames[i].style.height = iFrames[i].contentWindow.document.body.offsetHeight + 20 + 'px';
        }
    }

    $('iframe').load(function () {
        setTimeout(iResize, 0);
        //open all links in external window
        $('.frameWithBody').contents().find("a").attr("target", "_blank");
    });

    for (var i = 0, j = iFrames.length; i < j; i++) {
        var iSource = iFrames[i].src;
        iFrames[i].src = '';
        iFrames[i].src = iSource;
    }
});