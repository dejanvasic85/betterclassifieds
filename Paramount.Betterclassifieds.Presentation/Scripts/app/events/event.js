/*
 * jQuery user interface components
 */
(function ($, $paramount) {

    $paramount.event = {
        /*
         * Prepares the user interface for designing event details
         * @/// <param name="eventModel" type="EventAd">Knockout object instance</param>
         */
        onReady: function (eventModel) {

            $(function () {
                console.log(eventModel);
                $('#Location')
                    .geocomplete({
                        map: '#LocationMap',
                        markerOptions: {
                            draggable: false
                        },
                    })
                    .bind('geocode:result', function (event, result) {
                        console.log(result);
                        $('#LocationMap').show();
                    });

            });
        }
    }

})(jQuery, $paramount);