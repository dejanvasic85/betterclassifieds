(function ($, ko, $paramount) {

    $paramount.ui = $paramount.ui || {};
    $paramount.ui.home = {
        init: function () {

            var $linksRootEl = $('.categories-links');
            var baseUrl = $paramount.baseUrl;
            var categoryService = new $paramount.CategoryService(baseUrl);

            categoryService.getParentCategories().then(function (data) {
                var vm = new $paramount.models.CategoryLinks(data);
                ko.applyBindings(vm, $linksRootEl.get(0));
            });

            // Data-bind latest ads
            var $eventList = $('#eventList');

            if ($eventList.length > 0) {

                var eventService = new $paramount.EventService(baseUrl);
                var query = new $paramount.EventQuery().withMax(10)
                    .withUser('dejan.vasic').build();

                eventService.searchEvents(query).then(function (response) {
                    
                    if (response.errors) {
                        return;
                    }

                    var eventList = new $paramount.models.EventList(response);
                    
                    ko.applyBindings(eventList, $eventList.get(0));

                });
            }
        }
    }
    

})(jQuery, ko, $paramount);