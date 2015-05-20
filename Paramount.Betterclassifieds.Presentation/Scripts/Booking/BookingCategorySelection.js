(function($p) {
    'use strict';

    $p.models = $p.models || {};

    $p.models.BookingCategorySelection = function (options) {
        
        var self = this;
        self.inProgress = ko.observable(true);
        self.parentCategoryOptions = ko.observableArray();
        self.childCategoryOptions = ko.observableArray();
        self.publicationOptions = ko.observableArray();

        // Load parent categories
        options.categorySvc.getParentCategories().done(function(data) {
            debugger;
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