(function () {

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
        this.baseUrl = baseUrl;
    }

    EventService.prototype.startTicketOrder = function (order) {
        return post(this.baseUrl + 'Event/ReserveTickets', order);
    }

    EventService.prototype.bookTickets = function(ticketBookingDetails) {
        return post(this.baseUrl + 'Event/BookTickets', ticketBookingDetails);
    }

    $paramount.EventService = EventService;
    return $paramount;

})(jQuery, $paramount);