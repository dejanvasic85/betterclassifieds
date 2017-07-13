(function ($, $p, ko) {

    $p.models.EventPromoCode = function (data) {
        var me = this;

        me.eventPromoCodeId = ko.observable(data.eventPromoCodeId);
        me.eventId = ko.observable(data.eventId);
        me.promoCode = ko.observable(data.promoCode);
        me.discountPercent = ko.observable(data.discountPercent);
        me.isDisabled = ko.observable(data.isDisabled);
        me.createdDate = ko.observable(data.createdDate);

        // Validation
        me.validator = ko.validatedObservable({
            promoCode: me.promoCode.extend({ required: true, maxlength: 50 }),
            discountPercent: me.discountPercent.extend({ min: 0, max: 100 })
        });

        me.discountDisplay = ko.computed(function() {
            if (!me.discountPercent()) {
                return "";
            }

            return me.discountPercent() + '%';
        });
    }

})(jQuery, $paramount, ko);