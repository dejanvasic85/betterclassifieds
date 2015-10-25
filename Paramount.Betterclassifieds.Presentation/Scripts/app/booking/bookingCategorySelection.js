(function ($, ko, $p) {
    'use strict';

    $p.models = $p.models || {};

    $p.models.BookingCategorySelection = function (stepData) {
        var self = this;
        self.categoryId = ko.observable(stepData.categoryId);
        self.subCategoryId = ko.observable(stepData.subCategoryId);
        self.publications = ko.observableArray(stepData.publications);
        self.shouldShowSubCategory = ko.computed(function () {
            return self.categoryId() !== "";
        });
        self.shouldShowPublications = ko.observable(!stepData.isOnlineOnly);
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
    }

    $p.booking = $p.booking || {};
    $p.booking.stepOne = function (contract) {
        var categorySelection = new $p.models.BookingCategorySelection(contract);
        ko.applyBindings(categorySelection);

        $('#parentCategoryId').on('change', function () {
            var me = $(this);
            categorySelection.subCategoryId = ko.observable(null); // clear
            if (me.val() === '') {
                return;
            }
            $('#subCategoryId').loadSubCategories(me.val(), false);

            var categoryService = new $p.CategoryService();
            categoryService.isOnlineOnly(me.val()).done(function (response) {
                categorySelection.shouldShowPublications(!response.isOnlineOnly);
            });
        });


        $('#btnSubmit').on('click', function () {
            var $btn = $(this);
            $btn.button('loading');
            var modelToPost = ko.toJS(categorySelection);
            if (categorySelection.shouldShowPublications() === false) {
                $.each(modelToPost.publications, function (idx, p) {
                    p.isSelected = false;
                });
            }

            // Validate
            if (isNaN(categorySelection.subCategoryId()) || categorySelection.subCategoryId() === null) {
                categorySelection.errorMsg('You must select a sub category for your ad');
                $(this).button('reset');
                return;
            }

            $.ajax({
                url: $p.url.adBooking.stepOne,
                data: JSON.stringify(modelToPost),
                contentType: 'application/json',
                type: 'POST',
                dataType: 'json',
                success: function (respondUrl) {
                    window.location = respondUrl;
                },
                error: function (resp) {
                    categorySelection.errorMsg(resp.statusText);
                    $btn.button('reset');
                }
            });
        });

    };

})(jQuery, ko, $paramount);