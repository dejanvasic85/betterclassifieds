/*
 * jQuery user interface components
 */
(function ($, ko, $paramount) {

    var $eventEditor = $('#eventEditor');

    $paramount.event = {
        init: function (options) {
            // onReady function
            $(function () {
                var eventDetails;

                // Initally load the data
                $.getJSON(options.ServiceEndpoint.getEventDetails).done(function (response) {
                    eventDetails = new $paramount.models.EventAd(response, options);
                    ko.applyBindings(eventDetails);
                });

                // Image upload handler
                $paramount.upload({
                    url: options.ServiceEndpoint.uploadOnlineImage,
                    element: $eventEditor.find('#eventPhotoUpload'),
                    progressBar: $eventEditor.find('#eventPhotoUploadProgress'),
                    complete: function (documentId) {
                        eventDetails.eventPhoto(documentId);
                        eventDetails.eventPhotoUploadError(null);
                    },
                    error: function(errorMsg) {
                        eventDetails.eventPhotoUploadError(errorMsg);
                    }
                });

                $eventEditor.find('#eventForm').on('submit', function (e) {
                    if ($(this).valid() === false) {
                        return;
                    }
                    eventDetails.submitChanges().done(function(result) {
                        if (result.nextUrl) {
                            window.location = result.nextUrl;
                            return;
                        }
                        $eventEditor.find('#submitFailed').show();
                        $eventEditor.find('button').loading('reset');
                    });
                    e.preventDefault();
                });
            });
        }
    }

})(jQuery, ko, $paramount);