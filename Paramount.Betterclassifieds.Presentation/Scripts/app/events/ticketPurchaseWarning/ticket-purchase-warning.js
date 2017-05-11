(function ($, ko, $p) {

    ko.components.register('ticket-purchase-warning', {

        viewModel: function (params) {

            this.soldQty = params.soldQty;
            this.save = params.save;
            this.saveWithoutNotifications = params.saveWithoutNotifications;
            
        },


        template: { path: $p.baseUrl + '/Scripts/app/events/ticketPurchaseWarning/ticket-purchase-warning.html' }
    });

})(jQuery, ko, $paramount);