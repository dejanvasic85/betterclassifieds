(function ($, ko, $p) {
    function EventGroupSelector(data) {
        var me = this;
        me.eventBookingId = ko.observable();
        me.eventBookingTickets = ko.observableArray();
        me.showGroupSelection = ko.observable();
        if (data) {
            me.bind(data);
        }
    }

    EventGroupSelector.prototype.submit = function () {
        _.each(this.eventBookingTickets(), function (t) {
            console.log(ko.toJSON(t.selectedGroup()));
        });
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