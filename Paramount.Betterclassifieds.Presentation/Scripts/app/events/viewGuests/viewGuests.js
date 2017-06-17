(function (ko, $p) {

    var eventService = new $p.EventService(),
        adDesignService = new $p.AdDesignService();

    ko.components.register('view-guests', {
        template: {
            path: $paramount.baseUrl + 'Scripts/app/events/viewGuests/viewGuests.html'
        },
        viewModel: function (params) {
            var me = this;
            me.guests = ko.observableArray();
            me.admin = params.admin || false;

            function setGuests(guests) {
                _.each(guests, function (g) {
                    me.guests.push({

                        guestFullName: g.guestFullName,
                        groupName: g.groupName,
                        email: g.guestEmail,
                        admin: me.admin,
                        ticketNumberDisplay: '#' + g.ticketNumber,
                        ticketNumber: g.ticketNumber,
                        isPublic: g.isPublic,

                        editGuest: function (model) {
                            if (!me.admin) {
                                return;
                            }
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
                    guestsToReturn = me.guests(); // Just display all guests
                } else {
                    guestsToReturn = ko.utils.arrayFilter(me.guests(), function (g) {
                        return g.guestFullName.toLowerCase().indexOf(filter.toLowerCase()) > -1;
                    });
                }

                guestsToReturn = _.orderBy(guestsToReturn, ['groupName']);

                if (!me.admin) {
                    // Filter the guests where the isPublic is false
                    guestsToReturn = _.filter(guestsToReturn, { 'isPublic': true });
                }

                return guestsToReturn;
            });

            eventService.getGuests(params.eventId).then(setGuests);
        }
    });
})(ko, $paramount);