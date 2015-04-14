
var $paramount = (function (me, $) {
    
    me.imageService = {
        cancelCropImage: cancelCropImage,
        cropImage: cropImage,
        getImageUrl: getImageUrl
    };

    function getImageUrl(id, height, width) {
        if (id) {

            var w = width || 100;
            var h = height || 100;

            var urlSections = me.url.manageImg.imgThumb.split('/');
            urlSections[urlSections.length - 1] = w;
            urlSections[urlSections.length - 2] = h;
            urlSections[urlSections.length - 3] = id;

            // Join back together
            return urlSections.join('/');

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

