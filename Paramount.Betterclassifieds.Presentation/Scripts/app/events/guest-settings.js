(function(ko, $p) {
    
    $p.models.GuestSettings = function(data) {

        this.displayGuests = ko.observable(data.displayGuests);

    }

})(ko, $paramount);