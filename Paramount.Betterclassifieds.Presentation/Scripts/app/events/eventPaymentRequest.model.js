(function ($, ko, $paramount) {
    'use strict';

    function EventPaymentRequest(data, adDesignService) {
        var me = this;
        me.selectedPaymentType = ko.observable(data.preferredPaymentType);
        me.eventId = ko.observable(data.eventId);
        me.isPayPalConfigured = ko.observable(data.payPalEmail !== null);
        me.isDirectDebitConfigured = ko.observable(data.directDebitDetails !== null);

        me.selectPayPal = function () {
            me.selectedPaymentType($paramount.PAYMENT.PAYPAL);
        }

        me.selectDirectDebit = function () {
            me.selectedPaymentType($paramount.PAYMENT.DIRECT_DEBIT);
        }

        me.isPayPalSelected = ko.computed(function () {
            return me.selectedPaymentType() === $paramount.PAYMENT.PAYPAL;
        });

        me.isDirectDebitSelected = ko.computed(function () {
            return me.selectedPaymentType() === $paramount.PAYMENT.DIRECT_DEBIT;
        });

        me.canSubmitPaymentRequest = ko.computed(function () {
            if (me.selectedPaymentType() === $paramount.PAYMENT.PAYPAL && me.isPayPalConfigured()) {
                return true;
            }
            if (me.selectedPaymentType() === $paramount.PAYMENT.DIRECT_DEBIT && me.isDirectDebitConfigured()) {
                return true;
            }
            return false;
        });

        me.submitPaymentRequest = function (element, event) {
            var $btn = $(event.target); // Grab the jQuery element from knockout
            $btn.button('loading');

            var paymentRequest = {
                paymentMethod: me.selectedPaymentType(),
                eventId: me.eventId()
            }
            adDesignService.requestEventPayment(paymentRequest)
                .error(function () {
                    $btn.button('reset');
                });
        }
    }

    $paramount.models = $paramount.models || {};
    $paramount.models.EventPaymentRequest = EventPaymentRequest;


})(jQuery, ko, $paramount);