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

    function post(url, data) {
        return $.ajax({
            url: url,
            data: JSON.stringify(data),
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json'
        });
    }


})(jQuery, $paramount);