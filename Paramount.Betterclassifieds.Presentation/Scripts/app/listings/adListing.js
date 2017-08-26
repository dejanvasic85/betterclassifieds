(function (ko, $p) {

    ko.components.register('ad-listing', {
        viewModel: AdListing,
        template: { path: $p.baseUrl + 'Scripts/app/listings/adListing.html' }
    });

    var imgService = new $p.ImageService($p.baseUrl);

    function AdListing(params) {

        var listing = params.listing;
        this.adId = listing.adId;
        this.title = listing.title;
        this.shortTitle = listing.shortTitle;
        this.adUrl = listing.adUrl;
        this.editAdUrl = $p.baseUrl + 'editad/details/' + listing.adId;
        this.category = listing.categoryName;
        this.parentCategory = listing.parentCategoryName;
        this.startDate = $p.dateToDisplay(listing.startDate);
        this.startDateHumanized = listing.startDateHumanized;
        this.locationName = listing.locationName;
        this.locationAreaName = listing.locationAreaName;
        this.categoryFontIcon = "fa fa-5x fa fa-" + listing.categoryFontIcon;
        this.photo = getPrimaryPhoto(listing, params);
        this.userEnabled = params.user === true;
    }

    function getPrimaryPhoto(listing, params) {

        if (!listing.primaryImage)
            return null;

        return imgService.getImageUrl(listing.primaryImage, {
            w: params.imgHeight || 500,
            h: params.imgWidth || 300
        });

    }

    $p.models.AdListing = AdListing;

})(ko, $paramount);