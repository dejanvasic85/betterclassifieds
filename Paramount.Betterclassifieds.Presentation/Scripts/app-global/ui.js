﻿
/*
** General element hooks for the entire website (jQuery)
*/

(function ($) {

    $(function () {

        // Global ajax error handler
        $.ajaxSetup({
            error: function (err) {
                if (err.status === 404 || err.status === 500) {
                    toastr.error('Oh no. Something is not wired properly. Our team will be notified and apply fix shortly.');
                }
                else {
                    toastr.error('Oh no. We were unable to connect to our server. Check your internet connection and try again.');
                }
            }
        });

        // Every submit form button will have a Please Wait... while submitting the form to server ( non-ajax )
        var loadingText = 'Please wait...';
        $('form').find('button[type=submit]').attr('data-loading-text', loadingText);
        $('form').submit(function () {
            if ($(this).valid()) {
                $(this).find('button[type=submit]').button('loading');
            }
        });
        $('button.ko-load').attr('data-loading-text', loadingText);
        $('button.js-load, a.js-load').attr('data-loading-text', loadingText).on('click', function () { $(this).button('loading'); });

        // Any captcha input should add the form-control css class
        $('#CaptchaInputText').addClass("form-control");

        // Online Ad location should load areas
        $('#OnlineAdLocationId').on('change', function () {
            $('#OnlineAdLocationAreaId').loadLocationAreas($(this).val(), false);
        });

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
                    if (selected == option.value) {
                        me.append('<option selected value="' + option.value + '">' + option.text + '</option>');
                    } else {
                        me.append('<option value="' + option.value + '">' + option.text + '</option>');
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

        /*
         * jQuery extensions
         */
        $.fn.extend({
            loadSubCategories: function (parentCategoryId) {
                var me = this;
                me.empty();
                if (parentCategoryId === "") {
                    me.addClass('hidden');
                    return me;
                }
                me.attr('disabled', 'disabled').append('<option>Loading...</option>');

                var service = new $paramount.CategoryService();
                service.getChildCategories(parentCategoryId).done(function (data) {
                    me.empty().append('<option>-- Sub Category --</option>');
                    $.each(data, function (index, option) {
                        me.append('<option value=' + option.categoryId + '>' + option.title + '</option>');
                    });
                    me.removeAttr('disabled').removeClass('hidden');
                });
                return me;
            },
            loadLocationAreas: function (locationId) {
                var me = this;
                me.empty();
                if (locationId === "") {
                    me.addClass('hidden');
                    return me;
                }
                me.attr('disabled', 'disabled').append('<option>Loading...</option>');

                var locationService = new $paramount.LocationService();

                locationService.getLocationAreas(locationId).done(function (data) {
                    me.empty();
                    $.each(data, function (index, option) {
                        me.append('<option value=' + option.value + '>' + option.text + '</option>');
                    });
                    me.removeAttr('disabled').removeClass('hidden');
                });
                return me;
            },
            loadBtn: function () {
                this.button('loading');
                return this;
            },
            resetBtn: function () {
                this.button('reset');
                return this;
            }
        });
    });

})(jQuery);