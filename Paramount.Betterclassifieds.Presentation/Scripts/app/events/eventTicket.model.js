(function ($, $paramount, ko) {
    'use strict';

    $paramount.models = $paramount.models || {};
    $paramount.models.EventTicket = function (data, maxTicketsPerBooking) {
        $.extend(data, {});
        var me = this;

        me.eventId = ko.observable(data.eventId);
        me.eventTicketId = ko.observable(data.eventTicketId);
        me.ticketName = ko.observable(data.ticketName);
        me.availableQuantity = ko.observable(data.availableQuantity);
        me.price = ko.observable(data.price);
        me.priceFormatted = ko.computed(function () {
            return $paramount.formatCurrency(me.price());
        });
        me.selectedQuantity = ko.observable(data.selectedQuantity);
        me.remainingQuantity = ko.observable(data.remainingQuantity);
        me.soldOut = ko.observable(data.remainingQuantity === 0);
        me.isAvailable = ko.observable(data.remainingQuantity > 0);


        // MaxTickets Per booking setup
        if (data.remainingQuantity < maxTicketsPerBooking) {
            maxTicketsPerBooking = data.remainingQuantity;
        }
        me.maxTicketsPerBooking = ko.observableArray();
        for (var i = 0; i <= maxTicketsPerBooking; i++) {
            me.maxTicketsPerBooking.push({ label: i, value: i });
        }
    }
})(jQuery, $paramount, ko);