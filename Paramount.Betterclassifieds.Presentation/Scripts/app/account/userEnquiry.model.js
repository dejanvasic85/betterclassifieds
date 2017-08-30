(function ($p, ko, $) {


    function UserEnquiry(item) {
        this.id = ko.observable(item.id);
        this.fullName = ko.observable(item.fullName);
        this.email = ko.observable(item.email);
        this.enquiryText = ko.observable(item.question);
        this.createdDate = ko.observable(item.createdDate);

        this.mailtoLink = ko.observable('mailto:' + item.email);
    };


    // Assign the models/classes to the paramount models namespace
    $p.models = $p.models || {};
    $p.models.UserEnquiry = UserEnquiry;


})($paramount, ko, jQuery);