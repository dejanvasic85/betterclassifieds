(function ($, ko) {

    ko.components.register('ticket-option', {
        viewModel: function (params) {
            return params.ticket; // This should already be an observable bound to models.TicketOption
        },
        template: { path: '/Scripts/app/events/ticketSelection/ticket-option.html' }
    });



})(jQuery, ko);