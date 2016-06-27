(function ($, ko, $paramount) {

    function EventTicketDefinition(parent, data) {
        var me = this;
        me.ticketName = ko.observable();
        me.availableQuantity = ko.observable();
        me.eventTicketFields = ko.observableArray();
        me.price = ko.observable();

        function calculateBuyerPriceWithTxnFee(price) {
            var percentage = ((parent.eventTicketFee() / 100) + 1);
            var amount = (percentage * price) + (parent.eventTicketFeeCents() / 100);
            return $paramount.formatCurrency(amount);
        }

        me.addField = function () {
            me.eventTicketFields.push(new $paramount.models.DynamicFieldDefinition());
        }

        me.removeTicketField = function (f) {
            me.eventTicketFields.remove(f);
        }

        me.totalTicketCost = ko.computed(function () {
            if (!me.price()) {
                return "";
            }

            if (parent.includeTransactionFee() === true && me.price() > 0) {
                return calculateBuyerPriceWithTxnFee(me.price());
            }
            return $paramount.formatCurrency(me.price());
        });

        /*
         * Validation
         */
        me.validator = ko.validatedObservable({
            ticketName: me.ticketName.extend({ required: true }),
            availableQuantity: me.availableQuantity.extend({ min: 0, required: true }),
            price: me.price.extend({ min: 0, required: true })
        });

        /*
         * Sync existing data
         */
        if (data) {
            this.bindEventTicketDefinition(data);
        }
    }

    EventTicketDefinition.prototype.bindEventTicketDefinition = function (data) {
        $.extend(data, {});

        var me = this;
        me.ticketName(data.ticketName);
        me.availableQuantity(data.availableQuantity);
        me.price(data.price);

        if (data.eventTicketFields) {
            $.each(data.eventTicketFields, function (idx, f) {
                me.eventTicketFields.push(new $paramount.models.DynamicFieldDefinition(f));
            });
        }
    }

    $paramount.models = $paramount.models || {};
    $paramount.models.EventTicketDefinition = EventTicketDefinition;


})(jQuery, ko, $paramount);