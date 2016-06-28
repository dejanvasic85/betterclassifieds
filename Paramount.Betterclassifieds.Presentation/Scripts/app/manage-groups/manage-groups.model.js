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
            me.groups.push(me.newGroup());
            me.newGroup(new Group(data.tickets));
            console.log(ko.toJSON(me.newGroup()));
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
        me.ticketName = ko.observable(data.ticketName);
        me.eventTicketId = ko.observable(data.eventTicketId);
        me.isSelected = ko.observable(data.isSelected);
    }

    function Group(availableTickets, groupData) {
        var me = this;
        me.groupName = ko.observable();
        me.maxGuests = ko.observable();
        me.ticketSelection = ko.observableArray();
        me.guestCount = ko.observable(0);

        _.each(availableTickets, function (t) {
            me.ticketSelection.push(new GroupTicketSelection(t));
        });

        if (groupData) {
            me.groupName(groupData.groupName);
            me.maxGuests(groupData.maxGuests);
            me.guestCount(groupData.guestCount);
        }

        me.maxGuestsText = ko.computed(function () {
            return me.maxGuests() === null ? "Unlimited" : me.maxGuests();
        });
    }

    $paramount.models.ManageGroups = ManageGroups;

})(jQuery, ko, $paramount);