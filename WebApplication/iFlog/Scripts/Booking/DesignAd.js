(function ($paramount, ko) {

    $paramount.models = $paramount.models || {};

    $paramount.models.DesignAd = function (adService, data, maxImages, lineAd, updateRates) {

        var self = this;

        // The ad management service to use is injected
        self.svc = adService;

        // Online Images
        self.adImages = ko.observableArray(data);
        self.errorMsg = ko.observable("");
        self.removeImage = function (img) {
            self.svc.removeOnlineAdImage(img)
                .done(function (result) {
                    if (result.removed) {
                        self.adImages.remove(img);
                    }
                });
        };

        self.maxImages = ko.observable(maxImages);
        self.maxLimitNotReached = function () {
            return self.adImages().length < self.maxImages();
        };
        self.uploadImageInProgress = ko.observable(false);

        // Line Ad
        self.printInsertions = ko.observable(lineAd.printInsertions);
        self.lineAdHeader = ko.observable(lineAd.lineAdHeader);
        self.lineAdText = ko.observable(lineAd.lineAdText);
        self.wordCount = ko.computed(function () {
            if (self.lineAdText() == null || self.lineAdText().length === 0) {
                return 0;
            }
            return self.lineAdText().split(' ').length;
        });
        self.lineAdImageId = ko.observable(lineAd.lineAdImageId == null ? '' : lineAd.lineAdImageId);
        self.removePrintImage = function () {
            self.svc
                .removeLineAdImageForBooking(self.lineAdImageId())
                .complete(function () {
                    self.lineAdImageId("");
                });
        }

        // Editions
        self.publicationEditions = ko.observableArray([]);

        // Prices
        if (updateRates) {
            self.pricetotal = ko.observable();
            self.publicationPrices = ko.observableArray([]);
            self.onlineItemPrices = ko.observableArray([]);
            self.calculate = ko.computed(function () {
                self.svc.updateBookingRates({
                    lineAdText: self.lineAdText(),
                    lineAdHeader: self.lineAdHeader(),
                    usePhoto: self.lineAdImageId(),
                    editions: self.printInsertions()
                }).done(function (resp) {
                    // Map Total
                    self.pricetotal('Total: ' + $paramount.formatCurrency(resp.BookingTotal));

                    // Map online line items
                    self.onlineItemPrices.removeAll();
                    $.each(resp.OnlinePrice.Items, function (index, serverItem) {
                        self.onlineItemPrices.push(new OnlineLineItem(serverItem));
                    });

                    // Map publications 
                    self.publicationPrices.removeAll();
                    $.each(resp.PublicationPrices, function (index, serverItem) {
                        var publicationPrice = new PublicationPrice(serverItem);
                        if (publicationPrice.total > 0) {
                            self.publicationPrices.push(publicationPrice);
                        }
                    });
                });

            }).extend({ throttle: 1000 });
        }
    };

    var OnlineLineItem = function (serverItem) {
        this.name = serverItem.Name;
        this.price = $paramount.formatCurrency(serverItem.Price);
        this.quantity = serverItem.Quantity;
        this.itemTotal = $paramount.formatCurrency(serverItem.ItemTotal);
    };

    var PublicationPrice = function (serverItem) {
        var me = this;
        me.publicationName = serverItem.Publication;
        me.total = serverItem.PublicationTotal;
        me.publicationTotal = $paramount.formatCurrency(serverItem.PublicationTotal);
        me.lineItems = ko.observableArray([]);
        $.each(serverItem.Items, function (index, lineItem) {
            me.lineItems.push(new PrintLineItem(lineItem));
        });
    };

    var PrintLineItem = function (serverItem) {
        this.name = serverItem.Name;
        this.price = $paramount.formatCurrency(serverItem.Price);
        this.quantity = serverItem.Quantity;
        this.editions = serverItem.Editions;
        this.itemTotal = $paramount.formatCurrency(serverItem.ItemTotal);
    };

})($paramount, ko);