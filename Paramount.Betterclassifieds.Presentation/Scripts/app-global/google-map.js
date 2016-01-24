(function ($, google) {

    if (!google) {
        return;
    }

    if (!google.maps) {
        return;
    }

    $(function () {

        /*
         * Renders the google map on the required div marked with .google-map class
         * Required attributes: 
         * data-langitude
         * data-longitude
         * data-address (for displaying tooltip marker)
         */
        var $map = $('.google-map');
        if ($map.length === 0) {
            return;
        }
        var data = $map.data();
        var mapCanvas = $map.get(0);
        function initialize() {
            var latLng = new google.maps.LatLng(data.latitude, data.longitude);
            var mapOptions = {
                center: latLng,
                zoom: 15,
                mapTypeId: google.maps.MapTypeId.ROADMAP,
                scrollwheel: false
            };
            var map = new google.maps.Map(mapCanvas, mapOptions);

            var marker = new google.maps.Marker({
                position: latLng,
                title: data.address,
                visible: true
            });
            marker.setMap(map);
        }
        initialize();
    });

})(jQuery, google);