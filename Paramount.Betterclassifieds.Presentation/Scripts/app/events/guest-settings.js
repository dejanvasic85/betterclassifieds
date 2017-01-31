(function(ko, $p) {
    
    $p.models.GuestSettings = function(data) {
        var me = this,
            adDesignService = new $p.AdDesignService(data.id);

        me.eventId = ko.observable(data.eventId);
        me.displayGuests = ko.observable(data.displayGuests);

        me.saveGuestSettings = function(model, event) {
            var $btn = $(event.target);
            $btn.loadBtn();

            adDesignService.updateEventGuestSettings(ko.toJS(model))
                .success(function() {
                    toastr.success('Settings updated successfully');
                });
        }
    }

})(ko, $paramount);