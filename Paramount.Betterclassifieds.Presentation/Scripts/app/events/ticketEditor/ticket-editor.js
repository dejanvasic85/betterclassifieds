(function (ko, $p, toastr) {
    ko.components.register('ticket-editor', {
        viewModel: function (params) {
            var me = this;
            me.ticketName = ko.observable();
            me.availableQuantity = ko.observable();
            me.price = ko.observable();
            me.eventTicketFields = ko.observableArray();
            me.editMode = ko.observable(false);
            me.soldQty = ko.observable();
            me.displayGuestPurchasesWarning = ko.observable(false);
            me.ticketHasPurchases = ko.observable(false);

            if (params.ticketDetails) {
                me.editMode(true);
                me.ticketName(params.ticketDetails.ticketName);
                me.availableQuantity(params.ticketDetails.availableQuantity);
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

                console.log('current ticket state:', ko.toJS(me));

                if (me.ticketHasPurchases()) {
                    me.displayGuestPurchasesWarning(true);

                } else {
                    me.saveWithoutSendingNotifications();
                }
            }

            me.saveWithoutSendingNotifications = function () {
                toastr.success('Ticket details saved successfully.');
            }

            me.saveAndSendNotifications = function () {
                
            }
        },
        template: { path: $p.baseUrl + '/Scripts/app/events/ticketEditor/ticket-editor.html' }
    });
})(ko, $paramount, toastr);