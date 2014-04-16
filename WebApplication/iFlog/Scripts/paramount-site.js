﻿
/*
** General element hooks for the entire website
*/

(function ($) {

    $(function () {

        // Every submit form button will have a Please Wait... while submitting the form to server ( non-ajax )
        $('form').find('button[type=submit]').attr('data-loading-text', 'Please wait...');
        $('form').submit(function () {
            if ($(this).valid()) {
                $(this).find('button[type=submit]').button('loading');
            }
        });

        // Every js-select will load the select items from data-url attribute
        $('.js-select').each(function () {
            var me = $(this);
            me.attr('disabled', 'disabled');
            me.append('<option>Loading...</option>');
            var url = me.data().url;
            $.getJSON(url).done(function (data) {
                me.empty();
                $.each(data, function (index, option) {
                    me.append('<option value="' + option.Value + '">' + option.Text + '</option>');
                });
                me.removeAttr('disabled');
            });
        });
    });

})(jQuery);