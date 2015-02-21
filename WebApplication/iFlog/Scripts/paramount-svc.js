
var $paramount = (function (me, $) {

    me.svc = {
        cancelCropImage: cancelCropImage,
        cropImage: cropImage,
        getImageUrl: getImageUrl,
        setLineAdImageForBooking: setLineAdImageForBooking,
        removeLineAdImageForBooking: removeLineAdImageForBooking,
    };

    function getImageUrl(id) {
        if(id){
            return me.url.imgThumb.replace('-1', id);
        }
        return null;
    }

    function cancelCropImage(documentId) {
        return post(me.url.cancelCropImage, { documentId: documentId });
    }

    function cropImage(fileName, x, y, width, height) {
        return post(me.url.cropImage, { fileName: fileName, x: x, y: y, width: width, height: height });
    }

    function setLineAdImageForBooking(documentId) {
        return post(me.url.bookingLineAdImage, { documentId: documentId });
    }

    function removeLineAdImageForBooking(documentId) {
        return post(me.url.removeLineAdImage, { documentId: documentId });
    }

    function post(url, data) {
        return $.ajax({
            url: url,
            data: JSON.stringify(data),
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json'
        });
    }

    function get(url) {
        return $.ajax({
            url: url
        });
    }

    return me;

})($paramount, jQuery);