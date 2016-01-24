/*
 * jQuery UI hooks to elements for editing details for an Event Ad
 */
(function ($, ko, $paramount) {

    var $eventEditor = $('#eventEditor');
    $paramount.ui = $paramount.ui || {};
    $paramount.ui.eventEditor = {
        init: function (options) {
            // onReady function
            $(function () {
                var eventDetailsModel,
                    adDesignService = options.adDesignService || new $paramount.AdDesignService(),
                    imageService = options.imageService || new $paramount.ImageService();

                $.extend(options, {
                    adDesignService: adDesignService,
                    imageService: imageService
                });

                // Initally load the data
                adDesignService.getCurrentEventDetails().done(function (response) {
                    eventDetailsModel = new $paramount.models.EventAd(response, options);
                    ko.applyBindings(eventDetailsModel);
                });

                // Image upload handler
                $paramount.upload({
                    url: imageService.getUploadOnlineImageUrl(),
                    element: $eventEditor.find('#eventPhotoUpload'),
                    progressBar: $eventEditor.find('#eventPhotoUploadProgress'),
                    complete: function (documentId) {
                        adDesignService.assignOnlineImage(documentId).done(function () {
                            eventDetailsModel.eventPhoto(documentId);
                            eventDetailsModel.eventPhotoUploadError(null);
                        });
                    },
                    error: function (errorMsg) {
                        eventDetailsModel.eventPhotoUploadError(errorMsg);
                    }
                });

                $eventEditor.find('#eventForm').on('submit', function (e) {
                    if ($(this).valid() === false
                        || eventDetailsModel.eventEndDateValidation() !== ''
                        || eventDetailsModel.eventEndTimeValidation() !== '') {
                        e.preventDefault();
                        $eventEditor.find('button').button('reset');
                        return;
                    }

                    eventDetailsModel.submitChanges().then(function (resp) {
                        if (resp.nextUrl) {
                            return;
                        }
                        $eventEditor.find('button').button('reset');
                    });
                    e.preventDefault();
                });
            });
        }
    }

})(jQuery, ko, $paramount);