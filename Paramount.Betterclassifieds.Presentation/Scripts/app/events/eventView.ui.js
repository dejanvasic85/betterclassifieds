/*
 * jQuery UI hooks to elements for viewing details for an Event Ad
 */
(function ($, ko, $paramount) {
    'use strict';

    $paramount.ui = $paramount.ui || {};
    $paramount.ui.eventView = {
        init: function (options) {
            // On Ready
            $(function () {
                // Fetch the ticketing details
                if (options.ticketData) {
                    var ticketingInterface = document.getElementById('ticketing');

                    // Create the model
                    var ticketBookingModel = new $paramount.models.BookTickets({
                        ticketData: options.ticketData,
                        adId: options.adId,
                        startTicketOrderUrl: options.startTicketOrderUrl
                    });
                    ko.applyBindings(ticketBookingModel, ticketingInterface);
                }
            });
        }
    }

})(jQuery, ko, $paramount)