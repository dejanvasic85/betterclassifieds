(function ($, $paramount, ko, moment) {
    'use strict';

    $paramount.models = $paramount.models || {};
    
    $paramount.models.EventTicket = function (data) {
        debugger;
        var me = this;

        me.ticketName = ko.observable(data.TicketName);
        me.availableQuantity = ko.observable(data.AvailableQuantity);
        me.price = ko.observable(data.Price);
        me.selectedQuantity = ko.observable();
    }


})(jQuery, $paramount, ko, moment);