(function ($, $paramount, ko, notifier) {
    'use strict';

    function EventTicket(data, maxTicketsPerBooking) {

        var me = this;
        var eventService = new $paramount.EventService();

        me.adId = ko.observable();
        me.eventId = ko.observable();
        me.eventTicketId = ko.observable();
        me.ticketName = ko.observable();
        me.availableQuantity = ko.observable();
        me.price = ko.observable();
        me.priceFormatted = ko.computed(function () {
            return $paramount.formatCurrency(me.price());
        });
        me.selectedQuantity = ko.observable();
        me.remainingQuantity = ko.observable();
        me.soldQuantity = ko.observable();
        me.soldOut = ko.observable();
        me.isAvailable = ko.observable();
        me.maxTicketsPerBooking = ko.observableArray();

        // Editing functionality (requires adId to go through the AuthoriseBookingIdentity server attribute)
        me.editMode = ko.observable();
        me.enableEdit = function () {
            me.editMode(true);
        }
        me.saveTicketDetails = function (e, jQueryElement) {
            var $button = $(jQueryElement.toElement);
            $button.button('loading');
            var ticketDetails = ko.toJS(me);
            eventService.updateTicket(ticketDetails).done(function (response) {
                if (response) {
                    notifier.success("Ticket details saved successfully");
                    me.editMode(false);
                    $button.button('reset');
                }
            });
        }

        me.validator = ko.validatedObservable({
            ticketName: me.ticketName.extend({ required: true }),
            availableQuantity: me.availableQuantity.extend({ min: 0, required: true }),
            remainingQuantity: me.remainingQuantity.extend({ min: 0, required: true }),
            price: me.price.extend({ min: 0, required: true })
        });

        me.bindEventTicket(data, maxTicketsPerBooking);
    }

    EventTicket.prototype.bindEventTicket = function (data, maxTicketsPerBooking) {
        var me = this;

        me.adId(data.adId);
        me.eventId(data.eventId);
        me.eventTicketId(data.eventTicketId);
        me.ticketName(data.ticketName);
        me.availableQuantity(data.availableQuantity);
        me.price(data.price);
        me.selectedQuantity(data.selectedQuantity);
        me.remainingQuantity(data.remainingQuantity);
        me.soldQuantity(data.availableQuantity - data.remainingQuantity);
        me.soldOut(data.remainingQuantity <= 0);
        me.isAvailable(data.remainingQuantity > 0);
        me.editMode(data.enableEdit === true);

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