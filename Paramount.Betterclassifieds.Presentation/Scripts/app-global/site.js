/*
** $paramount utility class
*/
(function ($paramount, $window, $, $mobileDetect, htmlEditor) {
    var me = this;
    me.isMobileDevice = null;
    
    // Lazy loading for mobile checking
    me.evaluateMobile = function () {
        if (isMobileDevice !== null) {
            return isMobileDevice;
        }
        var detector = new $mobileDetect($window.navigator.userAgent);
        me.isMobileDevice = detector.mobile() !== null;
        return me.isMobileDevice;
    };

    /*
     * Utility function to guard against undefined parameters
     */
    function guard(val, name) {
        if (!val) {
            throw name + ' is required';
        }
    };

    $paramount.formatCurrency = function (value) {
        if (value == undefined)
            return '';
        return "$" + value.toFixed(2);
    };

    $paramount.isMobile = function () {
        return evaluateMobile();
    };

    $paramount.setOnlineEditor = function (element) {
        if (me.evaluateMobile()) {
            return null;
        }
        
        $paramount.onlineEditor = htmlEditor.replace(element);

        return $paramount.onlineEditor;
    };

    $paramount.delay = (function () {
        var timer = 0;
        return function (callback, ms) {
            clearTimeout(timer);
            timer = setTimeout(callback, ms);
        };
    })();

    /*
     * File upload control
     */
    $paramount.upload = function (options) {

        guard(options, 'options');
        guard(options.url, 'url');
        guard(options.element, '');

        options.progressBar = options.progressBar || $('<div><span class=".progress-bar"></span></div>'); // Todo - popup a real progress bar somewhere
        options.complete = options.complete || function () { };
        options.start = options.start || function () { };
        options.error = options.error || function () { };

        var upload = options.element.fileupload({
            dataType: 'json',
            url: options.url,
            autoUpload: true,
            singleFileUploads: false
        });

        upload.on('fileuploaddone', function (e, data) {
            if (data.result.documentId) {
                options.complete(data.result.documentId);
            } else {
                options.error(data.result.errorMsg);
            }
        });

        upload.on('fileuploadstart', function (data) {
            // Start showing the progress bar
            options.progressBar.show();
            options.start(data);
        });

        upload.on('fileuploadalways', function () {
            // Reset the width of the progress bar
            options.progressBar.hide();
            options.progressBar.find('.progress-bar').width('0');
        });

        upload.on('fileuploadprogressall', function (e, data) {
            var progress = parseInt(data.loaded / data.total * 100, 10);
            options.progressBar.find('.progress-bar').css('width', progress + '%');
        });

        upload.on('fileuploadadd', function (e, data) {
            var acceptFileTypes = /^image\/(gif|jpe?g|png)$/i;
            if (data.originalFiles[0]['type'].length && !acceptFileTypes.test(data.originalFiles[0]['type'])) {
                options.error('Not an accepted file type.');
                return false;
            }

            if (data.originalFiles[0].size > '@Model.MaxImageUploadBytes') {
                options.error("The file exceeds the maximum file size.");
                return false;
            }

            if (data.files.length > 1) {
                options.error("Please select one image at a time.");
                return false;
            }
            return true;
        });
    };

    return me;

})($paramount, window, jQuery, MobileDetect, CKEDITOR);


