(function (ko, $p) {

    ko.components.register('ad-listing', {
        viewModel: AdListing,
        template: { path: $p.baseUrl + 'Scripts/app/listings/adListing.html' }
    });

    var imgService = new $p.ImageService($p.baseUrl);

    function AdListing(params) {
        
        var listing = params.listing;
        
        this.adId = listing.adId;
        this.adName = listing.heading;
        this.adShortName = listing.adShortName;
        this.adUrl = listing.adUrl;
        this.category = listing.categoryName;
        this.parentCategory = listing.parentCategoryName;
        this.startDate = $p.dateToDisplay(listing.adStartDate);
        this.startDateHumanized = listing.startDateHumanized;
        this.location = listing.location;
        this.categoryFontIcon = "fa fa-5x fa fa-" + listing.categoryFontIcon;
        this.photo = null;
        
        if (listing.primaryImage) {
            this.photo = imgService.getImageUrl(listing.primaryImage, {
                w: params.imgHeight || 500,
                h: params.imgWidth || 300
            });
        }
    }

    $p.models.adListing = adListing;

})(ko, $paramount);