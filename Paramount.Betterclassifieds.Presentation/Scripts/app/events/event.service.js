
(function ($, $paramount) {

    function EventService(baseUrl) {
        this.baseUrl = baseUrl || $paramount.baseUrl;
    }

    EventService.prototype.getEvent = function (eventId) {
        return $paramount.httpGet(this.baseUrl + 'api/events/' + eventId);
    }

    EventService.prototype.startTicketOrder = function (order) {
        return $paramount.httpPost(this.baseUrl + 'event/reserveTickets', order);
    }

    EventService.prototype.bookTickets = function (ticketBookingDetails) {
        return $paramount.httpPost(this.baseUrl + 'event/bookTickets', ticketBookingDetails);
    }

    EventService.prototype.updateTicket = function (ticketDetails) {
        return $paramount.httpPost(this.baseUrl + 'editAd/eventTicketUpdate', {
            id: ticketDetails.adId, eventTicketViewModel: ticketDetails
        });
    }

    EventService.prototype.payWithPayPal = function () {
        return $paramount.httpPost(this.baseUrl + 'event/payWithPayPal');
    }

    EventService.prototype.getFieldsForTicket = function (eventTicketId) {
        return $paramount.httpGet(this.baseUrl + 'event/ticket-fields?id=' + eventTicketId);
    }

    EventService.prototype.getGroups = function (eventId) {
        var url = this.baseUrl + 'api/events/' + eventId + '/groups';
        return $paramount.httpGet(url);
    }

    EventService.prototype.getGroupsForTicket = function (eventId, eventTicketId) {
        var url = this.baseUrl + 'api/events/' + eventId + '/tickets/' + eventTicketId + '/groups';
        return $paramount.httpGet(url);
    }

    EventService.prototype.getTicketsForEvent = function (eventId) {
        var url = this.baseUrl + 'api/events/' + eventId + '/tickets';
        return $paramount.httpGet(url);
    }

    EventService.prototype.getTicketsForGroup = function (eventId, eventGroupId) {
        var url = this.baseUrl + 'api/events/' + eventId + '/groups/' + eventGroupId + '/tickets';
        return $paramount.httpGet(url);
    }

    EventService.prototype.getGuests = function (eventId) {
        var url = this.baseUrl + 'api/events/' + eventId + '/guests';
        return $paramount.httpGet(url);
    }

    EventService.prototype.getGuestsForTicket = function (eventId, eventTicketId) {
        var url = this.baseUrl + 'api/events/' + eventId + '/ticket/' + eventTicketId + '/guests';
        return $paramount.httpGet(url);
    }

    EventService.prototype.assignGroup = function (eventBookingTicketId, eventGroupId) {
        var data = {
            eventBookingTicketId: eventBookingTicketId,
            eventGroupId: eventGroupId
        }
        return $paramount.httpPost(this.baseUrl + 'event/assign-group', data);
    }

    EventService.prototype.calculateBuyerPriceWithTxnFee = function (price, eventTicketFee, eventTicketFeeCents) {
        var percentage = ((eventTicketFee / 100) + 1);
        var amount = (percentage * price) + (eventTicketFeeCents / 100);
        return $paramount.formatCurrency(amount);
    }

    EventService.prototype.resendGuestEmail = function (adId, eventBookingTicketId) {
        return $paramount.httpPost(this.baseUrl + 'event-dashboard/' + adId + '/resend-email/' + eventBookingTicketId);
    }

    EventService.prototype.createGuestEmailSendPromises = function (id, guests) {
        var me = this;
        var emailFuncs = _.map(guests, function (g) {
            return function () {
                return new Promise(function (resendResolve) {

                    me.resendGuestEmail(id, g.ticketNumber)
                        .then(function (res) {
                            resendResolve(res);
                        });

                });
            }
        });

        return emailFuncs;
    }

    $paramount.EventService = EventService;
    return $paramount;

})(jQuery, $paramount);