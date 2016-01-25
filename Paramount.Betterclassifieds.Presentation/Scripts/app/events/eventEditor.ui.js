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
                adDesignService.getCurrentEventDetails().then(function (response) {
                    eventDetailsModel = new $paramount.models.EventAd(response, options);
                    ko.applyBindings(eventDetailsModel);
                });

                var $printImg = $('#imgCropContainer > img');
                var eventPhotoFileName;

                // Image upload handler
                $paramount.upload({
                    url: imageService.uploadCropImageUrl(),
                    element: $eventEditor.find('#eventPhotoUpload'),
                    progressBar: $eventEditor.find('#eventPhotoUploadProgress'),
                    complete: function (documentId) {
                        eventPhotoFileName = documentId;
                        $('#printCropImg').attr('src', imageService.renderCropImageUrl(documentId));
                        $('#printImageCropDialog').modal('show');

                        //adDesignService.assignOnlineImage(documentId).done(function () {
                        //    eventDetailsModel.eventPhoto(documentId);
                        //    eventDetailsModel.eventPhotoUploadError(null);
                        //});
                    },
                    error: function (errorMsg) {
                        eventDetailsModel.eventPhotoUploadError(errorMsg);
                    }
                });

                $('#printImageCropDialog').on('shown.bs.modal', function () {
                    $printImg.cropper({
                        aspectRatio: 639 / 251,
                        modal: true,
                        dashed: false
                    });
                });

                $('#printImageCropDialog').on('hide.bs.modal', function (e) {
                    $printImg.cropper('destroy');
                });

                $('#btnCancelCropping').on('click', function () {
                    if (eventPhotoFileName) {
                        imageService.cancelCropImage(eventPhotoFileName);
                    }
                });

                $('#btnDoneCropping').on('click', function () {
                    var $btn = $(this);
                    $btn.button('loading');
                    var data = $printImg.cropper("getData");
                    data.documentId = eventPhotoFileName;
                    imageService
                        .cropImage(eventPhotoFileName, data.x, data.y, data.width, data.height, false)
                        .then(function (documentId) {
                            $btn.button('reset');
                            $('#printImageCropDialog').modal('hide');
                            adDesignService.assignOnlineImage(documentId).then(function () {
                                debugger;
                                eventDetailsModel.eventPhoto(documentId);
                                eventDetailsModel.eventPhotoUploadError(null);
                            });
                        });
                });

                $eventEditor.find('#eventForm').on('submit', function (e) {
                    // Todo - refactor this crap and use knockout validation instead
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