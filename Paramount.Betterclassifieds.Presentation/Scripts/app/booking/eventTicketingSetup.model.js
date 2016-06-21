(function ($, ko, $paramount) {

    function EventTicketingSetup(data) {
        var me = this,
            adDesignService = new $paramount.AdDesignService();

        me.tickets = ko.observableArray();
        me.closingDate = ko.observable();
        me.includeTransactionFee = ko.observable();
        me.eventTicketFee = ko.observable();
        me.eventTicketFeeCents = ko.observable();

        /*
         * Functions
         */
        me.addTicketType = function () {
            me.tickets.push(new $paramount.models.EventTicketDefinition(me));
        }

        me.removeTicketType = function (t) {
            me.tickets.remove(t);
        }

        me.submitTickets = function (e) {
            if ($paramount.checkValidity(me) === false) {
                return;
            }

            var $button = $('#btnSubmit');
            $button.button('loading');
            var eventTicketingSetup = ko.toJS(me);
            adDesignService.updateEventTicketDetails(eventTicketingSetup)
                .then(function (resp) {
                    if (resp.nextUrl) {
                        return;
                    }
                    $button.button('reset');
                });
        }

        me.clearClosingDate = function () {
            me.closingDate(null);
        }

        /*
         * Validation
         */
        me.validator = ko.validatedObservable({
            closingDate: me.closingDate.extend({
                pmtMinDate: new Date(data.adStartDate)
            })
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
            me.tickets.push(new $paramount.models.EventTicketDefinition(me, t));
        });
        me.closingDate(data.closingDate);
        me.eventTicketFee(data.eventTicketFee);
        me.eventTicketFeeCents(data.eventTicketFeeCents);
        me.includeTransactionFee(data.includeTransactionFee);
    }

    $paramount.models = $paramount.models || {};
    $paramount.models.EventTicketingSetup = EventTicketingSetup;


})(jQuery, ko, $paramount);