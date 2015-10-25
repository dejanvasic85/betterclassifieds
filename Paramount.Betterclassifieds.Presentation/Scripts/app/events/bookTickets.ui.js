(function ($, ko, $paramount) {
    'use strict';
    var $bookTicketsView = $('#bookTicketsView');

    $paramount.ui = $paramount.ui || {};
    $paramount.ui.bookTickets = {
        init: function (options) {
            $(function () {
                var eventService = new $paramount.EventService();
                var bookTickets = new $paramount.models.BookTickets(options.data, eventService);
                ko.applyBindings(bookTickets, $bookTicketsView[0]);
            });
        }
    }
}(jQuery, ko, $paramount));