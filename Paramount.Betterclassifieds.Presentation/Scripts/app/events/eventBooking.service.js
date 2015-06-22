(function($, ko, $paramount) {

    $paramount.EventBookingService = EventBookingService;


    // Constructor
    function EventBookingService() {
        this.endpoints = $paramount.url;
    }
    

})(jQuery, ko, $paramount);