(function ($, ko, $paramount) {

    function EventTicketDefinition(parent, data) {
        var me = this;
        me.ticketName = ko.observable();
        me.availableQuantity = ko.observable();
        me.price = ko.observable();
        

        me.totalTicketCost = ko.computed(function () {
            if (!me.price()) {
                return "";
            }

            if (parent.includeTransactionFee() === true) {
                return $paramount.formatCurrency((me.price() * 1.04));
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
    }

    $paramount.models = $paramount.models || {};
    $paramount.models.EventTicketDefinition = EventTicketDefinition;


})(jQuery, ko, $paramount);