(function ($p) {

    var routePrefix = $p.baseUrl + '/venues';

    $p.venueService = {
        getByVenueId: function (venueId) {
            return $p.httpGet(routePrefix + '/' + venueId);
        }
    };

})($paramount)