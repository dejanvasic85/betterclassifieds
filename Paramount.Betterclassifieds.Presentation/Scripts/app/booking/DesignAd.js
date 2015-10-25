(function ($, $paramount, ko) {

    $paramount.models = $paramount.models || {};

    $paramount.models.DesignAd = function (options) {

        var adService = options.adService,
            onlineImages = options.onlineImages,
            maxImages = options.maxImages,
            lineAd = options.lineAd,
            updateRates = options.updateRates,
            imageService = new $paramount.ImageService();

        var self = this;

        // Online Images
        self.adImages = ko.observableArray(onlineImages);
        self.getImageUrl = function (id) {
            return imageService.getImageUrl(id);
        }
        self.errorMsg = ko.observable("");
        self.removeImage = function (img) {
            adService.removeOnlineAdImage(img)
                .done(function (result) {
                    if (result) {
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
        if (lineAd !== undefined && lineAd !== null) {

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

            self.lineAdImageId.subscribeChanged(function (oldValue, newValue) {
                if (newValue !== '') {
                    adService.assignPrintImg(newValue); // Calls the service to set the value
                } else {
                    adService.removePrintImg(oldValue);
                }
            }, this);

            self.removePrintImage = function () {
                self.lineAdImageId("");
            }
            self.lineAdImgUrl = ko.computed(function() {
                if (self.lineAdImageId() === 'undefined' || self.lineAdImageId() === '') {
                    return null;
                }
                return imageService.getImageUrl(self.lineAdImageId());
            });
        }


        // Editions
        self.publicationEditions = ko.observableArray([]);

        // Prices
        if (updateRates === true) {
            self.pricetotal = ko.observable();
            self.publicationPrices = ko.observableArray([]);
            self.onlineItemPrices = ko.observableArray([]);
            self.calculate = ko.computed(function () {
                var factors = {}
                
                if (lineAd != null) {
                    factors.lineAdText = self.lineAdText();
                    factors.lineAdHeader = self.lineAdHeader();
                    factors.usePhoto = self.lineAdImageId();
                    factors.editions = self.printInsertions();
                }

                adService.updateBookingRates(factors).done(function (resp) {
                    // Map Total
                    self.pricetotal('Total: ' + $paramount.formatCurrency(resp.bookingTotal));

                    // Map online line items
                    self.onlineItemPrices.removeAll();
                    $.each(resp.onlinePrice.items, function (index, serverItem) {
                        self.onlineItemPrices.push(new OnlineLineItem(serverItem));
                    });

                    // Map publications 
                    self.publicationPrices.removeAll();
                    if (resp.publicationPrices !== undefined && resp.publicationPrices !== null) {

                        $.each(resp.publicationPrices, function (index, serverItem) {
                            var publicationPrice = new PublicationPrice(serverItem);
                            if (publicationPrice.total > 0) {
                                self.publicationPrices.push(publicationPrice);
                            }
                        });

                    }
                });

            }).extend({ throttle: 1000 });
        }


    };

    var OnlineLineItem = function (serverItem) {
        this.name = serverItem.name;
        this.price = $paramount.formatCurrency(serverItem.price);
        this.quantity = serverItem.quantity;
        this.itemTotal = $paramount.formatCurrency(serverItem.itemTotal);
    };

    var PublicationPrice = function (serverItem) {
        var me = this;
        me.publicationName = serverItem.publication;
        me.total = serverItem.publicationTotal;
        me.publicationTotal = $paramount.formatCurrency(serverItem.publicationTotal);
        me.lineItems = ko.observableArray([]);
        $.each(serverItem.items, function (index, lineItem) {
            me.lineItems.push(new PrintLineItem(lineItem));
        });
    };

    var PrintLineItem = function (serverItem) {
        this.name = serverItem.name;
        this.price = $paramount.formatCurrency(serverItem.price);
        this.quantity = serverItem.quantity;
        this.editions = serverItem.editions;
        this.itemTotal = $paramount.formatCurrency(serverItem.itemTotal);
    };

})(jQuery, $paramount, ko);