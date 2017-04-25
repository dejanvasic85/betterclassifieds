(function ($, $p, ko, toastr) {

    $p.models.ManageNotifications = function (data) {

        var me = this;
        var organiserService = new $p.OrganiserService(data.eventId);

        me.adId = ko.observable(data.adId);
        me.eventId = ko.observable(data.eventId);
        me.subscribeToPurchaseNotifications = ko.observable(data.subscribeToPurchaseNotifications);
        me.subscribeToDailyNotifications = ko.observable(data.subscribeToDailyNotifications);

        me.save = function (data, element) {
            var $btn = $(element.target);
            var model = ko.toJS(me);

            organiserService.setNotifications(model)
                .then(function () {
                    toastr.success("Settings updated successfully");
                    $btn.resetBtn();
                });
        }
    }

})(jQuery, $paramount, ko, toastr);