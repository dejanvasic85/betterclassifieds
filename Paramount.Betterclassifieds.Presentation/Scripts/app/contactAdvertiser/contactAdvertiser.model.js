(function ($, $models) {

    $models.ContactAdvertiser = function (data) {
        var me = this;

        if (!$paramount.notNullOrUndefined(data) ||
            !$paramount.notNullOrUndefined(data.adId) ||
            !$paramount.notNullOrUndefined(data.adEnquiryUrl)) {
            throw "data cannot be null or undefined when binding the contact form";
        }

        me.adId = ko.observable(data.adId);
        me.fullName = ko.observable();
        me.email = ko.observable();
        me.question = ko.observable();
        me.submitted = ko.observable(false);

        if (data) {
            me.bind(data);
        }

        me.sendMsg = function () {
            var $form = $('#contactAdvertiserForm');
            var $btn = $form.find('button');

            if ($form.valid()) {
                $btn.button('loading');
                var model = ko.toJSON(me);

                $paramount.httpPost(data.adEnquiryUrl, model)
                    .then(showSuccess)
                    .always(resetButton);

                function showSuccess() {
                    me.submitted(true);
                }

                function resetButton() {
                    $btn.button('reset');
                }
            }
        }
    }

    $models.ContactAdvertiser.prototype.bind = function (data) {
        this.fullName = ko.observable(data.fullName);
        this.email = ko.observable(data.email);
    }

    return $models;

})(jQuery, $paramount.models);