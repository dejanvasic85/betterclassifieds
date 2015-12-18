
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

    AdDesignService.prototype.assignOnlineImage = function (documentId) {
        $.extend(me.model, {
            documentId: documentId
        });
        return $paramount.httpPost(me.baseUrl + 'AssignOnlineImage', me.model);
    }

    AdDesignService.prototype.updateEventDetails = function (event) {
        $.extend(me.model, event);
        return $paramount.httpPost(me.baseUrl + 'UpdateEventDetails', me.model);
    }

    AdDesignService.prototype.updateEventTicketDetails = function(eventBookingTicketSetup) {
        $.extend(me.model, eventBookingTicketSetup);
        var promise = $paramount.httpPost(me.baseUrl + 'EventTickets', me.model);
        handleResponse(promise);
        return promise;
    }

    AdDesignService.prototype.requestEventPayment = function(paymentDetails) {
        $.extend(me.model, paymentDetails);
        var promise = $paramount.httpPost(me.baseUrl + 'EventPaymentRequest', me.model);
        handleResponse(promise);
        return promise;
    }

    AdDesignService.prototype.closeEvent = function(eventId) {
        $.extend(me.model, { eventId: eventId });
        var promise = $paramount.httpPost(me.baseUrl + 'CloseEvent', me.model);
        handleResponse(promise);
        return promise;
    }

    AdDesignService.prototype.setCategoryAndPublications = function (categoryPublicationModel) {
        return $paramount.httpPost(me.baseUrl + 'Step1', categoryPublicationModel);
    }

    AdDesignService.prototype.getCurrentEventDetails = function() {
        return $.getJSON(me.baseUrl + 'GetEventDetails');
    }

    $paramount.AdDesignService = AdDesignService;
    // Return the paramount module / namespace
    return $paramount;

    function handleResponse(promise) {
        promise.success(function (response) {
            if (response.nextUrl) {
                window.location = response.nextUrl;
            }
        });
    }

})(jQuery, $paramount);