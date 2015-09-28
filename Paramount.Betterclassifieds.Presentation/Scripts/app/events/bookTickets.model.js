(function ($, $paramount, ko, moment) {
    'use strict';

    $paramount.models = $paramount.models || {};
    $paramount.models.BookTickets = function (eventService, data) {
        var me = this;

        $.extend(data, {});
        me.tickets = ko.observableArray();
        me.adId = ko.observable(data.adId);
        me.totalSelectedTickets = ko.computed(function () {
            var total = 0;
            $.each(me.tickets(), function (index, item) {
                total += item.selectedQuantity();
            });
            return total;
        });

        $.each(data.ticketData, function (index, item) {
            me.tickets.push(new $paramount.models.EventTicket(item));
        });

        me.startOrder = function (element, event) {
            var $btn = $(event.target);
            $btn.button('loading');

            eventService.startTicketOrder(ko.toJSON(me)).success(function (response) {
                if (response.redirect) {
                    //window.location = response.redirect;
                }
            });
        }
    }

})(jQuery, $paramount, ko, moment);