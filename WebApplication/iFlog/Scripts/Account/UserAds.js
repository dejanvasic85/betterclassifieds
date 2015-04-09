(function($models, ko, $) {

    $models.UserAdsView = function(data) {

        var self = this;
        self.selectedStatus = ko.observable('All');
        self.ads = ko.observableArray();
        $.each(data, function(idx, item) {
            var ad = new $models.UserAd(item);
            self.ads.push(ad);
        });
        self.setStatus = function(item, val) {
            self.selectedStatus(val.currentTarget.attributes["data-status"].value);
        }
    };

    $models.UserAd = function (item) {
        var self = this;
        self.adId = ko.observable(item.AdId);
        self.status = ko.observable(item.Status);
        this.heading = ko.observable(item.Heading);
        this.description = ko.observable(item.Description);
        this.adImageId = ko.observable(item.AdImageId);
        this.starts = ko.observable(item.Starts);
        this.ends = ko.observable(item.Ends);
        this.visits = ko.observable(item.Visits);
        this.messages = ko.observable(item.Messages);

        this.cancelAd = function() {
            console.log('todo - cancel ad ' + self.adId());
        }

    };

})($paramount.models || {}, ko, jQuery);