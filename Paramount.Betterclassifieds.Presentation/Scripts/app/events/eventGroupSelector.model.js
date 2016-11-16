(function ($, ko, $p) {
    /*
     * Used for the EventBooked page after an EventBooking has been completed and groups are not required...
     */

    function EventGroupSelector(data) {
        var me = this;
        me.eventBookingId = ko.observable();
        me.eventBookingTickets = ko.observableArray();
        me.showGroupSelection = ko.observable();
        if (data) {
            me.bind(data);
        }
    }
    
    EventGroupSelector.prototype.bind = function (data) {
        var me = this;
        me.eventBookingId(data.eventBookingId);
        _.each(data.eventBookingTickets, function (t) {
            t.eventId = data.eventId;
            me.eventBookingTickets.push(new $p.models.EventGroupTicket(t));
        });

    }
    $p.models.EventGroupSelector = EventGroupSelector;
})(jQuery, ko, $paramount);