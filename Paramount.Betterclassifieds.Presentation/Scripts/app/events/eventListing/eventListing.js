(function (ko, $p) {

    ko.components.register('event-listing', {
        viewModel: EventListing,
        template: { path: $p.baseUrl + 'Scripts/app/events/eventListing/eventListing.html' }
    });

    var imgService = new $p.ImageService($p.baseUrl);

    function EventListing(params) {
        console.log(params);
        var listing = params.listing;

        this.adId = listing.adId;
        this.eventId = listing.eventId;
        this.eventName = listing.heading;
        this.eventUrl = listing.eventUrl;
        this.category = listing.categoryName;
        this.parentCategory = listing.parentCategoryName;
        this.shortDescription = listing.shortDescription;
        this.startDate = $p.dateToDisplay(listing.eventStartDate);
        this.endDate = listing.endDate;
        this.location = listing.location.replace(', Australia', '');
        this.categoryFontIcon = "fa fa-5x fa fa-" + listing.categoryFontIcon;
        this.photo = null;
        
        if (listing.primaryImage) {
            this.photo = imgService.getImageUrl(listing.primaryImage, {
                w: params.imgHeight || 500,
                h: params.imgWidth || 300
            });
        }
    }

    $p.models.EventListing = EventListing;

})(ko, $paramount);