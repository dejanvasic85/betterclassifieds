/*
** General hooks for the entire website for bootstrap functionality
** e.g. finds submit button puts it in loading mode
*/

(function ($) {

    $(function () {
        $('form').find('button[type=submit]').attr('data-loading-text', 'Please wait...');
        $('form').submit(function () {
            if ($(this).valid()) {
                $(this).find('button[type=submit]').button('loading');
            }
        });
    });

})(jQuery);