(function (ko, $p) {

    var eventService;

    ko.components.register('view-guests', {
        template: {
            path: $paramount.baseUrl + 'Scripts/app/events/viewGuests/view-guests.html'
        },
        viewModel: function (params) {
            var me = this;
            me.guests = ko.observableArray();

            function setGuests(guests) {
                _.each(guests, function(g) {
                    me.guests.push({
                        guestFullName : g.guestFullName,
                        groupName: g.groupName
                    });
                });
            }

            eventService = new $p.EventService();
            eventService.getGuests(params.eventId).then(setGuests);
        }
    });
})(ko, $paramount);