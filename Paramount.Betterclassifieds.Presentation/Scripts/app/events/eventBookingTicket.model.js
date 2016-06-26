(function ($, ko, $p) {
    function EventBookingTicket(data) {
        var me = this;
        me.eventBookingId = ko.observable();
        me.eventTicketId = ko.observable();
        me.totalPrice = ko.observable();
        me.guestFullName = ko.observable();
        me.guestEmail = ko.observable();
        me.ticketName = ko.observable();
        
        if (data) {
            me.bind(data);
        }
    }

    EventBookingTicket.prototype.bind = function (data) {
        var me = this;
        me.eventBookingId(data.eventBookingId);
        me.eventTicketId(data.eventTicketId);
        me.totalPrice(data.totalPrice);
        me.guestFullName(data.guestFullName);
        me.guestEmail(data.guestEmail);
        me.ticketName(data.ticketName);
    }
    $p.models.EventBookngTicket = EventBookingTicket;
})(jQuery, ko, $paramount);