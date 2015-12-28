(function ($, $paramount, ko) {
    'use strict';

    function BookTickets(data, eventService) {
        var me = this;

        $.extend(data, {});

        me.eventId = ko.observable(data.eventId);
        me.sendEmailToGuests = ko.observable(data.sendEmailToGuests);
        me.minsRemaining = ko.observable(data.reservationExpiryMinutes);
        me.secondsRemaining = ko.observable(data.reservationExpirySeconds);
        me.secondsRemainingDisplay = ko.computed(function () {
            return ("0" + me.secondsRemaining()).slice(-2);
        });
        me.canContinue = ko.observable(data.successfulReservationCount > 0);
        me.notAllRequestsAreFulfilled = ko.observable(data.reservations.length !== data.successfulReservationCount);

        var requiresPayment = _.some(data.reservations, function (r) {
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
        me.invalidCredentials = ko.observable(false);
        me.serverError = ko.observable(false);
        me.showPassword = ko.observable(data.isUserLoggedIn === false);

        // Tickets
        me.reservations = ko.observableArray();
        $.each(data.reservations, function (idx, reservationData) {
            if (data.isUserLoggedIn === true && idx === 0) {
                reservationData.guestFullName = data.firstName + ' ' + data.lastName;
                reservationData.guestEmail = data.email;
            }

            me.reservations.push(new $paramount.models.EventTicketReserved(reservationData, data.ticketFields));
        });

        me.totalCost = ko.computed(function () {
            return _.sum(me.reservations(), function (r) {
                if (r.notReserved()) {
                    return 0;
                }
                return r.price();
            });
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

        /*
         * Submit ticket booking
         */
        me.submitTicketBooking = function () {

            if (me.outOfTime()) {
                // Scroll to the top 
                $('html, body').animate({ scrollTop: $('.alert-count-down').offset().top }, 1000);
                return;
            }

            var fields = getAllDynamicFields();

            if ($paramount.checkValidity(me.reservations(), fields) === false) {
                return;
            };

            var $form = $('#bookTicketsForm');
            var $btn = $('#bookTicketsView button');

            if ($form.valid() === true) {
                $btn.button('loading');

                // Reset all the errors
                me.invalidCredentials(false);

                var request = ko.toJSON(me);
                eventService.bookTickets(request)
                    .then(function (response) {
                        if (response.loginFailed === true) {
                            me.invalidCredentials(true);
                            return;
                        }
                    })
                    .then(function (resp) {
                        if (resp.nextUrl) {
                            return;
                        }
                        $btn.button('reset');
                    });
            }
        }


        function getAllDynamicFields() {

            var result = [];
            var fieldObservables = _.pluck(me.reservations(), 'ticketFields');
            _.each(fieldObservables, function (f) {
                _.each(f(), function (dynamicField) {
                    result.push(dynamicField);
                });
            });
            return result;
        }
    }


    $paramount.models = $paramount.models || {};
    $paramount.models.BookTickets = BookTickets;

})(jQuery, $paramount, ko);