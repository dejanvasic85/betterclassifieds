(function ($, ko, $paramount) {
    
    function EventDashboardModel(editEventViewModel, adDesignService) {
        var me = this;
        me.adId = ko.observable();
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
        me.newSurveyOption = ko.observable();
        me.surveyStatChartData = ko.observable(buildSurveyChartData(editEventViewModel));
        me.bindEditEvent(editEventViewModel);

        /*
         * Methods - Close event
         */
        me.closeEvent = function (element, event) {
            var $btn = $(event.target); // Grab the jQuery element from knockout
            $btn.loadBtn();

            adDesignService.closeEvent(me.eventId())
                .then(function (res) {
                    if (res.errors) {
                        return;
                    }

                    $('#closeEventDialog').modal('hide');
                    me.isClosed(true);
                    me.requestPaymentStatus($paramount.EVENT_PAYMENT_STATUS.REQUEST_PENDING);
                });
        }

        me.deleteEvent = function (element, event) {
            var $btn = $(event.target);
            $btn.loadBtn();

            // This should return a nextUrl property that will automatically navigate away from this page
            adDesignService.remove(me.adId());
        }

        me.addSurveyOption = function () {

            var newOption = me.newSurveyOption();
            if (!newOption) {
                return;
            }

            if (_.includes(editEventViewModel.surveyStatistics, newOption)) {
                toastr.error('The option ' + newOption + ' already exists');
                return;
            }

            var model = {
                eventId: me.eventId(),
                optionName: newOption,
                count: 0
            };

            var currentStats = editEventViewModel.surveyStatistics;

            adDesignService.addSurveyOption(model).then(function (resp) {

                if (resp.errors) {
                    return;
                }
                
                var updated = {
                    surveyStatistics: [...currentStats, Object.assign({}, model)]
                };

                var updatedChartData = buildSurveyChartData(updated);
                me.surveyStatChartData(updatedChartData);

                me.newSurveyOption(null);
                toastr.success('Option added successfully');
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

        me.adId(editEventViewModel.adId);
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


    function buildSurveyChartData(model) {
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
            totalCount: _.sum(counts),
            labels: labels,
            datasets: [{
                data: counts,
                backgroundColor: colours
            }]
        }
    }

})(jQuery, ko, $paramount);