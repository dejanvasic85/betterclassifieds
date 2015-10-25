
var $paramount = (function (me, $) {

    function ImageService(baseUrl) {
        this.baseUrl = baseUrl || me.baseUrl;
    }

    ImageService.prototype.getImageUrl = function (id, dimensions) {
        // Default to the thumb size
        if (id === 'undefined' || id === '') {
            return null;
        }
        dimensions = dimensions || {};
        _.defaults(dimensions, { h: 100, w: 100 });
        return me.baseUrl + 'img/' + id + '/' + dimensions.h + '/' + dimensions.w;
    }

    ImageService.prototype.cancelCropImage = function (filename) {
        return me.httpPost(this.baseUrl + 'Image/CancelCrop', JSON.stringify({ filename: filename }));
    }

    ImageService.prototype.cropImage = function (fileName, x, y, width, height) {
        return me.httpPost(this.baseUrl + 'Image/CropImage', JSON.stringify({ fileName: fileName, x: x, y: y, width: width, height: height }));
    }

    ImageService.prototype.uploadCropImageUrl = function() {
        return this.baseUrl + 'Image/UploadCropImage';
    }

    ImageService.prototype.renderCropImageUrl = function (filename) {
        return this.baseUrl + 'Image/RenderCropImage?fileName=' + filename;
    }

    me.ImageService = ImageService;

    // Return the module/namespace
    return me;

})($paramount, jQuery);

