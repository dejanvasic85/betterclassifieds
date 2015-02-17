

/*
** $paramount utility class
*/

var $paramount = $paramount || {};

(function($window, $, $mobileDetect) {

    var me = this;
    me.isMobileDevice = null;

    // Lazy loading for mobile checking
    me.evaluateMobile = function() {
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

    $paramount.setOnlineEditor = function (setupCallback) {
        if (!me.evaluateMobile()) {
            $paramount.onlineEditor = setupCallback();
        }
    };


    /*
     * File upload control
     */
    $paramount.upload = function (options) {

        guard(options, 'options');
        guard(options.url, 'url');
        guard(options.element, '');

        options.progressBar = options.progressBar || $('<div><span class=".progress-bar"></span></div>'); // Todo - popup a real progress bar somewhere
        options.complete = options.complete || function() {};
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

})(window, jQuery, MobileDetect);


/*
** General element hooks for the entire website (jQuery)
*/

(function ($) {

    $(function () {

        // Global ajax error handler
        $.ajaxSetup({
            error: function () {
                toastr.error('Oh no. We were unable to connect to our server. Check your internet connection and try again.');
            }
        });

        // Every submit form button will have a Please Wait... while submitting the form to server ( non-ajax )
        $('form').find('button[type=submit]').attr('data-loading-text', 'Please wait...');
        $('form').submit(function () {
            if ($(this).valid()) {
                $(this).find('button[type=submit]').button('loading');
            }
        });
        $('button.js-load').attr('data-loading-text', 'Please wait...').on('click', function () { $(this).button('loading'); });

        // Any captcha input should add the form-control css class
        $('#CaptchaInputText').addClass("form-control");

        // Wire up the bootstrap tooltips
        $("[rel='tooltip']").tooltip();

        // Wire up js-select dropdowns
        $('.js-select').each(function () {
            var me = $(this);
            me.attr('disabled', 'disabled');
            me.append('<option>Loading...</option>');
            var url = me.data().url;
            var selected = me.data().selected;
            $.getJSON(url).done(function (data) {
                me.empty();
                $.each(data, function (index, option) {
                    if (selected == option.Value) {
                        me.append('<option selected value="' + option.Value + '">' + option.Text + '</option>');
                    } else {
                        me.append('<option value="' + option.Value + '">' + option.Text + '</option>');
                    }
                });
                me.removeAttr('disabled');
            });
        });

        // Buttons that navigate    
        $('button[data-nav]').on('click', function () {
            window.location = $(this).data().nav;
            return false;
        });

        // Wire up the datepickers (using bootstrap-datepicker.js library)
        $('.datepicker').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy',
            todateBtn: true,
            todayHighlight: true,
            startDate: new Date()
        });

        $('input[data-numbers-only]').on('keypress', function (e) {
            if ((e.which < 48 || e.which > 57)) {
                if (e.which == 8 || e.which == 46 || e.which == 0) {
                    return true;
                }
                else {
                    return false;
                }
            }
        });

        // JQuery validation extensions
        $.validator.methods.date = function (value, element) {
            return this.optional(element) || moment(value, 'dd/MM/yyyy').isValid();
        };

        // JQuery extensions
        $.fn.extend({
            loadSubCategories: function (url, parentCategoryId) {
                var me = this;
                me.empty();
                if (parentCategoryId === "") {
                    me.addClass('hidden');
                    return me;
                }
                me.attr('disabled', 'disabled').append('<option>Loading...</option>');
                $.getJSON(url, { parentId: parentCategoryId }).done(function (data) {
                    me.empty().append('<option>-- Sub Category --</option>');
                    $.each(data, function (index, option) {
                        me.append('<option value=' + option.CategoryId + '>' + option.Title + '</option>');
                    });
                    me.removeAttr('disabled').removeClass('hidden');
                });
                return me;
            }
        });

        $.fn.extend({
            loadLocationAreas: function (url, locationId) {
                var me = this;
                me.empty();
                if (locationId === "") {
                    me.addClass('hidden');
                    return me;
                }
                me.attr('disabled', 'disabled').append('<option>Loading...</option>');
                $.getJSON(url, { locationId: locationId }).done(function (data) {
                    me.empty();
                    $.each(data, function (index, option) {
                        me.append('<option value=' + option.Value + '>' + option.Text + '</option>');
                    });
                    me.removeAttr('disabled').removeClass('hidden');
                });
                return me;
            }
        });
    });

})(jQuery);
