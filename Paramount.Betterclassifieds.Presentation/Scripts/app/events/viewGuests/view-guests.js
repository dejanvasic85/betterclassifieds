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
                _.each(guests, function (g) {
                    me.guests.push({
                        guestFullName: g.guestFullName,
                        groupName: g.groupName
                    });
                });
            }

            me.guestListFilter = ko.observable();
            me.guestsFiltered = ko.computed(function () {
                var guestsToReturn;

                var filter = me.guestListFilter();
                if (!filter) {
                    guestsToReturn = me.guests();
                } else {
                    guestsToReturn = ko.utils.arrayFilter(me.guests(), function (g) {
                        return g.guestFullName.toLowerCase().indexOf(filter.toLowerCase()) > -1;
                    });
                }

                return _.orderBy(guestsToReturn, ['groupName']);
            });

            eventService = new $p.EventService();
            eventService.getGuests(params.eventId).then(setGuests);
        }
    });
})(ko, $paramount);