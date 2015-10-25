(function ($, $paramount) {

    function LocationService(baseUrl) {
        this.baseUrl = baseUrl || $paramount.baseUrl;
    }

    LocationService.prototype.getLocationAreas = function (locationId) {
        return $.get(this.baseUrl + 'Location/GetLocationAreas?locationId=' + locationId);
    }
    
    $paramount.LocationService = LocationService;
    return $paramount;

})(jQuery, $paramount);