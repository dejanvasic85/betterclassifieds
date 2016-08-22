(function ($, ko, $p) {
    function EventGroupTicket(data) {

        var me = this;
        me.eventId = ko.observable();
        me.eventTicketId = ko.observable();
        me.eventBookingTicketId = ko.observable();
        me.ticketName = ko.observable();
        me.guestFullName = ko.observable();
        me.selectedGroup = ko.observable();
        me.groups = ko.observableArray();
        me.isUpdating = ko.observable(false);
        me.errorMsg = ko.observable('');

        if (data) {
            me.bind(data);
        }

        me.friendlyTicketName = ko.computed(function () {
            return me.guestFullName() + ' - ' + me.ticketName();
        });

        me.isGroupSelected = ko.computed(function () {
            return _.isUndefined(me.selectedGroup()) === false;
        });


        function SelectControl(element) {
            var $el = $(element.currentTarget);
            var $formGroup = $el.closest('.form-group-lg');

            return {
                reset: function () {
                    return $formGroup.removeClass('has-error').removeClass('has-success');
                },
                error: function () {
                    return this.reset().addClass('has-error');
                },
                success: function () {
                    return this.reset().addClass('has-success');
                }
            }
        }

        var eventService = new $p.EventService();
        me.groupChanged = function (item, el) {
            var control = new SelectControl(el);
            me.errorMsg('');

            if (me.isGroupSelected()) {
                me.isUpdating(true);
                eventService.assignGroup(me.eventBookingTicketId(), me.selectedGroup().eventGroupId())
                    .then(function (resp) {
                        if (resp === true) {
                            control.success();
                        } else if (_.isArray(resp) && resp.length > 0) {
                            me.errorMsg(resp[0].value[0]);
                            control.error();
                        }
                        me.isUpdating(false);
                    });

            } else {
                // Remove group from the ticket
                control.reset();

                me.isUpdating(true);
                eventService.assignGroup(me.eventBookingTicketId(), null).then(function (resp) {
                    me.isUpdating(false);
                    if (_.isArray(resp) && resp.length > 0) {
                        me.errorMsg(resp[0].value[0]);
                        control.error();
                    }
                });
            }
        }
    }

    EventGroupTicket.prototype.bind = function (data) {
        this.eventId(data.eventId);
        this.eventBookingTicketId(data.eventBookingTicketId);
        this.ticketName(data.ticketName);
        this.guestFullName(data.guestFullName);
        this.eventTicketId(data.eventTicketId);

        var me = this;
        var service = new $paramount.EventService();
        service.getGroupsForTicket(this.eventId(), this.eventTicketId())
            .then(function (resp) {
                _.each(resp, function (gr) {
                    me.groups.push(new $p.models.EventGroup(gr));
                });
            });
    }

    $p.models.EventGroupTicket = EventGroupTicket;
})(jQuery, ko, $paramount)