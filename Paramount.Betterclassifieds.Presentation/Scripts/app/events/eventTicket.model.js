(function ($, $paramount, ko) {
    
    function EventTicket(data, maxTicketsPerBooking) {

        var me = this;

        me.adId = ko.observable();
        me.eventId = ko.observable();
        me.eventTicketId = ko.observable();
        me.ticketName = ko.observable();
        me.availableQuantity = ko.observable();
        me.price = ko.observable();
        me.isActive = ko.observable();
        me.priceFormatted = ko.computed(function () {
            return $paramount.formatCurrency(me.price());
        });
        me.selectedQuantity = ko.observable();
        me.remainingQuantity = ko.observable();
        me.soldQuantity = ko.observable();
        me.isAvailable = ko.observable();
        me.maxTicketsPerBooking = ko.observableArray();
        me.eventGroupId = ko.observable();
        me.eventGroupName = ko.observable();
        me.editTicketUrl = ko.observable();
        

        /*
         * Computed functions
         */
        me.soldOut = ko.computed(function () {
            return me.remainingQuantity() <= 0;
        });

        
        /*
         * Validation
         */
        me.validator = ko.validatedObservable({
            ticketName: me.ticketName.extend({ required: true }),
            availableQuantity: me.availableQuantity.extend({ min: 0, required: true }),
            remainingQuantity: me.remainingQuantity.extend({ min: 0, required: true }),
            price: me.price.extend({ min: 0, required: true })
        });

        if (data) {
            me.bindEventTicket(data, maxTicketsPerBooking);
        }
    }

    EventTicket.prototype.bindEventTicket = function (data, maxTicketsPerBooking) {
        var me = this;
        me.adId(data.adId);
        me.eventId(data.eventId);
        me.eventTicketId(data.eventTicketId);
        me.ticketName(data.ticketName);
        me.availableQuantity(data.availableQuantity);
        me.isActive(data.isActive);
        me.price(data.price);
        me.selectedQuantity(data.selectedQuantity);
        me.remainingQuantity(data.remainingQuantity);
        me.isAvailable(data.remainingQuantity > 0);
        me.soldQuantity(data.soldQty);
       
        me.editTicketUrl('/event-dashboard/' + data.adId + '/event-ticket/' + data.eventTicketId);

        // MaxTickets Per booking setup
        if (maxTicketsPerBooking) {
            if (data.remainingQuantity < maxTicketsPerBooking) {
                maxTicketsPerBooking = data.remainingQuantity;
            }

            for (var i = 0; i <= maxTicketsPerBooking; i++) {
                me.maxTicketsPerBooking.push({ label: i, value: i });
            }
        }

    }


    $paramount.models = $paramount.models || {};
    $paramount.models.EventTicket = EventTicket;


})(jQuery, $paramount, ko, toastr);