(function ($p, ko, $) {

    function UserAdsView(data) {

        var self = this;
        self.selectedStatus = ko.observable('All');
        self.ads = ko.observableArray();
        self.userEnquiries = ko.observableArray([]);
        self.selectedAd = ko.observable();

        $.each(data, function (idx, item) {
            var ad = new $p.models.UserAd(item);
            self.ads.push(ad);
        });

        self.setStatus = function (item, val) {
            self.selectedStatus(val.currentTarget.attributes["data-status"].value);
        };

        self.setSelectedAd = function (id) {
            self.selectedAd(id);
        };

        self.confirmCancel = function () {
            var itemToRemove = ko.utils.arrayFirst(self.ads(), function (item) {
                return item.adId() == self.selectedAd();
            });
            itemToRemove.status('Expired');
        };

        self.noAdsAvaialble = ko.computed(function() {
            return self.ads().length === 0;
        });
    };

    function UserAd(item) {
        var userAdService = new $p.UserAdService(),
            imageService = new $p.ImageService();
        var self = this;
        self.adId = ko.observable(item.adId);
        self.status = ko.observable(item.status);

        this.totalPrice = ko.observable(item.totalPrice);
        this.heading = ko.observable(item.heading);
        this.description = ko.observable(item.description);
        this.adImageId = ko.observable(item.adImageId);
        this.starts = ko.observable(item.starts);
        this.ends = ko.observable(item.ends);
        this.visits = ko.observable(item.visits);
        this.messageCount = ko.observable(item.messageCount);

        self.messages = ko.observableArray([]);
        $.each(item.messages, function (idx, value) {
            self.messages.push(new $p.models.UserEnquiry(value));
        });

        this.cancelAd = function () {
            // See the jQuery handler in the page instead. This is a placeholder to allow the click to wire up
        }

        this.bookAgain = function () {
            // See the jQuery handler in the page instead. This is a placeholder to allow the click to wire up
        }

        this.editHref = userAdService.getEditUrl(item.adId);
        this.bookingInvoiceHref = userAdService.getInvoiceUrl(item.adId);
        this.viewHref = item.adViewUrl;
        this.imageUrl = imageService.getImageUrl(item.adImageId);
        this.imageUrlSmaller = imageService.getImageUrl(item.adImageId, { h: 50, w: 50 });

        this.isEventAd = item.categoryAdType === $paramount.CATEGORY_AD_TYPE.EVENT;
        this.editEventUrl = this.isEventAd === true ? userAdService.getEditEventUrl(item.adId) : '';
    };

    $p.models = $p.models || {};
    $p.models.UserAd = UserAd;

    // Assign the models/classes to the paramount models namespace
    $p.models = $p.models || {};
    $p.models.UserAdsView = UserAdsView;

})($paramount, ko, jQuery);