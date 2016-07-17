(function ($, ko, toastr, $p) {
    'use strict';

    var adDesignService;

    function ManageTickets(data) {
        var me = this;
        me.id = ko.observable(data.id);
        me.eventId = ko.observable(data.eventId);
        me.tickets = $p.ko.bindArray(data.tickets, function (t) {
            return new $p.models.EventTicket(t, 20);
        });
        me.isCreateEnabled = ko.observable(false);
        me.newTicket = ko.observable(new NewEventTicket({ eventId: data.eventId }));
        adDesignService = new $p.AdDesignService(data.id);

        me.removeField = function (f) {
            me.newTicket().fields.remove(f);
        }
    }

    ManageTickets.prototype.startNewTicket = function () {
        this.isCreateEnabled(true);
    }

    ManageTickets.prototype.cancelNewTicket = function () {
        this.isCreateEnabled(false);
    }

    ManageTickets.prototype.createNewTicket = function (element, event) {
        var me = this;
        var $btn = $(event.target); // Grab the jQuery element from knockout
        var newTicket = me.newTicket();
        if ($paramount.checkValidity(newTicket)) {
            $btn.button('loading');
            var eventTicketData = ko.toJS(newTicket);
            adDesignService.addEventTicket(eventTicketData)
                .then(function(resp) {
                    if (resp === true) {
                        toastr.success('Ticket added successfully');
                        me.newTicket = new NewEventTicket({ eventId: me.eventId() });
                        eventTicketData.remainingQuantity = eventTicketData.availableQuantity;
                        me.tickets.push(new $p.models.EventTicket(eventTicketData, 20));
                    }
                })
                .always(function() {
                    $btn.button('reset');
                });
        }
    }

    function NewEventTicket(data) {
        var me = this;

        me.eventId = ko.observable(data.eventId);
        me.ticketName = ko.observable();
        me.price = ko.observable();
        me.availableQuantity = ko.observable();
        me.fields = ko.observableArray();

        me.validator = ko.validatedObservable({
            ticketName: me.ticketName.extend({ required: true }),
            price: me.price.extend({ min: 0, required: true }),
            availableQuantity: me.availableQuantity.extend({ min: 0, required: true })
        });

        me.addField = function () {
            me.fields.push(new $p.models.DynamicFieldDefinition());
        }
    }

    $p.models.ManageTickets = ManageTickets;

})(jQuery, ko, toastr, $paramount);