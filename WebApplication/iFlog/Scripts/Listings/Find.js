
/*
** Supports the find listings functionality
*/

(function ($) {

    var page = 1;

    // JQuery on-ready
    $(function () {
        $('#btnShowMore').on('click', function () {
            var me = $(this);
            me.button('loading');

            $.post(me.data().url).done(function (ads) {

                $('.list-group').append(ads);

            }).always(function () {
                page++;

                // 5 pages are enough so just hide the more button
                if (page === 5) {
                    me.hide();
                }
                me.button('reset');
            });
        });
    });

})(jQuery);