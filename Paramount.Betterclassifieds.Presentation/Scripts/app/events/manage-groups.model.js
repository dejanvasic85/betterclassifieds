(function ($, ko, $p, $n) {
    'use strict';

    var adDesignService;

    function ManageGroups(data) {

        adDesignService = new $p.AdDesignService(data.id);

        var me = this;
        me.id = ko.observable();
        me.eventId = ko.observable();
        me.groups = ko.observableArray();
        me.hasTickets = ko.observable();
        me.isCreateEnabled = ko.observable(false);

        me.newGroup = ko.observable(new GroupCreator(data));

        me.createStart = function () {
            me.newGroup(new GroupCreator(data));
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

            var groupData = me.newGroup().toGroupData();

            var $btn = $(event.target);
            $btn.button('loading');
            adDesignService.addEventGroup(groupData).then(function (resp) {
                me.groups.push(new Group(groupData.availableTickets, groupData));
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

        _.each(data.eventGroups, function (gr) {
            me.groups.push(new Group(gr.availableTickets, gr));
        });
    }

    function GroupTicketSelection(data) {
        var me = this;
        me.eventTicketId = ko.observable(data.eventTicketId);
        me.ticketName = ko.observable(data.ticketName);
        me.isSelected = ko.observable(data.isSelected);
    }

    function GroupCreator(data) {
        var me = this;

        me.eventId = ko.observable(data.eventId);
        me.groupName = ko.observable();
        me.maxGuests = ko.observable();
        me.ticketSelection = ko.observableArray();
        me.availableTickets = ko.observableArray();
        me.guestCount = ko.observable(0);
        me.isEnabled = ko.observable(true);

        // Generation
        me.generateEnabled = ko.observable(false);
        me.generateStart = ko.observable();
        me.generateEnd = ko.observable();
        me.toggleGeneration = function() {
            me.generateEnabled(!me.generateEnabled());
        };

        // Store all tickets for creating a new group
        _.each(data.tickets, function (t) {
            me.ticketSelection.push(new GroupTicketSelection(t));
        });

        me.toGroupData = function () {
            var jsonData = ko.toJS(me);

            jsonData.availableTickets = _.filter(jsonData.ticketSelection, function (i) { return i.isSelected === true });
            jsonData.isDisabled = !me.isEnabled();
            if (!jsonData.maxGuests || jsonData.maxGuests === '') {
                jsonData.maxGuests = null;
            }

            return jsonData;
        }


        /*
         * Validation
         */
        me.validator = ko.validatedObservable({
            groupName: me.groupName.extend({ required: true }),
            maxGuests: me.maxGuests.extend({ min: 0 })
        });

        me.isValid = $paramount.checkValidity(me);
    }

    function Group(availableTickets, groupData) {
        var me = this;
        me.eventGroupId = ko.observable(groupData.eventGroupId);
        me.groupName = ko.observable(groupData.groupName);
        me.maxGuests = ko.observable(groupData.maxGuests);
        me.maxGuestsText = ko.computed(function () {
            return me.maxGuests() === null ? "Unlimited" : me.maxGuests();
        });
        me.guestCount = ko.observable(groupData.guestCount);
        me.isEnabled = ko.observable(groupData.isDisabled === false);
        me.availableTickets = ko.observableArray();

        _.each(availableTickets, function (t) {
            me.availableTickets.push(new GroupTicketSelection(t));
        });
    }

    $p.models.ManageGroups = ManageGroups;

})(jQuery, ko, $paramount, toastr);