(function($models, ko, $) {

    $models.UserAdsView = function(data) {

        var self = this;
        self.selectedStatus = ko.observable('All');
        self.ads = ko.observableArray();
        self.userEnquiries = ko.observableArray([]);

        $.each(data, function (idx, item) {
            var ad = new $models.UserAd(item);
            self.ads.push(ad);
        });

        self.setStatus = function(item, val) {
            self.selectedStatus(val.currentTarget.attributes["data-status"].value);
        };
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
        this.messageCount = ko.observable(item.MessageCount);

        self.messages = ko.observableArray([]);
        $.each(item.Messages, function(idx, value) {
            self.messages.push(new $models.UserEnquiry(value));
        });

        this.cancelAd = function() {
            
        }
    };

    $models.UserEnquiry = function(item) {
        this.fullName = ko.observable(item.FullName);
        this.email = ko.observable(item.Email);
        this.enquiryText = ko.observable(item.Question);
        this.createdDate = ko.observable(item.CreatedDate);
    };

})($paramount.models || {}, ko, jQuery);