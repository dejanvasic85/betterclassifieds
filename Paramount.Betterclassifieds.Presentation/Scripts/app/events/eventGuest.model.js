(function ($, ko, $paramount) {
    'use strict';

    function EventGuest(data) {
        var me = this;
        me.guestFullName = ko.observable(data.guestFullName);
        me.guestEmail = ko.observable(data.guestEmail);
        me.dynamicFields = ko.observableArray();
        $.each(data, function (idx, f) {
            me.dynamicFields.push(new $paramount.models.DynamicFieldValue(f));
        });
    }

    $paramount.models = $paramount.models || {};
    $paramount.models.EventGuest = EventGuest;


})(jQuery, ko, $paramount);