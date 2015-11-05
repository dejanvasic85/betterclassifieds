(function ($, ko, $paramount) {
    'use strict';

    function EditEventModel(editEventViewModel) {
        var me = this;
        me.tickets = ko.observableArray();
        $.each(editEventViewModel.tickets, function (idx, t) {
            me.tickets.push(new $paramount.models.EventTicket(t));
        });
    }

    $paramount.models = $paramount.models || {};
    $paramount.models.EditEventModel = EditEventModel;

})(jQuery, ko, $paramount);