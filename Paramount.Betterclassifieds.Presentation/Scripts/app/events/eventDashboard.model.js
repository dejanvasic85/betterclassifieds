(function ($, ko, $paramount) {

    function EventDashboardModel(editEventViewModel, adDesignService) {
        var me = this;
        me.eventId = ko.observable();
        me.tickets = ko.observableArray();
        me.totalSoldQty = ko.observable();
        me.isClosed = ko.observable();
        me.totalRemainingQty = ko.computed(function () {
            return _.sumBy(me.tickets(), function (t) {
                return t.remainingQuantity();
            });
        });
        me.requiresEventOrganiserConfirmation = ko.observable();
        me.organiserAbsorbsTransactionFee = ko.observable();
        me.eventOrganiserOwedAmount = ko.observable();
        me.totalSoldAmount = ko.observable();
        me.requestPaymentStatus = ko.observable();
        me.showPaymentStatusLabel = ko.observable();
        me.showPayMeButton = ko.observable();
        me.showWithdrawPayment = ko.observable();
        me.pageViews = ko.observable();
        me.surveyStatistics = $paramount.ko.bindArray(editEventViewModel.surveyStatistics, function (stat) {
            return new SurveyStatistic(stat, editEventViewModel.surveyStaticsTotalAnswers);
        });


        me.addTicketType = function () {
            me.tickets.splice(0, 0, new $paramount.models.EventTicket({
                adId: editEventViewModel.adId,
                eventId: editEventViewModel.eventId,
                ticketName: 'Ticket Name',
                price: 0,
                availableQuantity: 0,
                remainingQuantity: 0,
                soldQty: 0
            }));
        }
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
    }

    EventDashboardModel.prototype.bindEditEvent = function (editEventViewModel) {
        var me = this;
        $.each(editEventViewModel.tickets, function (idx, t) {
            t.adId = editEventViewModel.adId;
            me.tickets.push(new $paramount.models.EventTicket(t));
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
        me.requiresEventOrganiserConfirmation(editEventViewModel.requiresEventOrganiserConfirmation);
    }

    $paramount.models = $paramount.models || {};
    $paramount.models.EventDashboardModel = EventDashboardModel;
    
    function SurveyStatistic(stat, totalAnswers) {
        this.optionName = ko.observable(stat.optionName);
        this.count = ko.observable(stat.count);
        this.total = ko.observable(totalAnswers);
        this.optionNameWithCount = ko.observable(stat.optionName + ' - ' + stat.count + '/' + totalAnswers);
    }

})(jQuery, ko, $paramount);