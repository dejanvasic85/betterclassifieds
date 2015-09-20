
// Ad Management service
(function ($, $paramount) {

    var me = this;
    
    // Constructor
    $paramount.adService = function (endpoints, adId) {
        $.extend(me, {
            endpoints: endpoints || $paramount.url.adBooking,

            // When editing an ad, we may always have to provide the ad id.
            // See AuthorizeBookingIdentity filter
            model: {
                id : adId
            }
        });
    };

    $paramount.adService.prototype.updateBookingRates = function (model) {
        return post(me.endpoints.updateBookingRates, model);
    }

    $paramount.adService.prototype.previewBookingEditions = function (firstEdition, insertions) {
        return post(me.endpoints.previewBookingEditions, {
            firstEdition: firstEdition,
            printInsertions: insertions,
        });
    }

    $paramount.adService.prototype.removePrintImg = function (documentId) {
        $.extend(me.model, {
            documentId: documentId
        });
        return post(me.endpoints.removePrintImgUrl, me.model);
    }

    $paramount.adService.prototype.removeOnlineAdImage = function (documentId) {
        $.extend(me.model, {
            documentId : documentId
        });
        return post(me.endpoints.removeOnlineAdImage, me.model);
    }

    $paramount.adService.prototype.assignPrintImg = function (documentId) {
        $.extend(me.model, {
            documentId: documentId
        });
        return post(me.endpoints.assignPrintImgUrl, me.model);
    }

    $paramount.adService.prototype.assignOnlineImage = function (documentId) {
        $.extend(me.model, {
            documentId: documentId
        });
        return post(me.endpoints.assignOnlineImageUrl, me.model);
    }

    $paramount.adService.prototype.updateEventDetails = function (event) {
        $.extend(me.model, event);
        return post(me.endpoints.updateEvent, me.model);
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
    return $paramount;

})(jQuery, $paramount);