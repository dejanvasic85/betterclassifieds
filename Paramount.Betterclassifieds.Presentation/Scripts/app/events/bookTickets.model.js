(function ($, $paramount, ko, moment) {
    'use strict';

    $paramount.models = $paramount.models || {};

    $paramount.models.BookTickets = function (data) {
        var me = this;

        me.tickets = ko.observableArray();

        $.each(data.ticketData, function(index, item) {
            me.tickets.push(new $paramount.models.EventTicket(item));
        });
    }

})(jQuery, $paramount, ko, moment);