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
        me.surveyStatChartData = getSurveyStatChartData(editEventViewModel);
        me.newSurveyOption = ko.observable();
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

        me.addSurveyOption = function () {

            var newOption = me.newSurveyOption();
            if (!newOption) {
                return;
            }

            var valueExists = false;

            _.each(me.surveyStatistics(), function (option) {
                if (option.optionName().toLowerCase().trim() === newOption.toLowerCase().trim()) {
                    valueExists = true;
                }
            });

            if (valueExists === true) {
                toastr.error('The option ' + newOption + ' already exists');
                return;
            }

            var model = {
                eventId: me.eventId(),
                optionName: newOption
            };

            adDesignService.addSurveyOption(model).then(function (resp) {
                me.surveyStatistics.push(new SurveyStatistic({
                    optionName: newOption.trim(),
                    count: 0
                }, editEventViewModel.surveyStaticsTotalAnswers));
                me.newSurveyOption(null);
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
    

    function getSurveyStatChartData(model) {
        if (!model.surveyStatistics ||
            !Array.isArray(model.surveyStatistics) ||
            model.surveyStatistics.length === 0) {
            return null;
        }

        var labels = model.surveyStatistics.map(function (stat) {
            return stat.optionName;
        });

        var counts = model.surveyStatistics.map(function (stat) {
            return stat.count;
        });

        var colours = model.surveyStatistics.map(function () {
            return $paramount.getRandomColor();
        });

        return {
            labels: labels,
            datasets: [{
                data: counts,
                backgroundColor: colours
            }]
        }
    }

})(jQuery, ko, $paramount);