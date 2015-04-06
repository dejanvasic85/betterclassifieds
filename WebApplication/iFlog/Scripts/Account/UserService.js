(function (me) {

    me.userService = {
        getAdsForUser : function() {
            return get();
        }
    };

    function get(url) {
        return $.ajax({
            url: url,
            type: 'GET',
            dataType: 'json',
            contentType: 'application/json'
        });
    }

})($paramount || {});