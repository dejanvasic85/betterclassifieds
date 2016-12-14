(function ($, ko, $paramount) {

    $paramount.ui = $paramount.ui || {};
    $paramount.ui.home = {
        init: function () {

            var $linksRootEl = $('.categories-links');
            var baseUrl = $paramount.baseUrl;
            var categoryService = new $paramount.CategoryService(baseUrl);
            
            categoryService.getParentCategories().then(function(data) { 
                var vm = new $paramount.models.CategoryLinks(data);
                ko.applyBindings(vm, $linksRootEl.get(0));
            });
        }
    }

})(jQuery, ko, $paramount);