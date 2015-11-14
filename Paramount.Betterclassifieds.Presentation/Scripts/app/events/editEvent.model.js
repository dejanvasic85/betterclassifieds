(function ($, ko, $paramount) {
    'use strict';

    function EditEventModel(editEventViewModel) {
        var me = this;  
        me.tickets = ko.observableArray();
        me.addTicketType = function () {
            me.tickets.push(new $paramount.models.EventTicket({
                editMode: true,
                adId : editEventViewModel.adId,
                eventId: editEventViewModel.eventId,
                ticketName: 'Ticket Name',
                price : 0,
                availableQuantity: 0, 
                remainingQuantity: 0
            }));
        }
        me.bindEditEvent(editEventViewModel);
    }

    EditEventModel.prototype.bindEditEvent = function (editEventViewModel) {
        var me = this;
        $.each(editEventViewModel.tickets, function (idx, t) {
            t.adId = editEventViewModel.adId;
            me.tickets.push(new $paramount.models.EventTicket(t));
        });
    }

    $paramount.models = $paramount.models || {};
    $paramount.models.EditEventModel = EditEventModel;

})(jQuery, ko, $paramount);