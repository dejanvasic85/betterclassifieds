(function ($, $paramount) {

    'use strict';

    $paramount.ui = $paramount.ui || {};
    $paramount.ui.editEvent = {
        init: function (editEventViewModel) {
            var rootElement = $('.event-ticket-management');

            var eventEditModel = new $paramount.models.EditEventModel(editEventViewModel);
            ko.applyBindings(eventEditModel, rootElement.get(0));

        }
    };

})(jQuery, $paramount);