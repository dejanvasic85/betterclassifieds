(function ($, $p) {

    $p.categorySvc = function (endpoints) {

        this.endpoints = endpoints;

    };

    $p.categorySvc.prototype.getParentCategories = function () {
        return $.ajax({ url: endpoints.category.getParents });
    };

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