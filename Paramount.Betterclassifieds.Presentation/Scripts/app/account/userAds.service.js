(function ($, $paramount) {

    function UserAdService(baseUrl) {
        this.baseUrl = baseUrl || $paramount.baseUrl;
    }
    
    UserAdService.prototype.cancelAd = function (adId) {
        return $paramount.httpPost(this.baseUrl + 'UserAds/Cancel', { adId: adId });
    }

    UserAdService.prototype.startNewBookingFromTemplate = function (adId) {
        return $paramount.httpPost(this.baseUrl + 'Booking/StartFromTemplate', { id: adId });
    }
    
    UserAdService.prototype.getInvoiceUrl = function (adId) {
        return this.baseUrl + 'Invoice/Booking/' + adId;
    }
    
    $paramount.UserAdService = UserAdService;
    return $paramount;

})(jQuery, $paramount);