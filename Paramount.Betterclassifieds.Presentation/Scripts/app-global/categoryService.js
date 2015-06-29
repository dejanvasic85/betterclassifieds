(function ($, $p) {

    $p.categorySvc = {
        getParentCategories: getParentCategories,
        getChildCategories : getChildCategories
    }

    function getParentCategories() {
        return $.ajax({ url: $p.url.categories.get() });
    }

    function getChildCategories(parentId) {
        return $.ajax({ url: $p.url.categories.get(parentId) });
    }

})(jQuery, $paramount);