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
                        value: 300,
                        color: "#F7464A",
                        highlight: "#FF5A5E",
                        label: "Red"
                    },
                    {
                        value: 50,
                        color: "#46BFBD",
                        highlight: "#5AD3D1",
                        label: "Green"
                    },
                    {
                        value: 100,
                        color: "#FDB45C",
                        highlight: "#FFC870",
                        label: "Yellow"
                    }
            ]);

        }
    };

})(jQuery, $paramount);