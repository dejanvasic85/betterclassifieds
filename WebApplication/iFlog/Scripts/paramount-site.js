
/*
** General element hooks for the entire website
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
        $('button.js-load').attr('data-loading-text', 'Please wait...').on('click', function() { $(this).button('loading'); });

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
                    if (selected === option.Value) {
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
            todayHighlight : true
        });

        // Wire up all the markdown editors on load
        $('.markdown-editor').markdown({
            autofocus: false,
            savable: false
        });

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
    });

})(jQuery);