(function ($, $paramount) {

    function UserAdService(baseUrl) {
        this.baseUrl = baseUrl || $paramount.baseUrl;
    }

    UserAdService.prototype.getAdsForUser = function () {
        return $paramount.httpGet(this.baseUrl + 'UserAds/GetAdsForUser');
    }

    UserAdService.prototype.cancelAd = function (adId) {
        return $paramount.httpPost(this.baseUrl + 'UserAds/Cancel', { adId: adId });
    }

    UserAdService.prototype.startNewBookingFromTemplate = function (adId) {
        return $paramount.httpPost(this.baseUrl + 'Booking/StartFromTemplate', { id: adId });
    }

    UserAdService.prototype.getEditUrl = function (adId) {
        return this.baseUrl + 'EditAd/Details/' + adId;
    }

    UserAdService.prototype.getInvoiceUrl = function (adId) {
        return this.baseUrl + 'Invoice/Booking/' + adId;
    }

    UserAdService.prototype.getEditEventUrl = function(adId) {
        return this.baseUrl + 'EditAd/Event/' + adId;
    }

    $paramount.UserAdService = UserAdService;
    return $paramount;

})(jQuery, $paramount);