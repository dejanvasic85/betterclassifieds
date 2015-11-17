(function ($, $paramount) {

    'use strict';

    $paramount.ui = $paramount.ui || {};
    $paramount.ui.eventDashboard = {
        init: function (eventDashboardViewModel) {
            var rootElement = $('.event-dashboard');
            var eventDashoardModel = new $paramount.models.EventDashboardModel(eventDashboardViewModel);
            ko.applyBindings(eventDashoardModel, rootElement.get(0));

            var ctx = document.getElementById("ticketSalesChart").getContext("2d");
            var theChart = new Chart(ctx).Doughnut([
                    {
                        value: eventDashboardViewModel.totalSoldQty,
                        color: "#46BFBD",
                        highlight: "#5AD3D1",
                        label: "Sold"
                    },
                    {
                        value: eventDashboardViewModel.totalRemainingQty,
                        color: "#F7464A",
                        highlight: "#FF5A5E",
                        label: "Remaining"
                    }
            ]);

        }
    };

})(jQuery, $paramount);