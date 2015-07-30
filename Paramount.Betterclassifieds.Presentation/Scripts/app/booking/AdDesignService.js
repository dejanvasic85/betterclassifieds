
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
        return post(this.endpoints.removePrintImgUrl, prepareModel({ documentId: documentId }));
    }

    me.adService.prototype.assignPrintImg = function (documentId) {
        return post(this.endpoints.assignPrintImgUrl, prepareModel({ documentId: documentId }));
    }

    me.adService.prototype.assignOnlineImage = function (documentId) {
        return post(this.endpoints.assignOnlineImageUrl, prepareModel({ documentId: documentId }));
    }

    me.adService.prototype.removeOnlineAdImage = function (documentId) {
        return post(this.endpoints.removeOnlineAdImage, prepareModel({ documentId: documentId }));
    }

    me.adService.prototype.updateEventDetails = function (event) {
        return post(this.endpoints.updateEvent, prepareModel(event));
    }
    
    function prepareModel(postModel) {
        if (this.adId) {
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