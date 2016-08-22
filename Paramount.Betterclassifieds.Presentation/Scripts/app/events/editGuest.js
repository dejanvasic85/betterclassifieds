(function ($, ko, notifier, $p) {
    'use strict';

    function EditGuest(data) {

        var me = this,
            adDesignService = new $p.AdDesignService(data.adId);

        me.eventBookingTicketId = ko.observable();
        me.eventBookingId = ko.observable();
        me.eventTicketId = ko.observable();
        me.guestFullName = ko.observable();
        me.guestEmail = ko.observable();
        me.fields = ko.observableArray();
        me.groups = ko.observableArray();
        me.selectedGroup = ko.observable();
        me.currentGroupId = ko.observable();
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


            var dataToPost = ko.toJS(me);
            if (me.selectedGroup()) {
                dataToPost.groupId = me.selectedGroup().eventGroupId();
            } else {
                dataToPost.groupId = null;
            }

            adDesignService.editGuest(dataToPost).then(function (resp) {
                $btn.button('reset');
                if (!resp.errors) {
                    notifier.success("Guest information updated.");
                }
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