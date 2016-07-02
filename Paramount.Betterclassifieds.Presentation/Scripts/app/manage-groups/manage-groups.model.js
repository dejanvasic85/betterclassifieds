(function ($, ko, $p, $n) {
    'use strict';

    var adDesignService;

    function ManageGroups(data) {
        var me = this;

        me.id = ko.observable();
        me.eventId = ko.observable();
        me.groups = ko.observableArray();
        me.hasTickets = ko.observable();
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
        me.create = function (model, event) {
            // Check validity
            if ($paramount.checkValidity(me.newGroup()) === false) {
                return;
            }

            // Set the available tickets
            var groupData = ko.toJS(me.newGroup());
            groupData.availableTickets = _.filter(groupData.ticketSelection, function (i) { return i.isSelected === true });
            groupData.eventId = me.eventId();
            groupData.isDisabled = !me.newGroup().isEnabled();
            if (!groupData.maxGuests || groupData.maxGuests === '') {
                groupData.maxGuests = null;
            }

            var $btn = $(event.target);
            $btn.button('loading');
            adDesignService.addEventGroup(groupData).then(function (resp) {
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
        me.hasTickets(data.tickets && data.tickets.length > 0);
        adDesignService = new $paramount.AdDesignService(data.id);

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
        me.eventGroupId = ko.observable();
        me.groupName = ko.observable();
        me.maxGuests = ko.observable();
        me.ticketSelection = ko.observableArray();
        me.availableTickets = ko.observableArray();
        me.guestCount = ko.observable(0);
        me.isEnabled = ko.observable(true);
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
            me.eventGroupId(groupData.eventGroupId);
            me.groupName(groupData.groupName);
            me.maxGuests(groupData.maxGuests);
            me.guestCount(groupData.guestCount);
            me.isEnabled(groupData.isDisabled === false);
            me.isEnabled.subscribe(function (isEnabled) {
                adDesignService.toggleEventGroup({
                    eventGroupId: me.eventGroupId(),
                    isDisabled: !isEnabled
                }).then(function () {
                    var statusName = isEnabled ? "enabled" : "disabled";
                    $n.success('Status is now ' + statusName);
                });
            });

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