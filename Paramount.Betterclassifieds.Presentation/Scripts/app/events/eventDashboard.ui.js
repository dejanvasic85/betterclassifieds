(function ($, $paramount) {

    $paramount.ui = $paramount.ui || {};
    $paramount.ui.eventDashboard = {
        init: function (eventDashboardViewModel) {
            var rootElement = $('.event-dashboard');
            var adDesignService = new $paramount.AdDesignService(eventDashboardViewModel.adId);
            var eventDashoardModel = new $paramount.models.EventDashboardModel(eventDashboardViewModel, adDesignService);
            ko.applyBindings(eventDashoardModel, rootElement.get(0));
        }
    };

})(jQuery, $paramount);