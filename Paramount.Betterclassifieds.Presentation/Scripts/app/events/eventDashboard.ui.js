(function ($, $paramount) {

    $paramount.ui = $paramount.ui || {};
    $paramount.ui.eventDashboard = {
        init: function (eventDashboardViewModel) {
            var adDesignService = new $paramount.AdDesignService(eventDashboardViewModel.adId);
            var eventDashoardModel = new $paramount.models.EventDashboardModel(eventDashboardViewModel, adDesignService);
            ko.applyBindings(eventDashoardModel);
        }
    };

})(jQuery, $paramount);