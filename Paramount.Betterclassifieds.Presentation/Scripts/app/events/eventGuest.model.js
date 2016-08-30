(function ($, ko, $paramount) {
    'use strict';

    function CurrentGuest(data) {
        var me = this;
        var adDesignService = new $paramount.AdDesignService(data.adId);

        me.guestFullName = ko.observable(data.guestFullName);
        me.guestEmail = ko.observable(data.guestEmail);
        me.dynamicFields = ko.observableArray();
        $.each(data.dynamicFields, function (idx, f) {
            me.dynamicFields.push(new $paramount.models.DynamicFieldValue(f));
        });

        me.editGuestUrl = ko.observable(adDesignService.editGuestUrl(data.ticketNumber));
    }

    $paramount.models = $paramount.models || {};
    $paramount.models.CurrentGuest = CurrentGuest;


})(jQuery, ko, $paramount);