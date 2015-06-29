
// Ad Management service
(function (me, $) {

    // Constructor
    me.adService = function (endpoints, adId) {
        // Inject endpoints for ad management and img management
        this.endpoints = endpoints || me.url.adBooking;
        this.adId = adId || null;
    };

    me.adService.prototype.updateBookingRates = function (model) {
        return post(this.endpoints.updateBookingRates, model);
    }

    me.adService.prototype.previewBookingEditions = function (firstEdition, insertions) {
        return post(this.endpoints.previewBookingEditions, {
            firstEdition: firstEdition,
            printInsertions: insertions,
        });
    }

    me.adService.prototype.removePrintImg = function (documentId) {
        return post(this.endpoints.removePrintImgUrl, this.prepareModel({ documentId: documentId }));
    }

    me.adService.prototype.assignPrintImg = function (documentId) {
        return post(this.endpoints.assignPrintImgUrl, this.prepareModel({ documentId: documentId }));
    }

    me.adService.prototype.assignOnlineImage = function (documentId) {
        return post(this.endpoints.assignOnlineImageUrl, this.prepareModel({ documentId: documentId }));
    }

    me.adService.prototype.removeOnlineAdImage = function (documentId) {
        return post(this.endpoints.removeOnlineAdImage, this.prepareModel({ documentId: documentId }));
    }

    me.adService.prototype.prepareModel = function (postModel) {
        if (this.adId !== null) {
            postModel.id = this.adId;
        }
        return postModel;
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
    
    // Return the paramount module / namespace
    return me;

})($paramount, jQuery);