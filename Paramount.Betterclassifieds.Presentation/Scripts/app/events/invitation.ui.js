(function($, $paramount) {

    'use strict';
    $paramount.ui = $paramount.ui || {};
    $paramount.ui.invitation = {
        init : function(eventId, availableTickets) {
            
            var service = new $paramount.EventService();

            // On ready
            $(function() {
                $('button[data-ticket]').on('click', function() {
                    var $me = $(this);
                    var selected = $me.data().ticket;
                    var ticket = _.find(availableTickets, function(t) { return t.eventTicketId === selected });
                    ticket.selectedQuantity = 1;

                    var order = {
                        eventId : eventId,
                        tickets : [ticket]
                    }

                    service.startTicketOrder(order);
                });
            });
        }
    }
    
})(jQuery, $paramount)