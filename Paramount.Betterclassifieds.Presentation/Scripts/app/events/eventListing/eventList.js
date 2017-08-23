(function (ko, $p) {

    var eventService = new $p.EventService($p.baseUrl);

    $p.models.EventList = function (params) {
        var me = this;
        me.events = ko.observableArray();
        me.userEnabled = params.user && params.user === true;

        var query = new $p.EventQuery()
            .withMax(params.maxItems)
            .withUser(params.user)
            .build();

        eventService.searchEvents(query).then(function (response) {
            if (response.errors) {
                return;
            }

            if (!Array.isArray(response)) {
                throw new Error("The response does not contain an array of events.");
            }

            _.each(response, function (item) {
                me.events.push(item);
            });
        });
    };

    ko.components.register('event-list', {
        viewModel: $p.models.EventList,
        template: { path: $p.baseUrl + 'Scripts/app/events/eventListing/eventList.html' }
    });


})(ko, $paramount);