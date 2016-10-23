(function ($, ko, $p) {

    var $ticketsModal = $('#ticketSelectionModal');
    var eventService;

    ko.components.register('tickets', {
        viewModel: Tickets,
        template: { path: '/Scripts/app/events/ticketSelection/tickets.html' }
    });

    function Tickets(params) {
        var me = this;

        eventService = new $p.EventService(params.baseUrl);

        me.availableTickets = ko.observableArray();
        me.eventInvitationId = ko.observable(); // Todo - bind it from params for invitation page
        me.groupsRequired = ko.observable();
        me.groups = ko.observableArray();
        me.groupSelectionEnabled = false;
        me.maxTicketsPerBooking = params.maxTicketsPerBooking;
        me.selectedGroupId = null;
        me.selectedTickets = ko.observableArray();
        me.displayNoSelectedTickets = ko.observable(false);

        // This maps to the EventTicketReservedViewModel
        me.reservationData = {
            eventId: params.eventId,
            eventInvitationId: params.invitationId,
            tickets: []
        };

        this.load(params, function() {
            console.log('done');
        });
    }

    Tickets.prototype.load = function (params, done) {
        var me = this;

        eventService.getEvent(params.eventId).then(function (eventData) {
            me.groupsRequired(eventData.groupsRequired);

            if (eventData.groupsRequired === true) {
                eventService.getGroups(params.eventId).then(function (groupData) {
                    _.each(groupData, function (gr) {
                        me.groups.push(new $p.models.EventGroup(gr));
                    });
                    me.groupSelectionEnabled = eventData.groupsRequired && groupData.length > 0;
                    done();
                });
            } else {
                eventService.getTicketsForEvent(params.eventId).then(function (ticketData) {
                    console.log(ticketData);
                    done();
                });
            }
        });
    }

})(jQuery, ko, $paramount);