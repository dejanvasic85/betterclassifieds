﻿/*
 * jQuery UI hooks to elements for editing details for an Event Ad
 */
(function ($, ko, $paramount) {

    var $eventEditor = $('#eventEditor');
    $paramount.ui = $paramount.ui || {};
    $paramount.ui.eventEditor = {
        init: function (options) {
            // onReady function
            $(function () {
                var eventDetailsModel;

                // Initally load the data
                $.getJSON(options.serviceEndpoint.getEventDetails).done(function (response) {
                    eventDetailsModel = new $paramount.models.EventAd(response, options);
                    ko.applyBindings(eventDetailsModel);
                });

                // Image upload handler
                $paramount.upload({
                    url: options.serviceEndpoint.uploadOnlineImage,
                    element: $eventEditor.find('#eventPhotoUpload'),
                    progressBar: $eventEditor.find('#eventPhotoUploadProgress'),
                    complete: function (documentId) {
                        eventDetailsModel.eventPhoto(documentId);
                        eventDetailsModel.eventPhotoUploadError(null);
                    },
                    error: function(errorMsg) {
                        eventDetailsModel.eventPhotoUploadError(errorMsg);
                    }
                });

                $eventEditor.find('#eventForm').on('submit', function (e) {
                    if ($(this).valid() === false) {
                        return;
                    }
                    eventDetailsModel.submitChanges().done(function(result) {
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