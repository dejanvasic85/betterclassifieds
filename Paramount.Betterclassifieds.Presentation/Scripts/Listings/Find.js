﻿
/*
** Supports the find listings functionality
*/

(function ($) {

    var maxPageRequests = parseInt( $('#hdnMaxPageRequests').val() );
    var currentPage = 1;

    // JQuery on-ready
    $(function () {
        $('#btnShowMore').on('click', function () {
            var me = $(this);
            me.button('loading');  

            $.post(me.data().url, {page : currentPage}).done(function (adListHtml) {
                var $items = $(adListHtml).find('.list-group-item');
                if ($items.length == 0) {
                    me.hide();
                } else {
                    $('.list-group').append($(adListHtml).find('.list-group-item'));
                }
            }).always(function () {
                currentPage++;
                if (currentPage === maxPageRequests) {
                    me.hide();
                }
                me.button('reset');
            });
        });
    });

})(jQuery);