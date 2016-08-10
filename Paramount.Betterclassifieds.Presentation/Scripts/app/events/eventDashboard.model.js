(function ($, ko, $paramount) {
    'use strict';

    function EventDashboardModel(editEventViewModel, adDesignService) {
        var me = this;
        me.eventId = ko.observable();
        me.tickets = ko.observableArray();
        me.guests = ko.observableArray();
        me.totalSoldQty = ko.observable();
        me.isClosed = ko.observable();
        me.totalRemainingQty = ko.computed(function () {
            return _.sum(me.tickets(), function (t) {
                return t.remainingQuantity();
            });
        });
        me.organiserAbsorbsTransactionFee = ko.observable();
        me.eventOrganiserOwedAmount = ko.observable();
        me.totalSoldAmount = ko.observable();
        me.requestPaymentStatus = ko.observable();
        me.showPaymentStatusLabel = ko.observable();
        me.showPayMeButton = ko.observable();
        me.showWithdrawPayment = ko.observable();
        me.pageViews = ko.observable();
        me.addTicketType = function () {
            me.tickets.splice(0, 0, new $paramount.models.EventTicket({
                editMode: true,
                adId: editEventViewModel.adId,
                eventId: editEventViewModel.eventId,
                ticketName: 'Ticket Name',
                price: 0,
                availableQuantity: 0,
                remainingQuantity: 0,
                soldQty: 0
            }));
        }
        me.hasGuests = ko.computed(function () {
            return me.guests().length > 0;
        });
        me.bindEditEvent(editEventViewModel);

        /*
         * Methods - Close event
         */
        me.closeEvent = function (element, event) {
            var $btn = $(event.target); // Grab the jQuery element from knockout
            $btn.button('loading');

            adDesignService.closeEvent(me.eventId())
                .done(function () {
                    $('#closeEventDialog').modal('hide');
                    me.isClosed(true);
                    me.requestPaymentStatus($paramount.EVENT_PAYMENT_STATUS.REQUEST_PENDING);
                })
                .complete(function () {
                    $btn.button('reset');
                });
        }

        /*
         * Computed Methods
         */
        me.showPaymentStatusLabel = ko.computed(function () {
            return me.requestPaymentStatus() !== $paramount.EVENT_PAYMENT_STATUS.REQUEST_PENDING;
        });

        me.showPayMeButton = ko.computed(function () {
            return me.requestPaymentStatus() === $paramount.EVENT_PAYMENT_STATUS.REQUEST_PENDING;
        });

        me.showWithdrawPayment = ko.computed(function () {
            return me.requestPaymentStatus() !== $paramount.EVENT_PAYMENT_STATUS.NOT_AVAILABLE && me.eventOrganiserOwedAmount() > 0;
        });


        me.guestListFilter = ko.observable();
        me.guestsFilterd = ko.computed(function () {
            var filter = me.guestListFilter();
            if (!filter) {
                return me.guests();
            } else {
                return ko.utils.arrayFilter(me.guests(), function (g) {
                    return g.guestFullName().toLowerCase().indexOf(filter.toLowerCase()) > -1;
                });
            }
        });
    }

    EventDashboardModel.prototype.bindEditEvent = function (editEventViewModel) {
        var me = this;
        $.each(editEventViewModel.tickets, function (idx, t) {
            t.adId = editEventViewModel.adId;
            me.tickets.push(new $paramount.models.EventTicket(t));
        });
        $.each(editEventViewModel.guests, function (idx, g) {
            me.guests.push(new $paramount.models.EventGuest(g));
        });

        me.eventId(editEventViewModel.eventId);
        me.organiserAbsorbsTransactionFee(editEventViewModel.organiserAbsorbsTransactionFee);
        me.isClosed(editEventViewModel.isClosed);
        me.totalSoldAmount(editEventViewModel.totalSoldAmount);
        me.eventOrganiserOwedAmount(editEventViewModel.eventOrganiserOwedAmount);
        me.totalSoldAmountFormatted = ko.computed(function () {
            return $paramount.formatCurrency(me.totalSoldAmount());
        });
        me.totalSoldQty(editEventViewModel.totalSoldQty);
        me.pageViews(editEventViewModel.pageViews);
        me.requestPaymentStatus(editEventViewModel.eventPaymentRequestStatus);
    }

    $paramount.models = $paramount.models || {};
    $paramount.models.EventDashboardModel = EventDashboardModel;

})(jQuery, ko, $paramount);