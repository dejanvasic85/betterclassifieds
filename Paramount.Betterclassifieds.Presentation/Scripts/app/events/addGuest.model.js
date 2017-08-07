(function ($, $p, ko) {

    function AddGuest() {
        this.id = ko.observable();
        this.eventId = ko.observable();
        this.isSeatedEvent = ko.observable();
        this.guestFullName = ko.observable();
        this.guestEmail = ko.observable();
        this.selectedTicket = ko.observable();
        this.seatNumber = ko.observable();
        this.sendEmailToGuest = ko.observable(true);
        this.tickets = ko.observableArray();
        this.ticketFields = ko.observableArray();
        this.promoCode = ko.observable();
        this.saved = ko.observable();
        this.selectedGroup = ko.observable();
        this.displayGuests = ko.observable();
        this.isPublic = ko.observable();
        this.hasGroups = ko.observable(false);
        this.eventGroups = ko.observableArray();
        this.seats = ko.observableArray();
        this.guestAddWarning = ko.observable('');
        this.showSeatWarning = ko.observable(false);
        this.disableAddGuest = ko.observable();
        this.validator = ko.validatedObservable({
            guestFullName: this.guestFullName.extend({ required: true }),
            guestEmail: this.guestEmail.extend({ required: true, email: true })
        });
        this.isLoading = ko.observable(true);
        this.cachedEventSeating = [];

        var me = this;
        me.seatSelected = function (seatNumber) {
            me.showSeatWarning(false);
            var seat = _.find(me.cachedEventSeating, function (s) {
                return s.seatNumber === seatNumber;
            });
            if (seat.available === false) {
                me.showSeatWarning(true);
                me.seatNumber(null);
                me.selectedTicket(null);
                me.disableAddGuest(true);
            } else {
                var eventTicketId = seat.eventTicketId;
                var eventTicket = _.find(me.tickets(), function (t) {
                    return t.eventTicketId() === eventTicketId;
                });
                me.selectedTicket(eventTicket);
                me.ticketChanged();
                me.disableAddGuest(false);
            }
        }

        me.getSelectedTicketName = ko.computed(function () {
            return me.selectedTicket() && me.selectedTicket().ticketName();
        });

        me.getSelectedTicketRemainingCount = ko.computed(function () {
            return me.selectedTicket() && me.selectedTicket().remainingQuantity();
        });
    }

    AddGuest.prototype.bindData = function (data) {
        var service = new $p.EventService();

        var me = this;
        me.id(data.id);
        me.eventId(data.eventId);
        me.guestFullName(data.guestFullName);
        me.guestEmail(data.guestEmail);
        me.displayGuests(data.displayGuests);
        me.isPublic(data.isPublic);
        me.hasGroups(data.eventGroups && data.eventGroups.length > 0);
        me.isSeatedEvent(data.isSeatedEvent);
        me.disableAddGuest(data.isSeatedEvent === true);

        _.each(data.ticketFields, function (tf) {
            me.ticketFields.push(new $p.models.DynamicFieldValue(tf));
        });

        _.each(data.eventTickets, function (et) {
            me.tickets.push(new $p.models.EventTicket(et, 5));
        });

        _.each(data.eventGroups, function (gr) {
            me.eventGroups.push(new $p.models.EventGroup(gr));
        });

        if (data.isSeatedEvent === true) {
            service.getEventSeating(data.eventId).then(function (response) {
                if (!response.rows) {
                    toastr.error('Unable to find any seats. Please ensure the seating has been setup first.');
                }
                _.each(response.rows, function (r) {
                    _.each(r.seats, function (s) {
                        me.seats.push(s.seatNumber);
                        me.cachedEventSeating.push(s);
                    });
                });
            });
        } else {
            me.isLoading(false);
        }
    }

    AddGuest.prototype.toModel = function (vm) {
        // maps to AddEventGuestViewModel
        return {
            id: vm.id(),
            eventId: vm.eventId(),
            guestFullName: vm.guestFullName(),
            guestEmail: vm.guestEmail(),
            isPublic: vm.isPublic(),
            seatNumber: vm.seatNumber(),
            sendEmailToGuest: vm.sendEmailToGuest(),
            selectedTicket: ko.toJS(vm.selectedTicket()),
            selectedGroup: ko.toJS(vm.selectedGroup),
            promoCode : vm.promoCode()
        }
    }

    AddGuest.prototype.ticketChanged = function () {
        var me = this;
        var service = new $p.EventService();
        me.ticketFields.removeAll();
        service.getFieldsForTicket(me.selectedTicket().eventTicketId()).then(function (resp) {
            _.each(resp, function (tf) {
                me.ticketFields.push(new $p.models.DynamicFieldValue(tf));
            });
        });

        me.eventGroups.removeAll();
        service.getGroupsForTicket(me.eventId(), me.selectedTicket().eventTicketId()).then(function (resp) {
            _.each(resp, function (gr) {
                me.eventGroups.push(new $p.models.EventGroup(gr));
            });
        });
    }

    AddGuest.prototype.addAnother = function (mode, event) {
        $(event.target).loadBtn();
        location.reload();
    }

    /*
    *   Submit 
    */
    AddGuest.prototype.submitGuest = function (element, event) {
        var me = this;
        if (!$p.checkValidity(me) || !$p.checkValidity(me.ticketFields())) {
            return;
        }

        if (me.isSeatedEvent() === true && $p.isNullOrUndefined(me.seatNumber())) {
            me.guestAddWarning('Please ensure seat is selected');
            return;
        }

        var $btn = $(event.target);
        $btn.button('loading');

        var objToSend = this.toModel(me);

        var service = new $p.AdDesignService(me.id());
        service.addGuest(objToSend).then(function (r) {
            if (r.errors) {
                return;
            }

            $btn.button('reset');
            me.saved(true);
        });
    }

    $p.models = $p.models || {};
    $p.models.AddGuest = AddGuest;

})(jQuery, $paramount, ko);