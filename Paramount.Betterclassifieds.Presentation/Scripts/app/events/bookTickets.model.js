(function ($, $paramount, ko) {
    'use strict';

    $paramount.models = $paramount.models || {};
    $paramount.models.BookTickets = function (data) {
        var me = this;

        $.extend(data, {});

        me.minsRemaining = ko.observable(data.reservationExpiryMinutes);
        me.secondsRemaining = ko.observable(data.reservationExpirySeconds);
        me.secondsRemainingDisplay = ko.computed(function () {
            return ("0" + me.secondsRemaining()).slice(-2);
        });
        me.canContinue = ko.observable(data.successfulReservationCount > 0);
        me.notAllRequestsAreFulfilled = ko.observable(data.reservations.length !== data.successfulReservationCount);
        var requiresPayment = _.some(data.reservations, function(r) {
            return r.price > 0;
        });
        me.requiresPayment = ko.observable(requiresPayment);

        // User details
        me.firstName = ko.observable(data.firstName);
        me.lastName = ko.observable(data.lastName);
        me.phone = ko.observable(data.phone);
        me.postCode = ko.observable(data.postCode);
        me.email = ko.observable(data.email);
        me.password = ko.observable();
        me.showPassword = ko.observable(data.isUserLoggedIn === false);

        // Tickets
        me.reservedTickets = ko.observableArray();
        $.each(data.reservations, function (idx, reservationData) {
            me.reservedTickets.push(new $paramount.models.EventTicketReserved(reservationData));
        });
        
        // Timer
        if (data.outOfTime !== true && data.successfulReservationCount > 0) {
            var interval = setInterval(function () {
                if (me.minsRemaining() === 0 && me.secondsRemaining() === 1) {
                    window.clearInterval(interval);
                }

                if (me.secondsRemaining() > 0) {
                    var updatedSecs = me.secondsRemaining() - 1;
                    me.secondsRemaining(updatedSecs);
                } else {
                    var updatedMins = me.minsRemaining() - 1;
                    me.minsRemaining(updatedMins);
                    me.secondsRemaining(59);
                }
            }, 1000);
        }
        
        // Time checking
        me.outOfTime = ko.computed(function () {
            if (data.outOfTime) {
                return true;
            }

            return me.minsRemaining() === 0 && me.secondsRemaining() === 0;
        });


        // Submit
        me.submitTicketBooking = function() {
            var $form = $('#bookTicketsUserDetailsForm');
            var $btn = $('#bookTicketsView button');

            if ($form.valid()) {
                $btn.button('loading');
            }
        }
    }

})(jQuery, $paramount, ko);