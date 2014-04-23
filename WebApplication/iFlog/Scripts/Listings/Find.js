
/*
** Supports the find listings functionality
*/

(function ($) {

    var resultsPerPage = Number.parseInt( $('#hdnResultsPerPage').val() );
    var maxPageRequests = Number.parseInt( $('#hdnMaxPageRequests').val() );
    var currentPage = 1;

    // JQuery on-ready
    $(function () {
        $('#btnShowMore').on('click', function () {
            var me = $(this);
            me.button('loading');  

            $.post(me.data().url, {page : currentPage}).done(function (adListHtml) {
                $('.list-group').append( $(adListHtml).find('.list-group-item') );
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