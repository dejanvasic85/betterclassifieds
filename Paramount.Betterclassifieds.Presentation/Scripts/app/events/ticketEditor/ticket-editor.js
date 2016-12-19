(function (ko, $p, toastr) {
    ko.components.register('ticket-editor', {
        viewModel: function (params) {
            console.log(params);
            $p.guard(params.eventId, 'eventId');

            var me = this,
                adDesignService = new $p.AdDesignService();

            me.eventId = ko.observable(params.eventId); // Must be set!
            me.eventTicketId = ko.observable();
            me.ticketName = ko.observable();
            me.availableQuantity = ko.observable(); // Only available in create
            me.remainingQuantity = ko.observable(); // Only available in edit
            me.price = ko.observable();
            me.eventTicketFields = ko.observableArray();
            me.editMode = ko.observable(false);
            me.soldQty = ko.observable();
            me.displayGuestPurchasesWarning = ko.observable(false);
            me.ticketHasPurchases = ko.observable(false);

            if (params.ticketDetails) {

                adDesignService = new $p.AdDesignService(params.ticketDetails.id);
                me.eventTicketId(params.ticketDetails.eventTicketId);
                me.editMode(true);
                me.ticketName(params.ticketDetails.ticketName);
                me.availableQuantity(params.ticketDetails.availableQuantity);
                me.remainingQuantity(params.ticketDetails.remainingQuantity);
                me.price(params.ticketDetails.price);
                me.ticketHasPurchases(params.ticketDetails.soldQty > 0);
                me.soldQty(params.ticketDetails.soldQty);
                _.each(params.ticketDetails.eventTicketFields, function (field) {
                    me.eventTicketFields.push(new $p.models.DynamicFieldDefinition(me, field));
                });
            }

            me.addField = function () {
                me.eventTicketFields.push(new $p.models.DynamicFieldDefinition(me));
            }

            me.removeField = function (field) {
                me.eventTicketFields.remove(field);
            }

            me.saveTicket = function () {
                me.saveWithoutSendingNotifications();
            }

            me.ticketName.subscribe(function () {
                if (me.ticketHasPurchases()) {
                    me.displayGuestPurchasesWarning(true);
                }
            });

            me.saveWithoutSendingNotifications = function () {
                save({
                    resendNotifications: false
                });
            }

            me.saveAndSendNotifications = function () {
                save({
                    resendNotifications: true
                });
            }

            function save(options) {
                var data = _.extend(options, ko.toJS(me));
                adDesignService.editTicket(data).then(showSuccess);
            }

            function showSuccess() {
                toastr.success("Ticket details saved successfully");
            }
        },
        template: { path: $p.baseUrl + '/Scripts/app/events/ticketEditor/ticket-editor.html' }
    });
})(ko, $paramount, toastr);