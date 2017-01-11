(function () {

    ko.components.register('availability-options', {
        viewModel: function (params) {
            var me = this;

            this.availability = ko.observable("alwaysAvailable");
            if (params.startDate() != null || params.endDate() != null) {
                this.availability("specificDates");
            }

            this.startDate = params.startDate;
            this.endDate = params.endDate;

            this.availability.subscribe(function(newValue) {

                if (newValue === "alwaysAvailable") {
                    me.startDate(null);
                    me.endDate(null);
                }

            });
        },
        template: {
            path: $paramount.baseUrl + '/Scripts/app/availabilityOptions/availability-options.html'
        }
    });

})(ko);