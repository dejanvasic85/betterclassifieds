(function ($, $paramount, ko, moment) {
    'use strict';

    $paramount.models = $paramount.models || {};
    $paramount.models.BookTickets = function (data) {
        var me = this;

        $.extend(data, {});
        me.tickets = ko.observableArray();
        me.adId = ko.observable(data.adId);

        $.each(data.ticketData, function (index, item) {
            me.tickets.push(new $paramount.models.EventTicket(item));
        });

        me.startOrder = function () {
            $.ajax({
                url: data.startTicketOrderUrl,
                data: ko.toJSON(me),
                dataType: 'application/json',
                type : 'POST'
            }).success(function(response) {
                if (response.redirect) {
                    window.location = response.redirect;
                }
            });
        }
    }

})(jQuery, $paramount, ko, moment);