(function ($, ko, notifier, $p) {
    'use strict';

    function EditGuest(data) {
        var me = this,
            eventService = new $paramount.AdDesignService(data.adId);
        
        me.eventBookingTicketId = ko.observable();
        me.eventBookingId = ko.observable();
        me.eventTicketId = ko.observable();
        me.guestFullName = ko.observable();
        me.guestEmail = ko.observable();
        me.fields = ko.observableArray();
        me.isEmailDifferent = ko.computed(function () {
            if (me.guestEmail()) {
                return data.guestEmail.toLowerCase() !== me.guestEmail().toLowerCase();
            }
            return false;
        });

        /*
         * Validation
         */
        me.validator = ko.validatedObservable({
            guestFullName: me.guestFullName.extend({ required: true }),
            guestEmail: me.guestEmail.extend({ required: true, email: true })
        });

        me.save = function (vm, event) {
            if ($p.checkValidity(me) === false) {
                return;
            }
            
            var $btn = $(event.target);
            $btn.button('loading');

            // Update the guest
            eventService.editGuest(ko.toJS(me))
                .then(function () {
                    notifier.success("Guest information updated.");
                    $btn.button('reset');
                });
        }

        if (data) {
            me.bind(data);
        }
    }

    EditGuest.prototype.bind = function (data) {
        var me = this;
        me.eventBookingTicketId(data.eventBookingTicketId);
        me.eventBookingId(data.eventBookingId);
        me.eventTicketId(data.eventTicketId);
        me.guestFullName(data.guestFullName);
        me.guestEmail(data.guestEmail);

        console.log(data);
        _.each(data.fields, function(f) {
            me.fields.push(new $p.models.DynamicFieldValue(f));
        });
    }

    $p.models.EditGuest = EditGuest;

})(jQuery, ko, toastr, $paramount);