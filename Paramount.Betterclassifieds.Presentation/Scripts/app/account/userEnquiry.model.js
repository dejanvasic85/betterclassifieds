(function($p, ko, $) {
    
   
    function UserEnquiry(item) {
        this.fullName = ko.observable(item.fullName);
        this.email = ko.observable(item.email);
        this.enquiryText = ko.observable(item.question);
        this.createdDate = ko.observable(item.createdDate);
    };

  
    // Assign the models/classes to the paramount models namespace
    $p.models = $p.models || {};
    $p.models.UserEnquiry = UserEnquiry;


})($paramount, ko, jQuery);