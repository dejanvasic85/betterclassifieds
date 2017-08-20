(function ($, ko, $paramount) {

    $paramount.ui = $paramount.ui || {};
    $paramount.ui.home = {
        init: function () {

            var baseUrl = $paramount.baseUrl;

            var $linksRootEl = $('.categories-links');
            if ($linksRootEl.length > 0) {
                var categoryService = new $paramount.CategoryService(baseUrl);

                categoryService.getParentCategories().then(function (data) {
                    var vm = new $paramount.models.CategoryLinks(data);
                    ko.applyBindings(vm, $linksRootEl.get(0));
                });
            }

            var $eventList = $('#eventList');
            if ($eventList.length > 0) {
                ko.applyBindings({}, $eventList.get(0));
            }

            var $adList = $('#adList');
            if ($adList.length > 0) {
                ko.applyBindings({}, $adList.get(0));
            }
        }
    }


})(jQuery, ko, $paramount);