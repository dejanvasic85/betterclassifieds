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
                    var eventService = new $paramount.EventService(options.baseUrl);
                    var ticketBookingModel = new $paramount.models.FindTickets(eventService, {
                        ticketData: options.ticketData,
                        maxTicketsPerBooking: options.maxTicketsPerBooking,
                        eventId : options.eventId
                    });

                    ko.applyBindings(ticketBookingModel, ticketingInterface);
                }
            });
        }
    }

})(jQuery, ko, $paramount)