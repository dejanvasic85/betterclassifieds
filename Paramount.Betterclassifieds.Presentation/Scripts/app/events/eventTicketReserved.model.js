﻿(function ($, $paramount, ko) {
    'use strict';
    function EventTicketReserved(data, ticketFieldData) {

        var me = this;
        me.eventTicketId = ko.observable();
        me.eventTicketReservationId = ko.observable();
        me.ticketName = ko.observable();
        me.guestFullName = ko.observable();
        me.guestEmail = ko.observable();
        me.price = ko.observable();
        me.status = ko.observable();
        me.isReserved = ko.observable();
        me.notReserved = ko.observable();
        me.ticketFields = ko.observableArray();

        /*
         * Computed functions
         */
        me.totalCostFormatted = ko.computed(function () {
            if (data.price === 0)
                return 'FREE';

            return $paramount.formatCurrency(me.price());
        });

        me.ticketTypeAndPrice = ko.computed(function () {
            var t = data.ticketName;
            if (data.price > 0) {
                t += " " + $paramount.formatCurrency(data.price);
            }
            return t;
        });

        /*
         * Validation
         */
        me.validator = ko.validatedObservable({
            guestFullName: me.guestFullName.extend({ required: true }),
            guestEmail: me.guestEmail.extend({ required: true, email: true })
        });

        /*
         * Update
         */
        this.updateEventTicketReservartion(data, ticketFieldData);

    }

    EventTicketReserved.prototype.updateEventTicketReservartion = function (data, ticketFieldData) {
        $.extend(data, {});

        var me = this;
        me.eventTicketId(data.eventTicketId);
        me.eventTicketReservationId(data.eventTicketReservationId);
        me.ticketName(data.ticketName);
        me.price(data.price);
        me.status(data.status);
        var isReserved = data.status.toLowerCase() === 'reserved';
        me.isReserved(isReserved);
        me.notReserved(isReserved === false);
        if (data.guestFullName) {
            me.guestFullName(data.guestFullName);
        }
        if (data.guestEmail) {
            me.guestEmail(data.guestEmail);
        }

        $.each(ticketFieldData, function (fieldIndex, f) {
            me.ticketFields.push(new $paramount.models.DynamicFieldValue(f));
        });
    }

    $paramount.models = $paramount.models || {};
    $paramount.models.EventTicketReserved = EventTicketReserved;


})(jQuery, $paramount, ko);