(function ($, $p, ko) {
    'use strict';

    /*
     * Used for the event view details page for ticket and group selection
     */

    var $ticketsModal = $('#ticketSelectionModal');

    function FindTickets(eventService, data) {
        var me = this;

        _.defaults(data, {});

        me.availableTickets = ko.observableArray();
        me.eventInvitationId = ko.observable(data.eventInvitationId);
        me.groupsRequired = ko.observable(data.groupsRequired);
        me.groups = ko.observableArray();
        me.eventService = eventService;
        me.groupSelectionEnabled = data.groupsRequired && !_.isUndefined(data.groups) && data.groups.length > 0;
        me.maxTicketsPerBooking = data.maxTicketsPerBooking;
        me.selectedGroupId = null;
        me.selectedTickets = ko.observableArray();
        me.displayNoSelectedTickets = ko.observable(false);

        // This maps to the EventTicketReservedViewModel
        me.reservationData = {
            eventId: data.eventId,
            eventInvitationId: data.invitationId,
            tickets: []
        };

        if (data.groupsRequired === true && data.groups) {
            $.each(data.groups, function (index, item) {
                me.groups.push(new $p.models.EventGroup(item));
            });
        } else {
            $.each(data.ticketData, function (index, item) {
                me.availableTickets.push(new $p.models.EventTicket(item, me.maxTicketsPerBooking));
            });
        }

        me.startOrder = function (element, event) {
            if (me.selectedTickets().length > 0) {
                me.reservationData.tickets = ko.toJS(me.selectedTickets());
                var $btn = $(event.target).loadBtn();
                eventService.startTicketOrder(me.reservationData).fail(function () {
                    $btn.resetBtn();
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
        me.onGroupSelect = function (model, el) {
            var $btn = $(el.target).loadBtn();

            me.selectedGroupId = model.eventGroupId();
            me.availableTickets.removeAll();

            me.eventService.getTicketsForGroup(me.reservationData.eventId, model.eventGroupId())
                .then(function (resp) {
                    _.each(resp, function (t) {
                        var maxTicketsAllowed = getMaxTicketsAllowed(me.selectedGroupId, t.eventTicketId, model.maxGuests(), t.remainingQuantity);
                        var eventTicket = new $p.models.EventTicket(t, maxTicketsAllowed);
                        eventTicket.eventGroupId = ko.observable(model.eventGroupId());
                        me.availableTickets.push(eventTicket);
                    });

                    $ticketsModal.modal('show');
                })
                .always(function () {
                    $btn.resetBtn();
                });
        }

        me.removeSelectedTicket = function (model) {
            me.selectedTickets.remove(this);
        }

        /*
         * User selected to save tickets and continue
         */
        me.onGroupTicketSave = function () {
            saveSelectedTickets();
            $ticketsModal.modal('hide');
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

            var maxTicketsAllowed = me.maxTicketsPerBooking; // Set the default max value first.
            if (!_.isNull(groupMaxGuests) && groupMaxGuests < maxTicketsAllowed) {
                maxTicketsAllowed = groupMaxGuests;
            }

            if (!_.isNull(ticketRemainingQuantity) && ticketRemainingQuantity < maxTicketsAllowed) {
                maxTicketsAllowed = ticketRemainingQuantity;
            }

            maxTicketsAllowed = maxTicketsAllowed - currentSelectedCount;

            return maxTicketsAllowed;
        }
    }

    $p.models = $p.models || {};
    $p.models.FindTickets = FindTickets;

})(jQuery, $paramount, ko);