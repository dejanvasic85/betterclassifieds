(function (ko, $p) {

    ko.components.register('event-listing', {
        viewModel: EventListing,
        template: { path: $p.baseUrl + 'Scripts/app/events/eventListing/eventListing.html' }
    });

    function EventListing(params) {
        var listing = params.listing;
        this.adId = listing.adId;
        this.eventId = listing.eventId;
        this.eventName = listing.eventName;
        this.eventUrl = listing.eventUrl;
    }

    $p.models.EventListing = EventListing;

})(ko, $paramount);