
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

    $paramount.EventService = EventService;
    return $paramount;

})(jQuery, $paramount);