(function ($, $paramount, ko) {
    'use strict';

    $paramount.models = $paramount.models || {};
    $paramount.models.EventTicket = function (data) {
        $.extend(data, {});
        var me = this;
        me.ticketName = ko.observable(data.ticketName);
        me.availableQuantity = ko.observable(data.availableQuantity);
        me.price = ko.observable(data.price);
        me.priceFormatted = ko.computed(function() {
            return $paramount.formatCurrency(me.price());
        });
        me.selectedQuantity = ko.observable(data.selectedQuantity);
    }
})(jQuery, $paramount, ko);