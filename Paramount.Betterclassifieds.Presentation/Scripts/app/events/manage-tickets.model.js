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
            return new $p.models.EventTicket(t);
        });

        me.isCreateEnabled = ko.observable(false);

        me.removeField = function (f) {
            me.newTicket().eventTicketFields.remove(f);
        }

        me.ticketSaved = function (newTicketDetails) {
            toastr.success('Ticket ' + newTicketDetails.ticketName + ' has been added successfully.');
            newTicketDetails.soldQty = 0;
            newTicketDetails.adId = me.id();
            me.tickets.push(new $p.models.EventTicket(newTicketDetails));
            me.isCreateEnabled(false);
        }
    }

    ManageTickets.prototype.startNewTicket = function () {
        this.isCreateEnabled(true);
    }

    ManageTickets.prototype.cancelNewTicket = function () {
        this.isCreateEnabled(false);
    }

    function TicketSettings(eventId, data) {
        var me = this;

        me.eventId = ko.observable(eventId);
        me.includeTransactionFee = ko.observable(data.includeTransactionFee);
        me.closingDate = ko.observable(data.closingDate);
        me.openingDate = ko.observable(data.openingDate);

        me.updateTicketSettings = function (model, event) {
            var $btn = $(event.target);
            $btn.loadBtn();

            adDesignService.editTicketSettings(model.eventId(), ko.toJS(model))
                .success(function (resp) {
                    if (!resp.errors) {
                        toastr.success("Settings updated successfully.");
                    }
                });
        }
    }

    $p.models.ManageTickets = ManageTickets;

})(jQuery, ko, toastr, $paramount);