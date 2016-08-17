(function ($, ko, $p) {
    'use strict';

    function EventBookingTicket(data) {
        var me = this,
            eventService = new $paramount.AdDesignService(data.adId);
        console.log(eventService);
        me.eventBookingTicketId = ko.observable();
        me.eventBookingId = ko.observable();
        me.eventTicketId = ko.observable();
        me.guestFullName = ko.observable();
        me.guestEmail = ko.observable();

        /*
         * Validation
         */
        me.validator = ko.validatedObservable({
            guestFullName: me.guestFullName.extend({ required: true }),
            guestEmail: me.guestEmail.extend({ required: true, email: true })
        });

        me.save = function (vm, event) {
            if ($p.checkValidity(me) === false) {
                return;
            }


            var $btn = $(event.target);
            $btn.button('loading');

            // Update the guest
            eventService.editGuest(ko.toJS(me))
                .then(function () {
                    $btn.button('reset');
                });
        }

        if (data) {
            me.bind(data);
        }
    }

    EventBookingTicket.prototype.bind = function (data) {
        var me = this;
        me.eventBookingTicketId(data.eventBookingTicketId);
        me.eventBookingId(data.eventBookingId);
        me.eventTicketId(data.eventTicketId);
        me.guestFullName(data.guestFullName);
        me.guestEmail(data.guestEmail);
    }
    $p.models.EventBookngTicket = EventBookingTicket;
})(jQuery, ko, $paramount);