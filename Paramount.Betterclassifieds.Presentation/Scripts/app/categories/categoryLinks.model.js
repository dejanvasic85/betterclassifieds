'use strict';

(function ($, ko, $paramount) {

    $paramount.models = $paramount.models || {};
    $paramount.models.CategoryLinks = function (parentCategories) {
        var me = this;
        me.parentCategories = ko.observableArray();
        _.each(parentCategories, function (c) {
            c.fontIcon = 'fa fa-' + c.fontIcon;
            me.parentCategories.push(c);
        });

        me.searchLink = function(category) {
            return $paramount.baseUrl + 'Listings/Find?categoryId=' + category.categoryId;
        }
    }
 
})(jQuery, ko, $paramount);