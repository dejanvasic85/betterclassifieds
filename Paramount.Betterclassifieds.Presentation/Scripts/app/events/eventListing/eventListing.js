(function (ko, $p) {

    ko.components.register('event-listing', {
        viewModel: function() {
            
        },
        template: { path: $p.baseUrl + 'Scripts/app/events/eventListing/eventListing.html' }
    });

    function EventListing(data) {
        ko.adId = data.adId;
        ko.eventId = data.eventId;
        ko.eventName = data.eventName;
    }

    $p.models.EventListing = EventListing;

})(ko, $paramount);