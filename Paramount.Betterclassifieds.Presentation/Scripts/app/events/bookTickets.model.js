(function ($, $paramount, ko) {
    'use strict';

    function BookTickets(data, eventService) {
        var me = this;

        $.extend(data, {});

        me.eventId = ko.observable(data.eventId);
        me.includeTransactionFee = ko.observable(data.includeTransactionFee);
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
        var groupsPromise = eventService.getGroups(data.eventId);
        $.each(data.reservations, function (idx, reservationData) {
            if (idx === 0) {
                if (data.email) {
                    reservationData.guestEmail = data.email;
                }

                var name = '';
                if (data.firstName) {
                    name = data.firstName;
                }

                if (data.lastName) {
                    name = name + ' ' + data.lastName;
                }

                reservationData.guestFullName = name;
            }

            reservationData.getGroupsPromise = groupsPromise;
            me.reservations.push(new $paramount.models.EventTicketReserved(reservationData));
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
                $btn.loadBtn();

                // Reset all the errors
                me.invalidCredentials(false);

                var request = ko.toJSON(me);
                eventService.bookTickets(request)
                    .then(function (resp) {
                        if (resp.nextUrl) {
                            return;
                        }
                        resetButton();
                    })
                    .fail(resetButton);

                function resetButton() {
                    $btn.resetBtn();
                }
            }
        }


        function getAllDynamicFields() {

            var result = [];
            var fieldObservables = _.map(me.reservations(), 'ticketFields');
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