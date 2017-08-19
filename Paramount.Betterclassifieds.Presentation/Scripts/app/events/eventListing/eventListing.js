(function (ko, $p) {

    ko.components.register('event-listing', {
        viewModel: EventListing,
        template: { path: $p.baseUrl + 'Scripts/app/events/eventListing/eventListing.html' }
    });

    var imgService = new $p.ImageService($p.baseUrl);

    function EventListing(params) {
        
        var listing = params.listing;

        this.adId = listing.adId;
        this.eventId = listing.eventId;
        this.eventName = listing.heading;
        this.eventShortName = listing.eventShortName;
        this.eventUrl = listing.eventUrl;
        this.category = listing.categoryName;
        this.parentCategory = listing.parentCategoryName;
        this.shortDescription = listing.shortDescription;
        this.startDate = $p.dateToDisplay(listing.eventStartDate);
        this.startDateHumanized = listing.startDateHumanized;
        this.endDate = listing.endDate;
        this.location = listing.location.replace(', Australia', '');
        this.categoryFontIcon = "fa fa-5x fa fa-" + listing.categoryFontIcon;
        this.photo = null;
        this.ticketPriceRange = null;
        this.ticketInfo = null;

        if (listing.hasTickets === true) {
            if (listing.areAllTicketsFree === true) {
                this.ticketInfo = 'Free';
            } else {
                this.ticketInfo = $paramount.formatCurrency(listing.cheapestTicket);

                if (listing.cheapestTicket !== listing.mostExpensiveTicket) {
                    this.ticketInfo += ' - ' + $paramount.formatCurrency(listing.mostExpensiveTicket);
                }
            }
        }


        if (listing.primaryImage) {
            this.photo = imgService.getImageUrl(listing.primaryImage, {
                w: params.imgHeight || 500,
                h: params.imgWidth || 300
            });
        }
    }

    $p.models.EventListing = EventListing;

})(ko, $paramount);