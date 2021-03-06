﻿(function ($, $p) {

    function CategoryService(baseUrl) {
        this.baseUrl = baseUrl || $p.baseUrl;
    }

    CategoryService.prototype.getParentCategories = function () {
        return $.ajax({ url: this.baseUrl + 'Categories/GetCategories' });
    }

    CategoryService.prototype.getChildCategories = function (parentId) {
        return $.ajax({ url: this.baseUrl + 'Categories/GetCategories?parentId=' + parentId });
    }

    CategoryService.prototype.isOnlineOnly = function (id) {
        return $p.httpPost(this.baseUrl + 'Categories/IsOnlineOnlyCategory', { id: id });
    }

    $p.CategoryService = CategoryService;

})(jQuery, $paramount);