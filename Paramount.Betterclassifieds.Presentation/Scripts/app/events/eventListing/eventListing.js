(function (ko, $p) {

    ko.components.register('event-listing', {
        viewModel: EventListing,
        template: { path: $p.baseUrl + 'Scripts/app/events/eventListing/eventListing.html' }
    });

    var imgService = new $p.ImageService($p.baseUrl);
    var eventService = new $p.EventService($p.baseUrl);

    function EventListing(params) {

        var listing = params.listing;
        console.log('user', params.user);
        this.adId = listing.adId;
        this.eventId = listing.eventId;
        this.eventName = listing.heading;
        this.eventShortName = listing.eventShortName;
        this.eventUrl = listing.eventUrl;
        this.category = listing.categoryName;
        this.parentCategory = listing.parentCategoryName;
        this.shortDescription = listing.shortDescription;
        this.startDate = $p.dateToDisplay(listing.eventStartDate);
        this.startDateHumanized = listing.eventStartDateHumanized;
        this.endDate = listing.endDate;
        this.location = listing.location.replace(', Australia', '');
        this.categoryFontIcon = "fa fa-5x fa fa-" + listing.categoryFontIcon;
        this.eventDashboardUrl = eventService.getEventDashboardUrl(this.adId);
        this.isPastEvent = $p.isUtcDateBeforeNow(listing.eventStartDateUtc);
        this.isComingSoon = this.isPastEvent === false;
        this.userEnabled = params.user === true;
        this.photo = getPhotoUrl(listing, params);
        this.ticketInfo = getTicketInfo(listing);
        this.isClosed = listing.isClosed;
    }

    function getPhotoUrl(listing, params) {
        if (!listing.primaryImage) {
            return null;
        }
        return imgService.getImageUrl(listing.primaryImage, {
            w: params.imgHeight || 500,
            h: params.imgWidth || 300
        });
    }

    function getTicketInfo(listing) {
        if (!listing.hasTickets) {
            return null;
        }

        if (listing.areAllTicketsFree === true) {
            return 'Free';
        }
        
        if (listing.isClosed === true) {    
            return 'Ticketing Closed';
        }

        var info = $paramount.formatCurrency(listing.cheapestTicket);
        if (listing.cheapestTicket !== listing.mostExpensiveTicket) {
            info += ' - ' + $paramount.formatCurrency(listing.mostExpensiveTicket);
        }
        return info;
    }

    $p.models.EventListing = EventListing;

})(ko, $paramount);