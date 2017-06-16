(function ($, $paramount, ko) {

    function BookTickets(data, eventService) {
        var me = this;

        $.extend(data, {});

        me.eventId = ko.observable(data.eventId);
        me.includeTransactionFee = ko.observable(data.includeTransactionFee);
        me.minsRemaining = ko.observable(data.reservationExpiryMinutes);
        me.secondsRemaining = ko.observable(data.reservationExpirySeconds);
        me.secondsRemainingDisplay = ko.computed(function () {
            return ("0" + me.secondsRemaining()).slice(-2);
        });
        me.canContinue = ko.observable(data.successfulReservationCount > 0);
        me.notAllRequestsAreFulfilled = ko.observable(data.reservations.length !== data.successfulReservationCount);
        me.displayGuests = ko.observable(data.displayGuests);

        // User details
        me.firstName = ko.observable(data.firstName);
        me.lastName = ko.observable(data.lastName);
        me.phone = ko.observable(data.phone);
        me.postCode = ko.observable(data.postCode);
        me.email = ko.observable(data.email);

        // Promo code
        me.promoCode = ko.observable();
        me.promoCodeApplied = ko.observable(0);
        me.promoDiscountPercent = ko.observable();
        me.promoDiscountAmount = ko.observable();
        me.promoNotAvailable = ko.observable();
        
        me.applyPromoCode = function (model, event) {
            if (!me.promoCode()) {
                return;
            }
            var $btn = $(event.target);
            $btn.loadBtn();

            eventService.applyPromoCode(me.eventId(), me.promoCode())
                .then(function (r) {
                    if (r.errors) {
                        return;
                    }

                    if (r.notAvailable === true) {
                        me.promoNotAvailable(true);
                        return;
                    }

                    me.promoCodeApplied(true);
                    me.promoDiscountPercent(r.discountPercent);
                    me.promoDiscountAmount(r.discountAmount);
                    me.priceAfterDiscount(r.priceAfterDiscount);
                    me.fee(r.fee);
                    me.totalPrice(r.totalPrice);

                })
                .always(function () {
                    $btn.resetBtn();
                });
        }

        // Pricing
        me.price = ko.observable(data.totalCostWithoutFees);
        me.priceAfterDiscount = ko.observable();
        me.fee = ko.observable(data.totalFees);
        me.totalPrice = ko.observable(data.totalCost);
        me.requiresPayment = ko.computed(function() {
            return me.totalPrice() > 0;
        });

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
            reservationData.displayGuests = me.displayGuests();
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

            if (!$paramount.checkValidity(me.reservations()) || !$paramount.checkValidity(fields)) {
                return;
            };

            var $form = $('#bookTicketsForm');
            var $btn = $('#proceedToPaymentBtn');

            if ($form.valid() === true) {
                $btn.loadBtn();
                
                var request = ko.toJSON(me);
                eventService.bookTickets(request)
                    .then(function (resp) {
                        if (resp.nextUrl) {
                            return;
                        }
                        $btn.resetBtn();
                    });
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