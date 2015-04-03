
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
            adId : this.adId
        });
    }

    me.adService.prototype.removeLineAdImageForBooking = function (documentId) {
        return post(this.endpoints.removeLineAdImage, { documentId: documentId, adId : this.adId });
    }

    me.adService.prototype.setLineAdImageForBooking = function (documentId) {
        return post(this.endpoints.bookingLineAdImage, { documentId: documentId, adId : this.adId });
    }

    me.adService.prototype.assignOnlineImage = function (documentId) {
        debugger;
        return post(this.endpoints.assignOnlineImageUrl, { documentId: documentId, adId: this.adId });
    }

    me.adService.prototype.removeOnlineAdImage = function (documentId) {
        return post(this.endpoints.removeOnlineAdImage, { documentId: documentId, adId: this.adId });
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