﻿(function ($, $paramount, ko) {

    'use strict';

    $paramount.models = $paramount.models || {};
    $paramount.models.EventTicketReserved = function (data) {
        $.extend(data, {});
        var me = this;
        me.eventTicketId = ko.observable(data.eventTicketId);
        me.eventTicketReservationId = ko.observable(data.eventTicketReservationId);
        me.ticketName = ko.observable(data.ticketName);
        me.price = ko.observable(data.price);
        me.status = ko.observable(data.status);
        me.isReserved = ko.observable(data.status.toLowerCase() === 'reserved');
        me.notReserved = ko.observable(data.status.toLowerCase() !== 'reserved');
        me.totalCostFormatted = ko.computed(function () {
            if (data.price === 0)
                return 'FREE';

            return $paramount.formatCurrency(me.price());
        });
        me.ticketTypeAndPrice = ko.computed(function () {
            var t = data.ticketName;
            if (data.price > 0) {
                t += " " + $paramount.formatCurrency(data.price);
            }
            return t;
        });
    }
})(jQuery, $paramount, ko);