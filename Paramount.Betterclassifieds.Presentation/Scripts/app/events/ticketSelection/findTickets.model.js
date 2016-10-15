(function ($, $paramount, ko) {
    'use strict';

    /*
     * Used for the event view details page for ticket and group selection
     */
     
    $paramount.models = $paramount.models || {};
    $paramount.models.FindTickets = function (eventService, data) {
        var me = this;

        $.extend(data, {});

        me.tickets = ko.observableArray();
        me.eventId = ko.observable(data.eventId);
        me.groupsRequired = ko.observable(data.groupsRequired);
        me.groups = ko.observableArray();

        if (data.groupsRequired === true && data.groups) {
            $.each(data.groups, function (index, item) {
                me.groups.push(new $paramount.models.EventGroup(item));
            });
        }

        $.each(data.ticketData, function (index, item) {
            me.tickets.push(new $paramount.models.EventTicket(item, data.maxTicketsPerBooking));
        });

        me.startOrder = function (element, event) {
            var $btn = $(event.target).button('loading');
            _.remove(me.tickets(), function (t) {
                return t.selectedQuantity() === undefined || t.selectedQuantity() === 0;
            });
            eventService.startTicketOrder(ko.toJSON(me)).error(function () {
                $btn.button('reset');
            });
        }

        // property used for the user interface to show/hide the submit button
        me.totalSelectedTickets = ko.computed(function () {
            var total = 0;
            $.each(me.tickets(), function (index, item) {
                if (item.selectedQuantity()) {
                    total += item.selectedQuantity();
                }
            });

            return total;
        });
    }

})(jQuery, $paramount, ko);