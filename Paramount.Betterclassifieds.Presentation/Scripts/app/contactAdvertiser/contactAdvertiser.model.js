(function ($, $models, $url) {

    $models.ContactAdvertiser = function (adId) {
        var me = this;

        me.adId = ko.observable(adId);
        me.name = ko.observable();
        me.email = ko.observable();
        me.message = ko.observable();
        me.submitted = ko.observable(false);

        me.submit = function () {
            me.submitted(true);
        }
    }

})(jQuery, $paramount.models, $paramount.url);