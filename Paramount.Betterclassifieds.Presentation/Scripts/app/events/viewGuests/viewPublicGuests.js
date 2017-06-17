(function (ko, $p) {

    var eventService = new $p.EventService();

    ko.components.register('view-public-guests', {
        template: {
            path: $paramount.baseUrl + 'Scripts/app/events/viewGuests/viewPublicGuests.html'
        },
        viewModel: function (params) {
            var me = this;
            me.guests = ko.observableArray();

            function setGuests(guests) {
                _.each(guests, function (g) {
                    me.guests.push({
                        guestName: g.guestName,
                        groupName: g.groupName
                    });
                });
            }

            me.guestListFilter = ko.observable();
            me.guestsFiltered = ko.computed(function () {
                var guestsToReturn;

                var filter = me.guestListFilter();
                if (!filter) {
                    guestsToReturn = me.guests(); // Just display all guests
                } else {
                    guestsToReturn = ko.utils.arrayFilter(me.guests(), function (g) {
                        return g.guestName.toLowerCase().indexOf(filter.toLowerCase()) > -1;
                    });
                }

                guestsToReturn = _.orderBy(guestsToReturn, ['groupName']);

                return guestsToReturn;
            });

            eventService.getGuestNames(params.eventId).then(setGuests);
        }
    });
})(ko, $paramount);