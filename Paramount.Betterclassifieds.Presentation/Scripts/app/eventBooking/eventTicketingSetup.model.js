(function ($, ko, $paramount) {

    function EventTicketingSetup(data) {
        var me = this,
            adDesignService = new $paramount.AdDesignService();

        /*
         * Methods and Computed
         */
        me.submitTickets = function (e, jQueryEl) {
            var $button = $(jQueryEl.toElement);
            $button.button('loading');
            var eventTicketingSetup = ko.toJS(me);
            adDesignService.updateEventTicketDetails(eventTicketingSetup);
        }
        me.addTicketType = function () {
            console.log('add ticket type ' + new Date());
        }


        /*
         * Validation
         */
        me.validator = ko.validatedObservable({
        });

        /*
         * Sync existing data
         */

    }

    $paramount.models = $paramount.models || {};
    $paramount.models.EventTicketingSetup = EventTicketingSetup;


})(jQuery, ko, $paramount);