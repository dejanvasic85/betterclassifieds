(function ($, $paramount) {
    $paramount.ui = $paramount.ui || {};
    $paramount.ui.eventPaymentRequest = {
        init: function (eventPaymentRequestViewModel) {
            var $rootElement = $('.event-payment-request');

            /*
             * Knockout model
             */

            var adDesignService = new $paramount.AdDesignService(eventPaymentRequestViewModel.adId);
            var viewModel = new $paramount.models.EventPaymentRequest(eventPaymentRequestViewModel, adDesignService);
            ko.applyBindings(viewModel, $rootElement.get(0));
        }
    }

})(jQuery, $paramount);