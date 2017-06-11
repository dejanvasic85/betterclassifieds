(function ($, ko, notifier, $p) {
    
    // Maps to ViewModel: EditGuestViewModel
    function EditGuest(data) {

        var me = this,
            adDesignService = new $p.AdDesignService(data.adId),
            eventService = new $p.EventService();

        me.adId = ko.observable();
        me.eventId = ko.observable();
        me.eventBookingTicketId = ko.observable();
        me.eventBookingId = ko.observable();
        me.eventTicketId = ko.observable();
        me.guestFullName = ko.observable();
        me.originalGuestEmail = ko.observable();
        me.guestEmail = ko.observable();
        me.fields = ko.observableArray();
        me.groups = ko.observableArray();
        me.selectedGroup = ko.observable();
        me.currentGroupId = ko.observable();
        me.displayGuests = ko.observable(); // High level setting
        me.isPublic = ko.observable();
        me.seatNumber = ko.observable();
        me.sendTransferEmail = ko.observable(true);
        me.sendEmailToGuestAboutRemoval = ko.observable(false);
        me.isEmailDifferent = ko.computed(function () {
            if (me.guestEmail() && me.originalGuestEmail()) {
                return me.guestEmail().toLowerCase() !== me.originalGuestEmail().toLowerCase();
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

            if (!$p.checkValidity(me) || !$paramount.checkValidity(me.fields())) {
                return;
            }

            var $btn = $(event.target);
            $btn.button('loading');


            var dataToPost = ko.toJS(me);
            if (me.selectedGroup()) {
                dataToPost.groupId = me.selectedGroup().eventGroupId();
            } else {
                dataToPost.groupId = null;
            }
            dataToPost.sendTransferEmail = me.isEmailDifferent() && me.sendTransferEmail();

            adDesignService.editGuest(dataToPost).then(function (resp) {
                if (!resp.errors) {
                    notifier.success("Guest information updated.");
                    //  Update the local details
                    me.originalGuestEmail(me.guestEmail());
                    me.eventBookingTicketId(resp.eventBookingTicketId);
                }
            });
        }

        me.removeGuest = function (vm, event) {
            var $btn = $(event.target);
            $btn.button('loading');

            var data = {
                eventId : me.eventId(),
                eventBookingTicketId: me.eventBookingTicketId(),
                sendEmailToGuestAboutRemoval: me.sendEmailToGuestAboutRemoval()
            }

            adDesignService.removeGuest(data);
        }

        me.resendGuestEmail = function(vm, event) {
            var $btn = $(event.target);
            $btn.button('loading');

            eventService.resendGuestEmail(me.adId(), me.eventBookingTicketId())
                .success(function() {
                    notifier.success('Email has been sent successfully.');
                });
        }

        if (data) {
            me.bind(data);
        }

    }

    EditGuest.prototype.bind = function (data) {
        var me = this;
        me.adId(data.adId);
        me.eventId(data.eventId);
        me.eventBookingTicketId(data.eventBookingTicketId);
        me.eventBookingId(data.eventBookingId);
        me.eventTicketId(data.eventTicketId);
        me.guestFullName(data.guestFullName);
        me.originalGuestEmail(data.guestEmail);
        me.guestEmail(data.guestEmail);
        me.currentGroupId(data.currentGroupId);
        me.displayGuests(data.displayGuests);
        me.isPublic(data.isPublic);
        me.seatNumber(data.seatNumber);

        _.each(data.fields, function (f) {
            me.fields.push(new $p.models.DynamicFieldValue(f));
        });

        _.each(data.groups, function (g) {
            var eventGroup = new $p.models.EventGroup(g);
            me.groups.push(eventGroup);

            if (g.eventGroupId === data.groupId) {
                me.selectedGroup(eventGroup);
            }
        });
    }

    $p.models.EditGuest = EditGuest;

})(jQuery, ko, toastr, $paramount);