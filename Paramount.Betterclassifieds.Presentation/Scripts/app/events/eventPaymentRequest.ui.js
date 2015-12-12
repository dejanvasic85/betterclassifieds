(function ($, $paramount) {
    'use strict';

    $paramount.ui = $paramount.ui || {};
    $paramount.ui.eventPaymentRequest = {
        init: function (eventPaymentRequestViewModel) {
            var $rootElement = $('.event-payment-request');


            /*
             * Class toggling for the payment buttons
             */
            var $paymentBtns = $rootElement.find('.payment-btn');
            $rootElement.find('.payment-btn').on('click', function () {
                $paymentBtns.removeClass('btn-success');
                $(this).addClass('btn-success');
            });

            /*
             * Knockout model
             */

            var adDesignService = new $paramount.AdDesignService(eventPaymentRequestViewModel.adId);
            var viewModel = new $paramount.models.EventPaymentRequest(eventPaymentRequestViewModel, adDesignService);
            ko.applyBindings(viewModel, $rootElement.get(0));
        }
    }

})(jQuery, $paramount);