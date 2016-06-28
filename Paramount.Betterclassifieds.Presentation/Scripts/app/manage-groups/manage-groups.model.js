(function ($, ko, $p, $n) {
    'use strict';

    function ManageGroups(data) {
        var me = this;

        me.id = ko.observable();
        me.eventId = ko.observable();
        me.groups = ko.observableArray();
        me.tickets = ko.observableArray();
        me.isCreateEnabled = ko.observable(false);

        // New Group 
        me.newGroup = ko.observable(new Group(data.tickets));

        me.createStart = function () {
            me.newGroup(new Group(data.tickets));
            me.isCreateEnabled(true);
        }
        me.createCancel = function () {
            me.isCreateEnabled(false);
        }
        me.create = function () {
            // Check validity
            if ($paramount.checkValidity(me.newGroup()) === false) {
                return;
            }

            // Set the available tickets
            var groupData = ko.toJS(me.newGroup());
            groupData.availableTickets = _.filter(groupData.ticketSelection, function (i) { return i.isSelected === true });
            groupData.eventId = me.eventId();
            if (!groupData.maxGuests || groupData.maxGuests === '' || groupData.maxGuests === '0' || groupData.maxGuests === 0) {
                groupData.maxGuests = null;
            }

            var service = new $paramount.AdDesignService(me.id());
            var $btn = $(event.target);
            $btn.button('loading');
            service.addEventGroup(groupData).then(function (resp) {
                me.groups.push(new Group(data.tickets, groupData));
                me.isCreateEnabled(false);
                $n.success('Group has been created successfully');
            }).always(function () {
                $btn.button('reset');
            });
        }

        if (data) {
            me.bind(data);
        }
    }

    ManageGroups.prototype.bind = function (data) {
        var me = this;
        me.id(data.id);
        me.eventId(data.eventId);

        _.each(data.eventGroups, function (gr) {
            me.groups.push(new Group(data.tickets, gr));
        });
    }

    function GroupTicketSelection(data) {
        var me = this;
        me.eventTicketId = ko.observable(data.eventTicketId);
        me.ticketName = ko.observable(data.ticketName);
        me.isSelected = ko.observable(data.isSelected);
    }

    function Group(availableTickets, groupData) {
        var me = this;
        me.groupName = ko.observable();
        me.maxGuests = ko.observable();
        me.ticketSelection = ko.observableArray();
        me.availableTickets = ko.observableArray();
        me.guestCount = ko.observable(0);
        me.validator = ko.validatedObservable({
            groupName: me.groupName.extend({ required: true }),
            maxGuests: me.maxGuests.extend({ min: 0 })
        });
        me.isValid = $paramount.checkValidity(me);

        // Store all tickets for creating a new group
        _.each(availableTickets, function (t) {
            me.ticketSelection.push(new GroupTicketSelection(t));
        });

        if (groupData) {
            me.groupName(groupData.groupName);
            me.maxGuests(groupData.maxGuests);
            me.guestCount(groupData.guestCount);

            _.each(groupData.availableTickets, function (t) {
                me.availableTickets.push(new GroupTicketSelection(t));
            });
        }

        me.maxGuestsText = ko.computed(function () {
            return me.maxGuests() === null ? "Unlimited" : me.maxGuests();
        });
    }

    $p.models.ManageGroups = ManageGroups;

})(jQuery, ko, $paramount, toastr);