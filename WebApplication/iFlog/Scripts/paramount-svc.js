
var $paramount = (function (me, $) {
    
    me.svc = {
        cancelCropImage: cancelCropImage,
        cropImage: cropImage,
        getImageUrl: getImageUrl
    };

    function getImageUrl(id) {
        if (id) {
            return me.url.manageImg.imgThumb.replace('-1', id);
        }
        return null;
    }

    function cancelCropImage(documentId) {
        return post(me.url.manageImg.cancelCropImage, { documentId: documentId });
    }

    function cropImage(fileName, x, y, width, height) {
        return post(me.url.manageImg.cropImage, { fileName: fileName, x: x, y: y, width: width, height: height });
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

    // Return the module/namespace
    return me;

})($paramount, jQuery);

