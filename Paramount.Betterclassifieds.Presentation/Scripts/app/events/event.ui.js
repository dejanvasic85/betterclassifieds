/*
 * jQuery user interface components
 */
(function ($, ko, $paramount) {

    $paramount.event = {
        init: function (options) {
            // onReady function
            $(function () {
                $.getJSON(options.ServiceEndpoint.getEventDetails).done(function (response) {
                    var eventDetails = new $paramount.models.EventAd(response, options);
                    ko.applyBindings(eventDetails);
                });
            });
        }
    }

})(jQuery, ko, $paramount);