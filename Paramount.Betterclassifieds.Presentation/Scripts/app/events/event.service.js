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
        return post(this.baseUrl + 'Event/ReserviceTickets', order);
    }

    $paramount.EventService = EventService;
    return $paramount;

})(jQuery, $paramount);