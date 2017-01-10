(function ($, ko, toastr, $p) {

    var adDesignService;

    function ManageTickets(data) {
        adDesignService = new $p.AdDesignService(data.id);

        var me = this;
        me.id = ko.observable(data.id);
        me.eventId = ko.observable(data.eventId);
        me.ticketSettings = new TicketSettings(data.eventId, data.ticketSettings);

        me.tickets = $p.ko.bindArray(data.tickets, function (t) {
            t.adId = data.id;
            return new $p.models.EventTicket(t, 20);
        });

        me.isCreateEnabled = ko.observable(false);
        me.newTicket = ko.observable(new NewEventTicket({ eventId: data.eventId }));

        me.removeField = function (f) {
            me.newTicket().eventTicketFields.remove(f);
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
            $btn.loadBtn();
            var eventTicketData = ko.toJS(newTicket);
            adDesignService.addEventTicket(eventTicketData)
                .success(function (resp) {
                    if (resp) {
                        toastr.success('Ticket added successfully');
                        me.newTicket(new NewEventTicket({ eventId: me.eventId() }));

                        eventTicketData.remainingQuantity = eventTicketData.availableQuantity;
                        eventTicketData.soldQty = 0;
                        eventTicketData.eventTicketId = resp.eventTicketId;
                        eventTicketData.adId = me.id();

                        me.tickets.push(new $p.models.EventTicket(eventTicketData, 20));
                        me.isCreateEnabled(false);
                    }
                })
                .always(function () {
                    $btn.resetBtn();
                });
        }
    }

    function NewEventTicket(data) {
        var me = this;

        me.eventId = ko.observable(data.eventId);
        me.ticketName = ko.observable();
        me.price = ko.observable();
        me.availableQuantity = ko.observable();
        me.eventTicketFields = ko.observableArray();

        me.validator = ko.validatedObservable({
            ticketName: me.ticketName.extend({ required: true }),
            price: me.price.extend({ min: 0, required: true }),
            availableQuantity: me.availableQuantity.extend({ min: 0, required: true })
        });

        me.addField = function () {
            me.eventTicketFields.push(new $p.models.DynamicFieldDefinition(me));
        }
    }

    function TicketSettings(eventId, data) {
        var me = this;

        me.eventId = ko.observable(eventId);
        me.includeTransactionFee = ko.observable(data.includeTransactionFee);
        me.showTicketAvailability = ko.observable(data.closingDate !== null || data.openingDate !== null);
        me.closingDate = ko.observable(data.closingDate);
        me.openingDate = ko.observable(data.openingDate);

        me.updateTicketSettings = function (model, event) {
            var $btn = $(event.target);
            $btn.loadBtn();

            adDesignService.editTicketSettings(model.eventId(), ko.toJS(model))
                .success(function () {
                    toastr.success("Settings updated successfully.");
                }).always(function () {
                    $btn.resetBtn();
                });
        }
    }

    $p.models.ManageTickets = ManageTickets;

})(jQuery, ko, toastr, $paramount);