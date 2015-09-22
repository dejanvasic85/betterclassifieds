(function ($, $models) {

    $models.ContactAdvertiser = function (adId, url) {
        var me = this;

        me.adId = ko.observable(adId);
        me.fullName = ko.observable();
        me.email = ko.observable();
        me.question = ko.observable();
        me.submitted = ko.observable(false);
        
        me.sendMsg = function () {
            var $form = $('#contactAdvertiserForm');
            var $btn = $form.find('button');
            
            if ($('#contactAdvertiserForm').valid()) {
                $btn.button('loading');
                var model = ko.toJSON(me);
                console.log(model);
                $.ajax({
                    url: url,
                    data: model,
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json'
                }).success(function (response) {
                    console.log('success');
                    console.log(response);
                }).done(function() {
                    $btn.button('reset');
                    me.submitted(true);
                });
            }
        }
    }

    return $models;

})(jQuery, $paramount.models);