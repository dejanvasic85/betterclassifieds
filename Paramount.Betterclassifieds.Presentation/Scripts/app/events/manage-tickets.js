(function ($, ko, toastr, $p) {
    'use strict';

    var adDesignService;

    function ManageTickets(data) {
        var me = this;
        me.id = ko.observable(data.id);
        me.eventId = ko.observable(data.eventId);
        me.tickets = $p.ko.bindArray(data.tickets, function (t) { return new $p.models.EventTicket(t, 20); });
        me.isCreateEnabled = ko.observable(false);
        me.newTicket = ko.observable(new NewEventTicket());
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

    ManageTickets.prototype.createNewTicket = function () {
        var newTicket = this.newTicket();

        if ($paramount.checkValidity(newTicket)) {
            console.log('coming soon');
            //adDesignService.createEventTicket(newTicket).success(function() {
            //    console.log('done');
            //});
        }
    }

    function NewEventTicket() {
        var me = this;

        me.ticketName = ko.observable();
        me.price = ko.observable();
        me.availableQuantity = ko.observable();
        me.fields = ko.observableArray();
        
        me.validator = ko.validatedObservable({
            ticketName: me.ticketName.extend({ required: true }),
            price: me.price.extend({ min: 0, required: true }),
            availableQuantity: me.availableQuantity.extend({ min: 0, required: true })
        });

        me.addField = function() {
            me.fields.push(new $p.models.DynamicFieldDefinition());
        }
    }

    $p.models.ManageTickets = ManageTickets;

})(jQuery, ko, toastr, $paramount);