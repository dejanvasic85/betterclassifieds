(function($p, ko, $) {
    
    // Assign the models/classes to the paramount models namespace
    $p.models = $p.models || {};
    $p.models.UserAdsView = UserAdsView;
    $p.models.UserAd = UserAd;
    $p.models.UserEnquiry = UserEnquiry;

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

        self.setStatus = function(item, val) {
            self.selectedStatus(val.currentTarget.attributes["data-status"].value);
        };

        self.setSelectedAd = function(id) {
            self.selectedAd(id);
        };
        
        self.confirmCancel = function() {
            var itemToRemove = ko.utils.arrayFirst(self.ads(), function (item) {
                return item.adId() == self.selectedAd();
            });
            itemToRemove.status('Expired');
            //self.ads.remove(itemToRemove);
        };
    };

    function UserAd(item) {
        var self = this;
        self.adId = ko.observable(item.AdId);
        self.status = ko.observable(item.Status);

        this.totalPrice = ko.observable(item.TotalPrice);
        this.heading = ko.observable(item.Heading);
        this.description = ko.observable(item.Description);
        this.adImageId = ko.observable(item.AdImageId);
        this.starts = ko.observable(item.Starts);
        this.ends = ko.observable(item.Ends);
        this.visits = ko.observable(item.Visits);
        this.messageCount = ko.observable(item.MessageCount);

        self.messages = ko.observableArray([]);
        $.each(item.Messages, function(idx, value) {
            self.messages.push(new $p.models.UserEnquiry(value));
        });

        this.cancelAd = function() {
            // See the jQuery handler in the page instead. This is a placeholder to allow the click to wire up
        }

        this.bookAgain = function() {
            // See the jQuery handler in the page instead. This is a placeholder to allow the click to wire up
        }
    };

    function UserEnquiry(item) {
        this.fullName = ko.observable(item.FullName);
        this.email = ko.observable(item.Email);
        this.enquiryText = ko.observable(item.Question);
        this.createdDate = ko.observable(item.CreatedDate);
    };

  
    

})($paramount, ko, jQuery);