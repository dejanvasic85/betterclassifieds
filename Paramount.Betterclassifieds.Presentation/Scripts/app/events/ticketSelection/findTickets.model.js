(function ($, $paramount, ko) {
    'use strict';

    /*
     * Used for the event view details page for ticket and group selection
     */

    var $ticketsModal = $('#ticketSelectionModal');
    var selectedTickets = [];
    var selectedGroupId = null;

    $paramount.models = $paramount.models || {};
    $paramount.models.FindTickets = function (eventService, data) {
        var me = this;

        $.extend(data, {});

        me.tickets = ko.observableArray();
        me.eventId = ko.observable(data.eventId);
        me.groupsRequired = ko.observable(data.groupsRequired);
        me.groups = ko.observableArray();
        me.eventService = eventService;

        if (data.groupsRequired === true && data.groups) {
            $.each(data.groups, function (index, item) {
                me.groups.push(new $paramount.models.EventGroup(item));
            });
        }

        $.each(data.ticketData, function (index, item) {
            me.tickets.push(new $paramount.models.EventTicket(item, data.maxTicketsPerBooking));
        });

        me.startOrder = function (element, event) {
            var $btn = $(event.target).loadBtn();
            _.remove(me.tickets(), function (t) {
                return t.selectedQuantity() === undefined || t.selectedQuantity() === 0;
            });
            eventService.startTicketOrder(ko.toJSON(me)).error(function () {
                $btn.resetBtn();
            });
        }

        // property used for the user interface to show/hide the submit button
        me.totalSelectedTickets = ko.computed(function () {
            var total = 0;
            if (_.isUndefined(me.tickets()) || me.tickets().length === 0) {
                return total;
            }

            $.each(me.tickets(), function (index, item) {
                if (!_.isUndefined(item.selectedQuantity())) {
                    total += item.selectedQuantity();
                }
            });

            return total;
        });

        /*
         * Used for the findTickets.model.js page
         */
        me.onGroupSelect = function (model, el) {
            var $btn = $(el.target).loadBtn();

            selectedGroupId = model.eventGroupId();
            me.tickets.removeAll();
            me.eventService.getAvailableTicketsForGroup(model.eventId(), model.eventGroupId()).then(loadTickets);

            function loadTickets(resp) {

                // Todo - create event ticket from each item in resp
                console.log('resp', resp);

                //me.tickets.push({
                //    ticketName: ko.observable('General Admission'),
                //    selectedQuantity: ko.observable(0),
                //    price : ko.observable(0)
                //});
                $ticketsModal.modal('show');
            }
        }

        /*
         * User selected to save tickets and continue
         */
        me.onGroupTicketSave = function () {
            saveSelectedTickets();
            $ticketsModal.modal('hide');
        }

        function saveSelectedTickets() {
            var tix = ko.toJS(me.tickets);

            _.remove(tix, function (t) {
                return _.isUndefined(t.selectedQuantity) || t.selectedQuantity <= 0;
            });

            _.each(tix, function (t) {
                t.eventGroupId = selectedGroupId;
            });

            selectedTickets.push(tix);
            console.log('selectedTickets', selectedTickets);
        }
    }
})(jQuery, $paramount, ko);