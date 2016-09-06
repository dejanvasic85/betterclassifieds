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

        me.generate = function (model, event) {
            var $btn = $(event.target);
            $btn.button('loading');

            // Create bunch of groups now based on the start and end
            me.newGroup().generateGroups(function (addedGroup) {
                me.groups.push(new Group(addedGroup.availableTickets, addedGroup));
            }, function () {
                $btn.button('reset');
                me.isCreateEnabled(false);
                $n.success('Done. Groups generated successfully.');
            });
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
        me.limitsEnabled = ko.observable(false);
        me.toggleLimits = function () {
            me.limitsEnabled(!me.limitsEnabled());
        }

        // Generation
        me.generateEnabled = ko.observable(false);
        me.generateStart = ko.observable();
        me.generateEnd = ko.observable();
        me.generateProgress = ko.observable(0);

        me.generateTotalGroups = ko.computed(function () {
            var first = parseInt(me.generateStart());
            var last = parseInt(me.generateEnd());
            return (last - first) + 1;
        });

        me.toggleGeneration = function () {
            me.generateEnabled(!me.generateEnabled());
        };

        me.generateError = ko.computed(function () {
            if (_.isUndefined(me.groupName())) {
                return 'You must provide a group name before generating.';
            }

            if (isNaN(me.generateStart()) || isNaN(me.generateEnd())) {
                return 'You must provide start and end before generating.';
            }

            var totalGroups = me.generateTotalGroups();

            if (totalGroups <= 1) {
                return 'Total groups must be equal or greater than two';
            }

            return '';
        });

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

            // Remove unused properties for submit
            delete jsonData.generateEnabled;
            delete jsonData.generateStart;
            delete jsonData.generateEnd;

            return jsonData;
        }

        me.generateGroups = function (onGroupAdded, onComplete) {
            var savedGroups = 0;

            var start = parseInt(me.generateStart());
            var end = parseInt(me.generateEnd());
            var totalGroups = end - start;

            for (var i = start; i <= end; i++) {
                var gr = me.toGroupData();
                gr.groupName += ' ' + i;
                
                adDesignService.addEventGroup(gr).success(function (resp) {
                    onGroupAdded(resp.group);
                    savedGroups++;
                    me.generateProgress(totalGroups / savedGroups);

                    if (savedGroups === totalGroups) {
                        onComplete();
                    }
                });
            }
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