
(function ($, $paramount) {

    function EventService(baseUrl) {
        this.baseUrl = baseUrl || $paramount.baseUrl;
    }

    EventService.prototype.startTicketOrder = function (order) {
        return $paramount.httpPost(this.baseUrl + 'Event/ReserveTickets', order);
    }

    EventService.prototype.bookTickets = function (ticketBookingDetails) {
        return $paramount.httpPost(this.baseUrl + 'Event/BookTickets', ticketBookingDetails);
    }

    EventService.prototype.updateTicket = function (ticketDetails) {
        return $paramount.httpPost(this.baseUrl + 'EditAd/EventTicketUpdate', {
            id: ticketDetails.adId, eventTicketViewModel: ticketDetails
        });
    }

    EventService.prototype.payWithPayPal = function () {
        return $paramount.httpPost(this.baseUrl + 'Event/PayWithPayPal');
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

    $paramount.EventService = EventService;
    return $paramount;

})(jQuery, $paramount);