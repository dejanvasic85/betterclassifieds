/*
 * ViewEventAd.cshtml - ui hooks
 */
(function ($, ko, $paramount) {
    'use strict';

    $paramount.ui = $paramount.ui || {};
    $paramount.ui.eventView = {
        init: function (options) {
            $(function () {
                var ticketingInterface = document.getElementById('ticketing');
                ko.applyBindings({}, ticketingInterface);

                //var eventService = new $paramount.EventService();
                //var data = {
                //    groupsRequired: options.groupsRequired,
                //    ticketData: options.ticketData,
                //    maxTicketsPerBooking: options.maxTicketsPerBooking,
                //    eventId: options.eventId
                //};
                //if (options.groupsRequired === true) {
                //    eventService.getGroups(options.eventId).then(function (groups) {
                //        data.groups = groups;
                //        var ticketBookingModel = new $paramount.models.FindTickets(eventService, data);
                //        ko.applyBindings(ticketBookingModel, ticketingInterface);
                //    });
                //} else {
                //    var ticketBookingModel = new $paramount.models.FindTickets(eventService, data);
                //    ko.applyBindings(ticketBookingModel, ticketingInterface);
                //}

                if (options.floorPlanDocumentId === '') {
                    $('.floor-plan').hide();
                } else if (options.floorPlanFileName.endsWith('.pdf')) {
                    var url = $paramount.baseUrl + 'Document/File/' + options.floorPlanDocumentId;
                    $('#btnViewFloorplan')
                        .removeAttr('data-target')
                        .removeAttr('data-toggle')
                        .attr('target', '_blank')
                        .attr('href', url);
                }

                $('.ticket-booth').affix({
                    offset: {
                        top: 300
                    }
                });
            });
        }
    }

})(jQuery, ko, $paramount)