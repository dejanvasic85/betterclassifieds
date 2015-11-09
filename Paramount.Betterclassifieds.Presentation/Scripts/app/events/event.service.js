(function ($, $paramount) {

    function post(url, data) {
        return $.ajax({
            url: url,
            data: data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json'
        });
    }

    function EventService(baseUrl) {
        this.baseUrl = baseUrl || $paramount.baseUrl;
    }

    EventService.prototype.startTicketOrder = function (order) {
        return post(this.baseUrl + 'Event/ReserveTickets', order);
    }

    EventService.prototype.bookTickets = function (ticketBookingDetails) {
        return post(this.baseUrl + 'Event/BookTickets', ticketBookingDetails);
    }

    EventService.prototype.updateTicket = function (ticketDetails) {
        return $paramount.httpPost(this.baseUrl + 'EditAd/EventTicketUpdate', {
            id: ticketDetails.adId, eventTicketViewModel: ticketDetails
        });
    }

    $paramount.EventService = EventService;
    return $paramount;

})(jQuery, $paramount);