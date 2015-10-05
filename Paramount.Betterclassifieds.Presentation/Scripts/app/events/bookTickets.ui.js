(function ($, ko, $paramount) {
    'use strict';
    var $bookTicketsView = $('#bookTicketsView');

    $paramount.ui = $paramount.ui || {};
    $paramount.ui.bookTickets = {
        init: function (options) {
            $(function () {
                var bookTickets = new $paramount.models.BookTickets(options.data);
                ko.applyBindings(bookTickets, $bookTicketsView[0]);
            });
        }
    }
}(jQuery, ko, $paramount));