(function ($, $paramount, ko, notifier) {
    'use strict';

    $paramount.models = $paramount.models || {};
    $paramount.models.EventTicket = function (data, maxTicketsPerBooking) {
        $.extend(data, {});
        var me = this;
        var eventService = new $paramount.EventService();

        me.adId = ko.observable(data.adId);
        me.eventId = ko.observable(data.eventId);
        me.eventTicketId = ko.observable(data.eventTicketId);
        me.ticketName = ko.observable(data.ticketName);
        me.availableQuantity = ko.observable(data.availableQuantity);
        me.price = ko.observable(data.price);
        me.priceFormatted = ko.computed(function () {
            return $paramount.formatCurrency(me.price());
        });
        me.selectedQuantity = ko.observable(data.selectedQuantity);
        me.remainingQuantity = ko.observable(data.remainingQuantity);
        me.soldQuantity = ko.observable(data.availableQuantity - data.remainingQuantity);
        me.soldOut = ko.observable(data.remainingQuantity <= 0);
        me.isAvailable = ko.observable(data.remainingQuantity > 0);

        // MaxTickets Per booking setup
        if (maxTicketsPerBooking) {
            if (data.remainingQuantity < maxTicketsPerBooking) {
                maxTicketsPerBooking = data.remainingQuantity;
            }
            me.maxTicketsPerBooking = ko.observableArray();
            for (var i = 0; i <= maxTicketsPerBooking; i++) {
                me.maxTicketsPerBooking.push({ label: i, value: i });
            }
        }

        // Editing functionality (requires adId to go through the AuthoriseBookingIdentity server attribute)
        me.editMode = ko.observable(false);
        me.enableEdit = function() {
            me.editMode(true);
        }
        me.saveTicketDetails = function (e, jQueryElement) {
            var $button = $(jQueryElement.toElement);
            $button.button('loading');
            var ticketDetails = ko.toJS(me);
            eventService.updateTicket(ticketDetails).done(function (response) {
                if (response) {
                    notifier.success("Ticket details updated successfully");
                    me.editMode(false);
                    $button.button('reset');
                }
            });
        }
    }
})(jQuery, $paramount, ko, toastr);