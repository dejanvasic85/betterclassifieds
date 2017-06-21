(function($, $p, ko) {

    $p.models.EventPromoCode = function(data) {
        var me = this;

        me.eventPromoCodeId = ko.observable(data.eventPromoCodeId);
        me.eventId = ko.observable(data.eventId);
        me.promoCode = ko.observable(data.promoCode);
        me.discountPercent = ko.observable(data.discountPercent);
        me.isDisabled = ko.observable(data.isDisabled);
        me.createdDate = ko.observable(data.createdDate);

    }

})(jQuery, $paramount, ko);