(function ($, $paramount, ko) {
    'use strict';

    $paramount.models = $paramount.models || {};
    $paramount.models.EventTicketReserved = function (data) {
        $.extend(data, {});
        var me = this;
        me.ticketName = ko.observable(data.ticketName);
        me.quantity = ko.observable(data.quantity);
        me.price = ko.observable(data.price);
        me.status = ko.observable(data.status);
    }
})(jQuery, $paramount, ko);