(function ($, $p, $url) {

    var $adEditor = $('#editAdForm');

    $p.editAd = {
        init: function (designAdModel, adService) {

            var imageService = new $p.ImageService();

            $(function () {
                // Set online editor
                $p.setOnlineEditor('OnlineAdDescription');

                // Upload online images
                $p.upload({
                    url: $url.adManagement.uploadOnlineImage,
                    element: $adEditor.find('#fileupload'),
                    progressBar: $adEditor.find('#onlineImages .progress'),
                    start: function () {
                        // Start
                        designAdModel.errorMsg('');
                        designAdModel.uploadImageInProgress(true);
                    },
                    complete: function (fileId) {
                        adService.assignOnlineImage(fileId)
                            .done(function () {
                                designAdModel.adImages.push(fileId);
                                designAdModel.uploadImageInProgress(false);
                            });
                    },
                    error: function (err) {
                        // Error
                        designAdModel.errorMsg(err);
                        designAdModel.uploadImageInProgress(false);
                    }
                });

                // Upload control for print
                var $printImg = $('#imgCropContainer > img');
                var printImgFileName;

                $p.upload({
                    url: $url.manageImg.uploadCropImage,
                    element: $adEditor.find('#fileuploadPrint'),
                    progressBar: $adEditor.find('#fileUploadPrintProgress'),
                    start: function () {
                    },
                    complete: function (fileName) {
                        printImgFileName = fileName;
                        $('#printCropImg').attr('src', $url.manageImg.renderCropImage + "?fileName=" + fileName);
                        $('#printImageCropDialog').modal('show');
                    }
                });

                $('#printImageCropDialog').on('shown.bs.modal', function () {
                    $printImg.cropper({
                        aspectRatio: 1,
                        data: { x: 500, y: 500, height: 700, width: 700 },
                        modal: true,
                        dashed: false
                    });
                });

                $('#printImageCropDialog').on('hide.bs.modal', function (e) {
                    $printImg.cropper('destroy');
                });

                $('#btnDoneCropping').on('click', function () {
                    var $btn = $(this);
                    $btn.button('loading');
                    var data = $printImg.cropper("getData");
                    data.documentId = printImgFileName;
                    imageService
                        .cropImage(printImgFileName, data.x, data.y, data.width, data.height)
                        .done(function (documentId) {
                            $btn.button('reset');
                            $('#printImageCropDialog').modal('hide');
                            designAdModel.lineAdImageId(documentId);
                        });
                });

                $('#btnCancelCropping').on('click', function () {
                    if (printImgFileName) {
                        imageService.cancelCropImage(printImgFileName);
                    }
                });

                ko.applyBindings(designAdModel);
            });
        }
    }


})(jQuery, $paramount, $paramount.url, $paramount.models);