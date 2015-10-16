(function ($, $paramount, ko) {
    'use strict';

    $paramount.models = $paramount.models || {};
    $paramount.models.FindTickets = function (eventService, data) {
        var me = this;

        $.extend(data, {});
        
        me.tickets = ko.observableArray();
        
        $.each(data.ticketData, function (index, item) {
            me.tickets.push(new $paramount.models.EventTicket(item, data.maxTicketsPerBooking));
        });

        me.startOrder = function (element, event) {
            var $btn = $(event.target);
            $btn.button('loading');

            _.remove(me.tickets(), function (t) {
                return t.selectedQuantity() === undefined || t.selectedQuantity() === 0;
            });

            var request = ko.toJSON(me);

            eventService.startTicketOrder(request).success(function (response) {
                if (response.redirect) {
                    window.location = response.redirect;
                }
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