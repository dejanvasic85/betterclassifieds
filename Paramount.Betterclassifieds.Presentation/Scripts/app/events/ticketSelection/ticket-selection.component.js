(function ($, ko) {

    ko.components.register('ticket-selection', {
        viewModel: function (params) {
            return params.ticket; // This should already be an observable bound to models.TicketOption
        },
        template: { path: '/Scripts/app/events/ticketSelection/ticket-selection.html' }
    });



})(jQuery, ko);