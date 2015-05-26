(function($, ko, $p) {
    'use strict';

    $p.models = $p.models || {};

    $p.models.BookingCategorySelection = function (data) {
        
        var self = this;
        self.categoryId = ko.observable(data.categoryId);
        self.subCategoryId = ko.observable(data.subCategoryId);
        self.publications = ko.observableArray(data.publications);
        self.shouldShowSubCategory = ko.computed(function () {
            return self.categoryId() !== "";
        });
        self.togglePublication = function (pub) {
            if (!pub) {
                return;
            }

            if (pub.isSelected === 'undefined') {
                return;
            }

            pub.isSelected = !pub.isSelected;
        };
        self.errorMsg = ko.observable("");
    };
    
})(jQuery, ko, $paramount);