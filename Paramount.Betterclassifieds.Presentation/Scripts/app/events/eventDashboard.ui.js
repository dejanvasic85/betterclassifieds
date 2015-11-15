(function ($, $paramount) {

    'use strict';

    $paramount.ui = $paramount.ui || {};
    $paramount.ui.eventDashboard = {
        init: function (eventDashboardViewModel) {
            var rootElement = $('.event-dashboard');
            var eventDashoardModel = new $paramount.models.EventDashboardModel(eventDashboardViewModel);
            ko.applyBindings(eventDashoardModel, rootElement.get(0));

        }
    };

})(jQuery, $paramount);