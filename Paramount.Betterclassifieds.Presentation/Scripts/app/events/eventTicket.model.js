(function ($, $paramount, ko) {
    'use strict';

    $paramount.models = $paramount.models || {};
    $paramount.models.EventTicket = function (data) {
        $.extend(data, {});
        var me = this;
        me.eventId = ko.observable(data.eventId);
        me.eventTicketId = ko.observable(data.eventTicketId);
        me.ticketName = ko.observable(data.ticketName);
        me.availableQuantity = ko.observable(data.availableQuantity);
        me.price = ko.observable(data.price);
        me.priceFormatted = ko.computed(function() {
            return $paramount.formatCurrency(me.price());
        });
        me.selectedQuantity = ko.observable(data.selectedQuantity);
        me.remainingQuantity = ko.observable(data.remainingQuantity);
    }
})(jQuery, $paramount, ko);