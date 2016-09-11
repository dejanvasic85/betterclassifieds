(function ($p, ko, $) {

    function UserAdsView(data) {

        var me = this;
        me.selectedStatus = ko.observable('All');
        me.ads = ko.observableArray();
        me.userEnquiries = ko.observableArray([]);
        me.selectedAd = ko.observable();

        $.each(data, function (idx, item) {
            var ad = new $p.models.UserAd(item);
            me.ads.push(ad);
        });

        me.setStatus = function (item, val) {
            me.selectedStatus(val.currentTarget.attributes["data-status"].value);
        };

        me.setSelectedAd = function (id) {
            me.selectedAd(id);
        };

        me.confirmCancel = function () {
            var itemToRemove = ko.utils.arrayFirst(me.ads(), function (item) {
                return item.adId() == me.selectedAd();
            });
            itemToRemove.status('Expired');
        };

        me.noAdsAvaialble = ko.computed(function () {
            return me.ads().length === 0;
        });
    };

    function UserAd(item) {
        var userAdService = new $p.UserAdService(),
            imageService = new $p.ImageService();
        var me = this;
        me.adId = ko.observable(item.adId);
        me.status = ko.observable(item.status);

        this.totalPrice = ko.observable(item.totalPrice);
        this.heading = ko.observable(item.heading);
        this.description = ko.observable(item.description);
        this.adImageId = ko.observable(item.adImageId);
        this.adPublishedDateDescription = ko.observable(item.adPublishedDateDescription);
        this.visits = ko.observable(item.visits);
        this.messageCount = ko.observable(item.messageCount);
        this.fontIcon = ko.observable('fa fa-5x fa-' + item.categoryFontIcon + ' thumbnail');
        this.parentCategoryName = ko.observable(item.parentCategoryName);
        this.categoryName = ko.observable(item.categoryName);

        me.messages = ko.observableArray([]);
        $.each(item.messages, function (idx, value) {
            me.messages.push(new $p.models.UserEnquiry(value));
        });

        this.cancelAd = function () {
            // See the jQuery handler in the page instead. This is a placeholder to allow the click to wire up
        }

        this.bookAgain = function () {
            // See the jQuery handler in the page instead. This is a placeholder to allow the click to wire up
        }


        this.bookingInvoiceHref = userAdService.getInvoiceUrl(item.adId);
        this.viewHref = item.adViewUrl;
        this.imageUrl = imageService.getImageUrl(item.adImageId);
        this.imageUrlSmaller = imageService.getImageUrl(item.adImageId, { h: 50, w: 50 });
        this.categoryAdType = ko.observable(item.categoryAdType);
        this.editHref = ko.observable();
        switch (item.categoryAdType) {
            case $paramount.CATEGORY_AD_TYPE.EVENT:

                this.editHref(userAdService.getEventDashboardUrl(item.adId));
                break;

            default:
                this.editHref = ko.observable(userAdService.getEditUrl(item.adId));
                break;
        }


    };

    $p.models = $p.models || {};
    $p.models.UserAd = UserAd;

    // Assign the models/classes to the paramount models namespace
    $p.models = $p.models || {};
    $p.models.UserAdsView = UserAdsView;

})($paramount, ko, jQuery);