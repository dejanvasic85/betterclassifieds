(function($, ko, $p) {
    'use strict';

    $p.models = $p.models || {};

    $p.models.BookingCategorySelection = function (options) {
        
        var self = this;
        self.inProgress = ko.observable(true);

        // Options
        self.parentCategoryOptions = ko.observableArray();
        self.childCategoryOptions = ko.observableArray();
        self.publicationOptions = ko.observableArray();

        // Values
        self.selectedParentCategory = ko.observable();
        self.selectedChildCategory = ko.observable(options.selectedChildCategory);

        // Load parent categories
        options.categorySvc.getParentCategories().done(function(data) {
            self.parentCategoryOptions.removeAll();
            $.each(data, function (idx, c) {
                var Category = new $p.models.Category(c.CategoryId, c.Title, c.IsOnlineOnly);
                self.parentCategoryOptions.push(Category);

                //if (options.selectedParentCategoryId !== undefined && options.selectedParentCategoryId === c.CategoryId) {
                //    debugger;
                //    self.selectedParentCategory(options.selectedParentCategoryId);
                //}
                self.selectedParentCategory(options.selectedParentCategoryId);
            });
        });
    };

    /*
     * Category model
     */
    $p.models.Category = function(id, title, isOnlineOnly) {
        this.id = ko.observable(id);
        this.title = ko.observable(title);
        this.isOnlineOnly = ko.observable(isOnlineOnly);
    };

    /*
     * Publication model
     */
    $p.models.Publication = function(id, title) {
        this.id = ko.observable(id);
        this.title = ko.observable(title);
    };

})(jQuery, ko, $paramount);