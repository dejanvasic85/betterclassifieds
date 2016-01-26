
// Ad Booking service
(function ($, $paramount) {

    var me = this;

    // Constructor
    function AdDesignService(adId) {
        $.extend(me, {
            // When editing an ad, we may always have to provide the ad id.
            // See AuthorizeBookingIdentity filter
            model: {
                id: adId
            },
            // If there adId is defined then we are pointing at the EditAdController
            baseUrl: _.isUndefined(adId)
                ? $paramount.baseUrl + 'Booking/'
                : $paramount.baseUrl + 'EditAd/'
        });
    };

    AdDesignService.prototype.updateBookingRates = function (model) {
        return $paramount.httpPost(me.baseUrl + 'GetRate', model);
    }

    AdDesignService.prototype.previewBookingEditions = function (firstEdition, insertions) {
        return $paramount.httpPost(me.baseUrl + 'PreviewEditions', {
            firstEdition: firstEdition,
            printInsertions: insertions
        });
    }

    AdDesignService.prototype.removePrintImg = function (documentId) {
        $.extend(me.model, {
            documentId: documentId
        });
        return $paramount.httpPost(me.baseUrl + 'RemoveLineAdImage', me.model);
    }

    AdDesignService.prototype.removeOnlineAdImage = function (documentId) {
        $.extend(me.model, {
            documentId: documentId
        });
        return $paramount.httpPost(me.baseUrl + 'RemoveOnlineImage', me.model);
    }

    AdDesignService.prototype.assignPrintImg = function (documentId) {
        $.extend(me.model, {
            documentId: documentId
        });
        return $paramount.httpPost(me.baseUrl + 'SetLineAdImage', me.model);
    }

    AdDesignService.prototype.assignOnlineImage = function (documentId, removeExisting) {
        $.extend(me.model, {
            documentId: documentId,
            removeExisting : removeExisting || false
        });
        return $paramount.httpPost(me.baseUrl + 'AssignOnlineImage', me.model);
    }
    
    AdDesignService.prototype.updateEventTicketDetails = function(eventBookingTicketSetup) {
        $.extend(me.model, eventBookingTicketSetup);
        return $paramount.httpPost(me.baseUrl + 'EventTickets', me.model);
    }

    AdDesignService.prototype.requestEventPayment = function(paymentDetails) {
        $.extend(me.model, paymentDetails);
        return $paramount.httpPost(me.baseUrl + 'EventPaymentRequest', me.model);
    }

    AdDesignService.prototype.closeEvent = function(eventId) {
        $.extend(me.model, { eventId: eventId });
        return $paramount.httpPost(me.baseUrl + 'CloseEvent', me.model);
    }

    AdDesignService.prototype.setCategoryAndPublications = function (categoryPublicationModel) {
        return $paramount.httpPost(me.baseUrl + 'Step1', categoryPublicationModel);
    }

    AdDesignService.prototype.updateEventDetails = function (event) {
        $.extend(me.model, event);
        return $paramount.httpPost(me.baseUrl + 'UpdateEventDetails', me.model);
    }

    AdDesignService.prototype.getCurrentEventDetails = function () {
        var url = me.baseUrl + 'GetEventDetails';

        if ($paramount.notNullOrUndefined(me.model.id)) {
            url += '?id=' + me.model.id;
        }

        return $.getJSON(url);
    }

    $paramount.AdDesignService = AdDesignService;
    
    return $paramount;
    
})(jQuery, $paramount);