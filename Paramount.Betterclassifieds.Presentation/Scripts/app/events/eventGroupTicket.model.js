(function ($, ko, $p) {
    function EventGroupTicket(data) {
        var me = this;
        me.eventId = ko.observable();
        me.eventTicketId = ko.observable();
        me.eventBookingTicketId = ko.observable();
        me.ticketName = ko.observable();
        me.guestFullName = ko.observable();
        me.selectedGroup = ko.observable();
        me.groups = ko.observableArray();
        
        if (data) {
            me.bind(data);
        }

        me.friendlyTicketName = ko.computed(function () {
            return me.guestFullName() + ' - ' + me.ticketName();
        });
    }
    EventGroupTicket.prototype.bind = function (data) {
        this.eventId(data.eventId);
        this.eventBookingTicketId(data.eventBookingTicketId);
        this.ticketName(data.ticketName);
        this.guestFullName(data.guestFullName);
        this.eventTicketId(data.eventTicketId);

        var me = this;
        var service = new $paramount.EventService();
        service.getGroupsForTicket(this.eventId(), this.eventTicketId())
            .then(function(resp) {
                _.each(resp, function(gr) {
                    me.groups.push(new $p.models.EventGroup(gr));
                });
            });
    }
    $p.models.EventGroupTicket = EventGroupTicket;
})(jQuery, ko, $paramount)