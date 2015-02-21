
var $paramount = (function (me, $) {

    me.svc = {
        removeImage: removeImage,
        cancelCropImage: cancelCropImage,
        cropImage: cropImage,
        getImageUrl : getImageUrl
    };

    function getImageUrl(id) {
        return me.url.imgThumb.replace('-1', id);
    }

    function removeImage(documentId) {
        return post(me.url.removeImage, { documentId: documentId });
    }

    function cancelCropImage(documentId) {
        return post(me.url.cancelCropImage, { documentId: documentId });
    }

    function cropImage(documentId, x, y, width, height) {
        return post(me.url.cropImage, { documentId: documentId, x: x, y: y, width: width, height: height });
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