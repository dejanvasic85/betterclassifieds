(function ($, ko, $p, eventService) {

    function Seat(data) {
        var me = this;
        me.id = ko.observable(data.id);
        me.seatNumber = ko.observable(data.seatNumber);
        me.available = ko.observable(data.available || false);
        me.selected = ko.observable(data.selected || false);
        me.ticket = ko.observable(new $p.models.EventTicket(data.eventTicket));
        me.style = ko.observable({ 'background-color': data.available === true ? data.eventTicket.colourCode : '#eee' });

    }

    function Row(data) {
        var me = this;
        me.rowName = ko.observable(data.rowName);
        me.seats = ko.observableArray();
        for (var i = 0; i < data.seats.length; i++) {
            me.seats.push(new Seat(data.seats[i], me));
        }
    }

    function SeatSelector(params) {
        var me = this;
        me.eventId = ko.observable(params.eventId);
        me.tickets = ko.observableArray();
        me.rows = ko.observableArray();

        // Load the venue details
        eventService.getEventSeating(params.eventId).then(loadSeating);

        function loadSeating(seatingResponse) {

            console.log(seatingResponse);

            _.each(seatingResponse.tickets, function (t) {
                me.tickets.push({
                    ticketNameAndPrice: t.ticketName + ' ' + $p.formatCurrency(t.price),
                    soldOut: t.remainingQuantity <= 0,
                    style: { 'background-color': t.colourCode }
                });
            });

            // Todo - support different type of layouts?
            _.each(seatingResponse.rows, function (r) {
                me.rows.push(new Row(r));
            });
        }

        me.bookSeats = function () {

        }
    }

    ko.components.register('seat-selector', {
        viewModel: SeatSelector,
        template: {
            path: $p.baseUrl + 'Scripts/app/events/seatSelector/seatSelector.html'
        }
    });

})(jQuery, ko, $paramount, new $paramount.EventService());