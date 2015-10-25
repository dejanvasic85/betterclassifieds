(function ($, $paramount) {

    function UserAdService(baseUrl) {
        this.baseUrl = baseUrl || $paramount.baseUrl;
    }

    UserAdService.prototype.getEditUrl = function (adId) {
        return this.baseUrl + 'EditAd/Details/' + adId;
    }

    UserAdService.prototype.getInvoiceUrl = function (adId) {
        return this.baseUrl + 'Invoice/Booking/' + adId;
    }

    UserAdService.prototype.getViewUrl = function (adId) {
        return this.baseUrl + 'Listings/ViewAd/' + adId;
    }

    $paramount.UserAdService = UserAdService;
    return $paramount;

})(jQuery, $paramount);