(function ($, $p, ko) {

    'use strict';

    function AddGuest() {
        this.id = ko.observable();
        this.eventId = ko.observable();
        this.guestFullName = ko.observable();
        this.guestEmail = ko.observable();
        this.selectedTicket = ko.observable();
        this.sendEmailToGuest = ko.observable(true);
        this.tickets = ko.observableArray();
        this.ticketFields = ko.observableArray();
        this.saved = ko.observable();
        this.selectedGroup = ko.observable();
        this.hasGroups = ko.observable(false);
        this.eventGroups = ko.observableArray();
        this.validator = ko.validatedObservable({
            guestFullName: this.guestFullName.extend({ required: true }),
            guestEmail: this.guestEmail.extend({ required: true, email: true })
        });
    }

    AddGuest.prototype.bindData = function (data) {
        var me = this;
        me.id(data.id);
        me.eventId(data.eventId);
        me.guestFullName(data.guestFullName);
        me.guestEmail(data.guestEmail);
        me.hasGroups(data.eventGroups && data.eventGroups.length > 0);

        _.each(data.ticketFields, function (tf) {
            me.ticketFields.push(new $p.models.DynamicFieldValue(tf));
        });

        _.each(data.eventTickets, function (et) {
            me.tickets.push(new $p.models.EventTicket(et, 5));
        });

        _.each(data.eventGroups, function (gr) {
            me.eventGroups.push(new $p.models.EventGroup(gr));
        });
    }

    AddGuest.prototype.submitGuest = function (element, event) {
        var me = this;
        if (!$paramount.checkValidity(me, me.ticketFields())) {
            return;
        }

        var $btn = $(event.target);
        $btn.button('loading');

        var objToSend = ko.toJS(me);
        delete objToSend["tickets"]; // Remove unecessary collection that is bound to a simple dropdown

        var service = new $p.AdDesignService(me.id());
        service.addGuest(objToSend).then(function () {
            $btn.button('reset');
            me.saved(true);
        });
    }

    AddGuest.prototype.ticketChanged = function () {
        var me = this;
        var service = new $p.EventService();
        me.ticketFields.removeAll();
        service.getFieldsForTicket(me.selectedTicket().eventTicketId()).then(function (resp) {
            _.each(resp, function (tf) {
                me.ticketFields.push(new $p.models.DynamicFieldValue(tf));
            });
        });
    }

    AddGuest.prototype.addAnother = function (element, event) {
        this.guestFullName(null);
        this.guestEmail(null);
        _.each(this.ticketFields(), function (tf) {
            tf.fieldValue(null);
        });
        this.saved(false);
        $('html, body').animate({ scrollTop: $('.add-guest-view').offset().top }, 1000);
    }

    $paramount.models = $paramount.models || {};
    $paramount.models.AddGuest = AddGuest;

})(jQuery, $paramount, ko);