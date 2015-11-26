(function ($, ko, $paramount) {

    function EventTicketingSetup(data) {
        var me = this,
            adDesignService = new $paramount.AdDesignService();

        me.tickets = ko.observableArray();
        me.ticketFields = ko.observableArray();

        /*
         * Functions
         */
        me.addTicketType = function () {
            me.tickets.push(new $paramount.models.EventTicketDefinition());
        }

        me.removeTicketType = function (t) {
            me.tickets.remove(t);
        }

        me.addField = function () {
            me.ticketFields.push(new $paramount.models.EventTicketField());
        }

        me.removeTicketField = function (f) {
            me.ticketFields.remove(f);
        }

        me.submitTickets = function (e, jQueryEl) {

            if ($paramount.checkValidity(me.tickets(), me.ticketFields()) === false) {
                return;
            }

            var $button = $(jQueryEl.toElement);
            $button.button('loading');
            var eventTicketingSetup = ko.toJS(me);
            adDesignService.updateEventTicketDetails(eventTicketingSetup);
        }

        me.showFieldOptionWarning = ko.computed(function () {
            return me.ticketFields().length > 3;
        });
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
        if (data.ticketFields) {
            $.each(data.ticketFields, function (idx, f) {
                me.ticketFields.push(new $paramount.models.EventTicketField(f));
            });
        }
    }

    $paramount.models = $paramount.models || {};
    $paramount.models.EventTicketingSetup = EventTicketingSetup;


})(jQuery, ko, $paramount);