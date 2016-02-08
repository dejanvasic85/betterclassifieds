$paramount.social = (function($) {
    
    /*
     * Facebook sharing event handler
     */
    return {
        setupFacebook : function(facebookBtn, data) {
            var facebookShareData = _.extend({
                method: 'share'
            }, data);

            facebookBtn.on('click', function () {
                FB.ui(facebookShareData, function(response) {});
            });
        }
    }
})(jQuery);