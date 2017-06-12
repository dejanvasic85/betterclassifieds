(function ($, ko, $p, eventService, notifier) {

    function Seat(data) {
        var me = this;
        me.id = ko.observable(data.id);
        me.seatNumber = ko.observable(data.seatNumber);
        me.available = ko.observable(data.available || false);
        me.selected = ko.observable(data.selected || false);
        me.ticketName = data.eventTicket.ticketName;
        me.price = $p.formatCurrency(data.eventTicket.price);
        me.ticket = new $p.models.EventTicket(data.eventTicket);
        me.style = ko.observable({ 'background-color': data.available === true ? data.eventTicket.colourCode : '#eee' });
        me.tooltip = ko.computed(function () {
            if (me.available() === true) {
                return me.seatNumber();
            }
            return 'Unavailable - ' + me.seatNumber();
        });
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
        me.selectedSeats = ko.observableArray();
        me.loading = ko.observable(true);

        me.openingDate = ko.observable();
        me.ticketingNotOpened = ko.observable(false);
        me.ticketingClosed = ko.observable(false);
        me.ticketingOpened = ko.observable(false);

        if (params.openingDate) {
            var opening = moment.utc(params.openingDate).local();
            me.openingDate(opening);
            me.ticketingNotOpened(opening.isAfter(moment()));
        }

        if (params.closingDate) {
            var closing = moment.utc(params.closingDate).local();
            me.ticketingClosed(closing.isBefore(moment()));
        }

        if (params.eventEndDate) {
            me.ticketingClosed(moment.utc(params.eventEndDate).local().isBefore(moment()));
        }

        me.ticketingOpened(!me.ticketingNotOpened() && !me.ticketingClosed());

        me.selectSeat = function (seat) {
            if (seat.available() === false) {
                return;
            }

            if (_.includes(me.selectedSeats(), seat)) {
                me.selectedSeats.remove(seat);
            } else {

                if (me.selectedSeats().length >= params.maxSeats) {
                    notifier.info('You have reached your maximum selection of ' + params.maxSeats + ' seats.');
                    return;
                }

                me.selectedSeats.push(seat);
            }

            seat.selected(!seat.selected());
        }

        me.removeTicket = function (seat) {
            me.selectedSeats.remove(seat);
            seat.selected(false);
        }

        me.bookSeats = function (model, event) {
            var $btn = $(event.target);
            $btn.loadBtn();

            if (me.selectedSeats().length > 0) {
                var tickets = [];
                _.each(me.selectedSeats(), function (s) {
                    var ticketData = ko.toJS(s.ticket);
                    ticketData.seatNumber = s.seatNumber();
                    ticketData.selectedQuantity = 1;
                    tickets.push(ticketData);
                });

                var order = {
                    eventId: me.eventId(),
                    tickets: tickets
                }

                eventService.startTicketOrder(order);
            }
        }

        me.seatsRenderComplete = function () {
            me.loading(false);
        }

        // Load the seating
        if (me.ticketingOpened()) {

            eventService.getEventSeating(params.eventId, params.orderRequestId).then(loadSeating);

            function loadSeating(seatingResponse) {

                _.each(seatingResponse.tickets, function (t) {
                    me.tickets.push({
                        ticketNameAndPrice: t.ticketName + ' ' + $p.formatCurrency(t.price),
                        soldOut: t.remainingQuantity <= 0,
                        style: { 'background-color': t.colourCode }
                    });
                });

                // Todo - support different type of layouts? how ???
                _.each(seatingResponse.rows, function (r) {
                    me.rows.push(new Row(r));
                });
            }
        }

    }

    ko.components.register('seat-selector', {
        viewModel: SeatSelector,
        template: {
            path: $p.baseUrl + 'Scripts/app/events/seatSelector/seatSelector.html'
        }
    });

})(jQuery, ko, $paramount, new $paramount.EventService(), toastr);