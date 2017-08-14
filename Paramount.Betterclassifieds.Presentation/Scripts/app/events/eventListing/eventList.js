(function (ko, $p) {
    
    $p.models.EventList = function(data) {
            
        //this.events = $p.ko.bindArray(data, function (listing) {
        //    return new $p.models.EventListing(listing);
        //});

        if (!Array.isArray(data)) {
            throw new Error('Expected data to be an array of events');
        }

        this.events = data;
    };

})(ko, $paramount);