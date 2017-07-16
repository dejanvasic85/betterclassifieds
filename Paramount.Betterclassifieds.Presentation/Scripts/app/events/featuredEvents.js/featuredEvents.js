(function (ko, $p) {

    ko.components.register('featured-events', {
        viewModel: function (params) {
            var eventListings = ko.observableArray();
            var eventService = new $p.EventService($p.baseUrl);
            eventService.getEvents().then(function () {
                eventListings.push(new $p.models.EventListing());
            });
        },
        template: { path: $p.baseUrl + 'Scripts/app/events/featuredEvents/featuredEvents.html' }
    });

})(ko, $paramount);