/*
 * jQuery user interface components
 */
(function ($, ko, $paramount) {

    $paramount.event = {
        /*
         * Prepares the user interface for designing event details
         * @/// <param name="eventModel" type="EventAd">Knockout object instance</param>
         */
        init: function (options) {

            $(function () {

                // Setup the UI 
                var $map = $('#Location')
                     .geocomplete({
                         map: '#LocationMap',
                         markerOptions: {
                             draggable: false
                         },
                     })
                     .bind('geocode:result', function () {
                         $('#LocationMap').show();
                     });

                // Setup the knockout object, initalise based on the existing server object
                $.getJSON(options.serviceEndpoint.getEventDetails).done(function (response) {
                    var eventDetails = new $paramount.models.EventAd(response, options);
                    ko.applyBindings(eventDetails);

                    if (response.Location) {
                        $map.geocomplete('find', response.Location);
                    }
                });
            });
        }
    }

})(jQuery, ko, $paramount);