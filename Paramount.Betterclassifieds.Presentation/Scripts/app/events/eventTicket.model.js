(function ($, $paramount, ko, moment) {
    'use strict';

    $paramount.models = $paramount.models || {};
    
    $paramount.models.EventTicket = function (data) {
        this.ticketName = ko.observable(data.TicketName);
        this.availableQuantity = ko.observable(data.AvailableQuantity);
        this.price = ko.observable(data.Price);
    }


})(jQuery, $paramount, ko, moment);