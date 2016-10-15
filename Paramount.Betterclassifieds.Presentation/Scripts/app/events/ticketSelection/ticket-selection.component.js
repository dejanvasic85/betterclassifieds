(function ($, ko, $p) {

    ko.components.register('ticket-selection', {
        viewModel: function (params) {
            if (_.isUndefined(params) || _.isUndefined(params.eventId) || _.isUndefined(params.groupsRequired)) {
                throw new Error('Components requires an eventId and gropsRequired properties but were undefined');
            }


            var me = this;
            me.groupsRequired = ko.observable(params.groupsRequired);
            me.eventId = ko.observable(params.eventId);
            me.availableTickets = ko.observableArray();

            if (params.groupsRequired === true) {
                


            } else {



            }

        },
        template: { path: '/Scripts/app/events/ticketSelection/ticket-selection.html' }
    });


    ko.components.register('ticket-option', {
        viewModel : function() {
            
        },
        template: '<div>I will be a ticket i promise</div>'

    });


})(jQuery, ko, $paramount);

