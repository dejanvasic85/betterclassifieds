(function ($, ko, toastr, $p) {

    function ManageTickets(data) {
        var me = this;
        me.id = ko.observable(data.id);
        me.eventId = ko.observable(data.eventId);
        me.tickets = $p.ko.bindArray(data.tickets, function (t) { return new $p.models.EventTicket(t, 20) });
    }

    ManageTickets.prototype.createStart = function () {
        
    }

    $p.models.ManageTickets = ManageTickets;

})(jQuery, ko, toastr, $paramount);