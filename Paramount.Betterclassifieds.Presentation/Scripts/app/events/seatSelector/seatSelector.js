(function ($, ko, $p, eventService) {

    function Seat(data) {
        var me = this;
        me.id = ko.observable(data.id);
        me.notAvailable = ko.observable(data.notAvailable || false);
        me.selected = ko.observable(data.selected || false);
        me.ticket = ko.observable(new $p.models.EventTicket(data.ticket));
        me.row = row;
        me.style = ko.observable({ 'background-color': data.notAvailable ? '#eee' : data.colourCode });
    }

    function Row(data) {
        var me = this;
        me.name = ko.observable(data.name);
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
            _.each(seatingResponse.tickets, function(t) {
                me.tickets.push(new $p.models.EventTicket(t));
            });

            // Todo - support different type of layouts?
            _.each(seatingResponse.rows, function(r) {
                me.rows.push(new Row(r));
            });
        }
    }

    ko.components.register('seat-selector', {
        viewModel: SeatSelector,
        template: {
            path: $p.baseUrl + 'Scripts/app/events/seatSelector/seatSelector.html'
        }
    });

})(jQuery, ko, $paramount, new $paramount.EventService());