(function ($, ko, $paramount) {

    function EventTicketingSetup(data) {
        var me = this,
            adDesignService = new $paramount.AdDesignService();

        me.tickets = ko.observableArray();
        me.ticketFields = ko.observableArray();
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

        me.addField = function () {
            me.ticketFields.push(new $paramount.models.DynamicFieldDefinition());
        }

        me.removeTicketField = function (f) {
            me.ticketFields.remove(f);
        }

        me.submitTickets = function (e) {

            if ($paramount.checkValidity(me, me.tickets(), me.ticketFields()) === false) {
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

        me.showFieldOptionWarning = ko.computed(function () {
            return me.ticketFields().length > 3;
        });

        me.showClosingDateInfo = ko.computed(function () {
            return me.closingDate() !== null;
        });

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
        if (data.ticketFields) {
            $.each(data.ticketFields, function (idx, f) {
                me.ticketFields.push(new $paramount.models.DynamicFieldDefinition(f));
            });
        }
        me.closingDate(data.closingDate);
        me.eventTicketFee(data.eventTicketFee);
        me.eventTicketFeeCents(data.eventTicketFeeCents);
        me.includeTransactionFee(data.includeTransactionFee);
    }

    $paramount.models = $paramount.models || {};
    $paramount.models.EventTicketingSetup = EventTicketingSetup;


})(jQuery, ko, $paramount);