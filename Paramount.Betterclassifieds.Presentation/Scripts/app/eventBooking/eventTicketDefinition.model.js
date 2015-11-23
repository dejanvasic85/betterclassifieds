(function ($, ko, $paramount) {

    function EventTicketDefinition(data) {
        var me = this;
        me.ticketName = ko.observable();
        me.availableQuantity = ko.observable();
        me.price = ko.observable();

        /*
         * Validation
         */
        me.validator = ko.validatedObservable({
            ticketName: me.ticketName.extend({ required: true }),
            availableQuantity: me.availableQuantity.extend({ required: true, min: 0 }),
            price: me.price.extend({ min: 0, required: true })
        });

        /*
         * Sync existing data
         */
        this.bindEventTicketDefinition(data);
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