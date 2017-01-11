(function (ko) {
    ko.components.register('ticket-options', {
        viewModel : function(params) {
            this.tickets = params.ticketsObservable;
        },
        template: { path: $paramount.baseUrl + 'Scripts/app/events/ticketSelection/ticket-options.html' }
    });
})(ko);