(function ($, ko, $p) {

    var eventService;

    ko.components.register('find-tickets', {
        viewModel: Tickets,
        template: { path: '/Scripts/app/events/ticketSelection/find-tickets.html' }
    });

    function Tickets(params) {
        var me = this;


        eventService = new $p.EventService(params.baseUrl);

        me.availableTickets = ko.observableArray();
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
            eventInvitationId: params.eventInvitationId,
            tickets: []
        };

        me.startOrder = function (model, event) {
            if (me.selectedTickets().length > 0) {
                me.reservationData.tickets = ko.toJS(me.selectedTickets());
                var $btn = $(event.target).loadBtn();
                eventService.startTicketOrder(me.reservationData).fail(function () {
                    $btn.resetBtn();
                }).then(function(resp) {
                    if (resp.errors) {
                        $btn.resetBtn();
                    }
                });
            } else {
                me.displayNoSelectedTickets(true);
            }
        }

        me.saveAndOrder = function (element, event) {
            saveSelectedTickets();
            me.startOrder(element, event);
        };

        me.allowToOrderTickets = ko.computed(function () {
            if (me.groupSelectionEnabled === true) {
                return true;
            }
            return me.selectedTickets().length > 0;
        });

        /*
         * Used for the findTickets.model.js page
         */
        me.onGroupSelect = function (model, event) {
            var $btn = $(event.target).loadBtn();

            me.selectedGroupId = model.eventGroupId();
            me.availableTickets.removeAll();

            eventService.getTicketsForGroup(me.reservationData.eventId, model.eventGroupId())
                .then(function (resp) {
                    _.each(resp, function (t) {
                        var maxTicketsAllowed = getMaxTicketsAllowed(me.selectedGroupId, t.eventTicketId, model.maxGuests(), t.remainingQuantity);
                        var eventTicket = new $p.models.EventTicket(t, maxTicketsAllowed);
                        eventTicket.eventGroupId = ko.observable(model.eventGroupId());
                        me.availableTickets.push(eventTicket);
                    });

                    $('#ticketSelectionModal').modal('show');
                })
                .always(function () {
                    $btn.resetBtn();
                });
        }

        me.removeSelectedTicket = function () {
            me.selectedTickets.remove(this);
        }

        me.onGroupTicketSave = function () {
            saveSelectedTickets();
            $('#ticketSelectionModal').modal('hide');
        }

        me.onGroupTicketSaveAndOrder = function (model, event) {
            saveSelectedTickets();
            me.startOrder(model, event);
        }

        function saveSelectedTickets() {
            _.each(me.availableTickets(), function (t) {
                if (t.selectedQuantity() > 0) {
                    me.selectedTickets.push(t);
                }
            });
        }

        function getMaxTicketsAllowed(eventGroupId, eventTicketId, groupMaxGuests, ticketRemainingQuantity) {
            var currentSelectedCount = _.sumBy(me.selectedTickets(), function (t) {
                if (t.eventGroupId() === eventGroupId && t.eventTicketId() === eventTicketId) {
                    return t.selectedQuantity();
                }
                return 0;
            });

            var maxTicketsAllowed = me.maxTicketsPerBooking; // Align by the maximum first
            if (!_.isNull(groupMaxGuests) && groupMaxGuests < maxTicketsAllowed) {
                maxTicketsAllowed = groupMaxGuests;
            }

            if (!_.isNull(ticketRemainingQuantity) && ticketRemainingQuantity < maxTicketsAllowed) {
                maxTicketsAllowed = ticketRemainingQuantity;
            }

            maxTicketsAllowed = maxTicketsAllowed - currentSelectedCount;

            return maxTicketsAllowed;
        }

        /*
         * Databind data from the server
         */
        this.load(params, function () {
            // Todo - toggle loader
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
                    _.each(ticketData, function (t) {
                        me.availableTickets.push(new $p.models.EventTicket(t, me.maxTicketsPerBooking));
                    });
                    done();
                });
            }
        });
    }

})(jQuery, ko, $paramount);