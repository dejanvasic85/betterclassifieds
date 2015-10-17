﻿(function ($, $paramount, ko) {
    'use strict';

    $paramount.models = $paramount.models || {};
    $paramount.models.BookTickets = function (data, eventService) {
        var me = this;

        $.extend(data, {});

        me.eventId = ko.observable(data.eventId);
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
        me.showPassword = ko.observable(data.isUserLoggedIn === false);

        // Tickets
        me.reservations = ko.observableArray();
        $.each(data.reservations, function (idx, reservationData) {
            me.reservations.push(new $paramount.models.EventTicketReserved(reservationData));
        });
        me.totalCost = ko.computed(function () {
            return _.sum(me.reservations(), function (r) {
                if (r.notReserved()) {
                    return 0;
                }
                return r.price() * r.quantity();
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
        me.paymentMethod = ko.observable();
        me.setPayPal = function() {
            me.paymentMethod('PayPal');
        }
        me.setCreditCard = function () {
            me.paymentMethod('CreditCard');
        }

        // Submit
        me.submitTicketBooking = function () {
            if (me.outOfTime()) {
                // Scroll to the top 
                $('html, body').animate({ scrollTop: $('.alert-count-down').offset().top }, 1000);
                return;
            }

            var $form = $('#bookTicketsForm');
            var $btn = $('#bookTicketsView button');

            if ($form.valid()) {
                $btn.button('loading');
                var request = ko.toJSON(me);
                eventService.bookTickets(request).success(function (response) {
                    if (response.redirect) {
                        window.location = response.redirect;
                    }

                });
            }
        }
    }

})(jQuery, $paramount, ko);