
var $paramount = (function (me, $) {

    function ImageService(baseUrl) {
        this.baseUrl = baseUrl || me.baseUrl;
    }

    ImageService.prototype.getImageUrl = function (id, dimensions) {
        // Default to the thumb size
        if (_.isUndefined(id) || _.isEmpty(id)) {
            return null;
        }
        dimensions = dimensions || {};
        _.defaults(dimensions, { h: 100, w: 100 });
        return me.baseUrl + 'img/' + id + '/' + dimensions.w + '/' + dimensions.h;
    }

    ImageService.prototype.cancelCropImage = function (filename) {
        return me.httpPost(this.baseUrl + 'Image/CancelCrop', { filename: filename });
    }

    ImageService.prototype.cropImage = function (fileName, x, y, width, height, resizeForPrint) {
        return me.httpPost(this.baseUrl + 'Image/CropImage', { fileName: fileName, x: x, y: y, width: width, height: height, resizeForPrint: resizeForPrint });
    }

    ImageService.prototype.uploadCropImageUrl = function () {
        return this.baseUrl + 'Image/UploadCropImage';
    }

    ImageService.prototype.renderCropImageUrl = function (filename) {
        return this.baseUrl + 'Image/RenderCropImage?fileName=' + filename;
    }

    ImageService.prototype.getUploadOnlineImageUrl = function () {
        return this.baseUrl + 'Image/UploadOnlineImage';
    }

    ImageService.prototype.getUploadEventFloorplanUrl = function () {
        return this.baseUrl + 'Image/UploadEventFloorplanUrl';
    }

    me.ImageService = ImageService;

    // Return the module/namespace
    return me;

})($paramount, jQuery);

