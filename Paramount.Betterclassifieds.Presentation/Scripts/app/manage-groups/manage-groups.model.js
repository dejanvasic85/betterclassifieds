(function ($, ko, $p) {
    'use strict';

    function ManageGroups(data) {
        var me = this;

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
            var groupData = ko.toJS(me.newGroup());

            // Set the available tickets
            groupData.availableTickets = _.filter(groupData.ticketSelection, function (i) { return i.isSelected === true });
            me.groups.push(new Group(data.tickets, groupData));
            me.isCreateEnabled(false);
        }

        if (data) {
            me.bind(data);
        }
    }

    ManageGroups.prototype.bind = function (data) {
        var me = this;

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

        // Store all tickets for creating a new group
        _.each(availableTickets, function (t) {
            me.ticketSelection.push(new GroupTicketSelection(t));
        });

        if (groupData) {
            me.groupName(groupData.groupName);
            me.maxGuests(groupData.maxGuests);
            me.guestCount(groupData.guestCount);

            _.each(groupData.availableTickets, function(t) {
                me.availableTickets.push(new GroupTicketSelection(t));
            });
        }

        me.maxGuestsText = ko.computed(function () {
            return me.maxGuests() === null ? "Unlimited" : me.maxGuests();
        });
    }

    $paramount.models.ManageGroups = ManageGroups;

})(jQuery, ko, $paramount);