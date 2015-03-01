(function ($paramount, ko) {

    $paramount.models = $paramount.models || {};

    $paramount.models.DesignAd = function (data, maxImages, lineAdHeader, lineAdText, lineAdImageId) {
        var self = this;

        // Online Images
        self.adImages = ko.observableArray(data);
        self.errorMsg = ko.observable("");
        self.removeImage = function (img) {
            $paramount.svc.removeOnlineAdImageForBooking(img)
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
        self.lineAdHeader = ko.observable(lineAdHeader);
        self.lineAdText = ko.observable(lineAdText);
        self.wordCount = ko.computed(function () {
            if (self.lineAdText().length === 0) {
                return 0;
            }
            return self.lineAdText().split(' ').length;
        });
        self.lineAdImageId = ko.observable(lineAdImageId);
        self.removePrintImage = function () {
            $paramount.svc
                .removeLineAdImageForBooking(self.lineAdImageId())
                .complete(function () {
                    self.lineAdImageId("");
                });
        }

        // Prices
        self.pricetotal = ko.observable();
        self.priceitems = ko.observableArray([]);
        self.calculate = ko.computed(function () {

            $paramount.svc.updateBookingRates({
                lineAdText: self.lineAdText(),
                lineAdHeader: self.lineAdHeader()
            }).done(function (resp) {
                debugger;
                self.pricetotal($paramount.formatCurrency(resp.Total));
                self.priceitems.removeAll();
                $.each(resp.LineItems, function (prop, price) {
                    self.priceitems.push({ 'item': prop, 'price': $paramount.formatCurrency(price) });
                });
            });

        }).extend({ throttle: 1000 });
    };


})($paramount, ko);