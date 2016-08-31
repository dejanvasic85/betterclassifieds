(function ($, ko, notifier, $p) {
    'use strict';

    function EditGuest(data) {

        var me = this,
            adDesignService = new $p.AdDesignService(data.adId);

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
        me.sendEmailToGuest = ko.observable(true);
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

            if ($p.checkValidity(me) === false) {
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
            dataToPost.sendEmailToGuest = me.isEmailDifferent() && me.sendEmailToGuest();

            adDesignService.editGuest(dataToPost).then(function (resp) {
                $btn.button('reset');
                if (!resp.errors) {
                    notifier.success("Guest information updated.");
                    me.originalGuestEmail(me.guestEmail());
                    me.eventBookingTicketId(resp.eventBookingTicketId);
                }
            });
        }

        me.removeGuest = function (vm, event) {
            var $btn = $(event.target);
            $btn.button('loading');

            var data = {
                eventBookingTicketId: me.eventBookingTicketId(),
                sendEmailToGuestAboutRemoval: me.sendEmailToGuestAboutRemoval()
            }
            adDesignService.removeGuest(data);
        }

        if (data) {
            me.bind(data);
        }

    }

    EditGuest.prototype.bind = function (data) {
        var me = this;
        me.eventId(data.eventId);
        me.eventBookingTicketId(data.eventBookingTicketId);
        me.eventBookingId(data.eventBookingId);
        me.eventTicketId(data.eventTicketId);
        me.guestFullName(data.guestFullName);
        me.originalGuestEmail(data.guestEmail);
        me.guestEmail(data.guestEmail);
        me.currentGroupId(data.currentGroupId);

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