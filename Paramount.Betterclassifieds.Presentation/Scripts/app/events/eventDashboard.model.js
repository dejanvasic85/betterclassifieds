﻿(function ($, ko, $paramount) {
    'use strict';

    function EventDashboardModel(editEventViewModel) {
        var me = this;
        me.tickets = ko.observableArray();
        me.totalSoldQty = ko.observable();
        me.totalRemainingQty = ko.computed(function () {
            return _.sum(me.tickets(), function (t) {
                return t.remainingQuantity();
            });
        });
        me.totalSoldAmount = ko.observable();
        me.pageViews = ko.observable();
        me.addTicketType = function () {
            me.tickets.push(new $paramount.models.EventTicket({
                editMode: true,
                adId: editEventViewModel.adId,
                eventId: editEventViewModel.eventId,
                ticketName: 'Ticket Name',
                price: 0,
                availableQuantity: 0,
                remainingQuantity: 0,
                soldQty: 0
            }));
        }
        me.bindEditEvent(editEventViewModel);
    }

    EventDashboardModel.prototype.bindEditEvent = function (editEventViewModel) {
        var me = this;
        $.each(editEventViewModel.tickets, function (idx, t) {
            t.adId = editEventViewModel.adId;
            me.tickets.push(new $paramount.models.EventTicket(t));
        });
        me.totalSoldAmount(editEventViewModel.totalSoldAmount);
        me.totalSoldAmountFormatted = ko.computed(function () {
            return $paramount.formatCurrency(me.totalSoldAmount());
        });
        me.totalSoldQty(editEventViewModel.totalSoldQty);
        me.pageViews(editEventViewModel.pageViews);
    }

    $paramount.models = $paramount.models || {};
    $paramount.models.EventDashboardModel = EventDashboardModel;

})(jQuery, ko, $paramount);