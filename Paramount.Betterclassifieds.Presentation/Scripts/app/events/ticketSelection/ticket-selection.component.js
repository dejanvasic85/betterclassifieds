(function ($, ko, $p) {

    ko.components.register('ticket-selection', {
        viewModel: function (params) {
            return params.ticketData;
        },
        template: { path: '/Scripts/app/events/ticketSelection/ticket-selection.html' }
    });



})(jQuery, ko, $paramount);

