(function (ko, $p) {

    var eventService = new $p.EventService(),
        adDesignService = new $p.AdDesignService();

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
                        groupName: g.groupName,
                        email: g.guestEmail,
                        admin: params.admin || false,
                        ticketNumberDisplay: '#' + g.ticketNumber,
                        ticketNumber: g.ticketNumber,

                        editGuest: function (model) {
                            var url = adDesignService.editGuestUrl(model.ticketNumber);
                            window.location = url;
                        }
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

            eventService.getGuests(params.eventId).then(setGuests);
        }
    });
})(ko, $paramount);