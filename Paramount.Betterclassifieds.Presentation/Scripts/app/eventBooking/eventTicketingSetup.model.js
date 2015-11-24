(function ($, ko, $paramount) {

    function EventTicketingSetup(data) {
        var me = this,
            adDesignService = new $paramount.AdDesignService();

        me.tickets = ko.observableArray();

        /*
         * Functions
         */
        me.submitTickets = function (e, jQueryEl) {
            var $button = $(jQueryEl.toElement);
            $button.button('loading');
            var eventTicketingSetup = ko.toJS(me);
            adDesignService.updateEventTicketDetails(eventTicketingSetup);
        }

        me.addTicketType = function () {
            me.tickets.push(new $paramount.models.EventTicketDefinition());
        }

        me.removeTicketType = function(t) {
            me.tickets.remove(t);
        }

        /*
         * Validation
         */
        me.validator = ko.validatedObservable({
        });

        /*
         * Sync existing data
         */
        me.bindEventTicketingSetup(data);
    }

    EventTicketingSetup.prototype.bindEventTicketingSetup = function (data) {
        $.extend(data, {});

        var me = this;
        $.each(data.tickets, function (idx, t) {
            me.tickets.push(new $paramount.models.EventTicketDefinition(t));
        });
    }

    $paramount.models = $paramount.models || {};
    $paramount.models.EventTicketingSetup = EventTicketingSetup;


})(jQuery, ko, $paramount);