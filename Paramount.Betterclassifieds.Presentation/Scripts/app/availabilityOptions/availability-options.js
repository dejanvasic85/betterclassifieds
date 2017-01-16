(function () {

    ko.components.register('availability-options', {
        viewModel: function (params) {
            var me = this;

            this.availability = ko.observable("alwaysAvailable");

            if (isObservableDefinedAndNotNull(params.startDate) || isObservableDefinedAndNotNull(params.endDate)) {
                this.availability("specificDates");
            }

            this.startDate = params.startDate;
            this.endDate = params.endDate;

            // Optional parameters
            this.startDateLabel = params.startDateLabel || 'Start Date';
            this.endDateLabel = params.endDateLabel || 'End Date';
            this.showEndDate = _.isUndefined(params.showEndDate) ? true : params.showEndDate;
            this.dateOptionLabel = params.dateOptionLabel || 'Only for the following dates';

            this.availability.subscribe(function(newValue) {

                if (newValue === "alwaysAvailable") {
                    me.startDate(null);
                    if(me.endDate){
                        me.endDate(null);
                    }

                    if (params.alwaysAvailableSelected) {
                        params.alwaysAvailableSelected();
                    }
                }

            });
        },
        template: {
            path: $paramount.baseUrl + 'Scripts/app/availabilityOptions/availability-options.html'
        }
    });

    function isObservableDefinedAndNotNull(observable) {
        if (!_.isUndefined(observable) && !_.isNull(observable())) {
            return true;
        }
        return false;
    }

})(ko);