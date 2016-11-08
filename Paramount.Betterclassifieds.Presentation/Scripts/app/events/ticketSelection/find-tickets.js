(function ($, ko, $p) {

    var eventService;

    function Tickets(params) {
        var me = this;

        eventService = new $p.EventService(params.baseUrl);
        me.eventGroupsPromise = eventService.getGroups(params.eventId);
        me.getTicketsPromise = eventService.getTicketsForEvent(params.eventId);

        me.availableTickets = ko.observableArray();
        me.groupsRequired = ko.observable();
        me.groups = ko.observableArray();
        me.groupSelectionEnabled = false;
        me.maxTicketsPerBooking = params.maxTicketsPerBooking;
        me.selectedGroupId = null;
        me.selectedTickets = ko.observableArray();
        me.displayNoSelectedTickets = ko.observable(false);


        // This maps to the EventTicketReservedViewModel
        me.reservationData = {
            eventId: params.eventId,
            eventInvitationId: params.eventInvitationId,
            tickets: []
        };

        me.startOrder = function (model, event) {
            if (me.selectedTickets().length > 0) {
                me.reservationData.tickets = ko.toJS(me.selectedTickets());
                var $btn = $(event.target).loadBtn();
                eventService.startTicketOrder(me.reservationData).fail(function () {
                    $btn.resetBtn();
                }).then(function (resp) {
                    if (resp.errors) {
                        $btn.resetBtn();
                    }
                });
            } else {
                me.displayNoSelectedTickets(true);
            }
        }

        me.saveAndOrder = function (element, event) {
            saveSelectedTickets();
            me.startOrder(element, event);
        };

        me.allowToOrderTickets = ko.computed(function () {
            if (me.groupSelectionEnabled === true) {
                return true;
            }
            return me.selectedTickets().length > 0;
        });

        /*
         * Used for the findTickets.model.js page
         */
        me.onGroupSelect = function (model, event) {
            var $btn = $(event.target).loadBtn();

            me.selectedGroupId = model.eventGroupId();
            me.availableTickets.removeAll();

            eventService.getTicketsForGroup(me.reservationData.eventId, model.eventGroupId())
                .then(function (resp) {
                    _.each(resp, function (t) {
                        var maxTicketsAllowed = getMaxTicketsAllowed(me.selectedGroupId, t.eventTicketId, model.maxGuests(), t.remainingQuantity);
                        var eventTicket = new $p.models.EventTicket(t, maxTicketsAllowed);
                        eventTicket.eventGroupId(model.eventGroupId());
                        me.availableTickets.push(eventTicket);
                    });

                    $('#ticketSelectionModal').modal('show');
                })
                .always(function () {
                    $btn.resetBtn();
                });
        }

        me.removeSelectedTicket = function () {
            me.selectedTickets.remove(this);
        }

        me.onGroupTicketSave = function () {
            saveSelectedTickets();
            $('#ticketSelectionModal').modal('hide');
        }

        me.onGroupTicketSaveAndOrder = function (model, event) {
            saveSelectedTickets();
            me.startOrder(model, event);
        }

        me.currentPage = ko.observable(1);
        me.pageSize = 3;
        me.totalGroups = ko.computed(function () {
            return me.groups().length;
        });
        me.groupsPaged = ko.computed(function () {
            var chunked = _.chunk(me.groups(), me.pageSize);
            return chunked[me.currentPage() - 1];
        });
        me.ticketsPaged = ko.computed(function () {
            var chunkd = _.chunk(me.availableTickets(), me.pageSize);
            return chunkd[me.currentPage() - 1];
        });
        me.changePage = function (pageNum) {
            me.currentPage(pageNum);
        }

        function saveSelectedTickets() {
            _.each(me.availableTickets(), function (t) {
                if (t.selectedQuantity() > 0) {

                    var existingTicket = _.find(me.selectedTickets(), function (existing) {
                        return existing.eventTicketId() === t.eventTicketId() && existing.eventGroupId() === t.eventGroupId();
                    });

                    if (existingTicket) {
                        var currentQty = existingTicket.selectedQuantity();
                        existingTicket.selectedQuantity(currentQty + t.selectedQuantity());
                        return;
                    }

                    if (t.eventGroupId() && me.eventGroupsPromise !== null) {
                        me.eventGroupsPromise.then(function (groups) {
                            var group = _.find(groups, { eventGroupId: t.eventGroupId() });
                            t.eventGroupName(group.groupName);
                            me.selectedTickets.push(t);

                            return groups;
                        });
                    } else {
                        me.selectedTickets.push(t);
                    }
                }
            });
        }

        function getMaxTicketsAllowed(eventGroupId, eventTicketId, groupMaxGuests, ticketRemainingQuantity) {
            var currentSelectedCount = _.sumBy(me.selectedTickets(), function (t) {
                if (t.eventGroupId() === eventGroupId && t.eventTicketId() === eventTicketId) {
                    return t.selectedQuantity();
                }
                return 0;
            });

            var maxTicketsAllowed = me.maxTicketsPerBooking; // Align by the maximum first
            if (!_.isNull(groupMaxGuests) && groupMaxGuests < maxTicketsAllowed) {
                maxTicketsAllowed = groupMaxGuests;
            }

            if (!_.isNull(ticketRemainingQuantity) && ticketRemainingQuantity < maxTicketsAllowed) {
                maxTicketsAllowed = ticketRemainingQuantity;
            }

            maxTicketsAllowed = maxTicketsAllowed - currentSelectedCount;

            return maxTicketsAllowed;
        }

        /*
         * Databind data from the server
         */
        this.load(params, function () {
            // Todo - toggle loader
        });
    }

    Tickets.prototype.load = function (params, done) {
        var me = this;

        eventService.getEvent(params.eventId).then(function (eventData) {
            me.groupsRequired(eventData.groupsRequired);

            if (eventData.groupsRequired === true) {

                me.eventGroupsPromise.then(function (groupData) {
                    _.each(groupData, function (gr) {
                        me.groups.push(new $p.models.EventGroup(gr));
                    });
                    me.groupSelectionEnabled = eventData.groupsRequired && groupData.length > 0;
                    done();

                    $(window).off('.affix');
                    $('.ticket-booth').removeClass("affix affix-top").removeData('bs.affix');

                    return groupData;
                });
            } else {
                me.getTicketsPromise.then(function (ticketData) {
                    _.each(ticketData, function (t) {
                        me.availableTickets.push(new $p.models.EventTicket(t, me.maxTicketsPerBooking));
                    });
                    done();
                    return ticketData;
                });
            }
        });
    }


    ko.components.register('find-tickets', {
        viewModel: Tickets,
        template: { path: '/Scripts/app/events/ticketSelection/find-tickets.html' }
    });


})(jQuery, ko, $paramount);