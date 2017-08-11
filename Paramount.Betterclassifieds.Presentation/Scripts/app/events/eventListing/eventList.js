(function (ko, $p) {
    
    $p.models.EventList = function(data) {
            
        this.events = $p.ko.bindArray(data, function (listing) {
            return new $p.models.EventListing(listing);
        });

    };

})(ko, $paramount);