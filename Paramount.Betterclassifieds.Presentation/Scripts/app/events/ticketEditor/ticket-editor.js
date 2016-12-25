(function (ko, $p, toastr) {
    ko.components.register('ticket-editor', {
        viewModel: function (params) {
            $p.guard(params.eventId, 'eventId');

            var me = this,
                adDesignService = new $p.AdDesignService(),
                eventService = new $p.EventService(),
                resendGuestNotifications = false;

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
            me.displayNotificationProgress = ko.observable(false);
            me.ticketHasPurchases = ko.observable(false);
            me.guestsAffected = ko.observable(0);
            me.guestsNotified = ko.observable(0);

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
                if (ticketHasPurchases() === true) {
                    me.displayGuestPurchasesWarning(true);
                }
            }

            me.removeField = function (field) {
                me.eventTicketFields.remove(field);
                if (me.ticketHasPurchases() === true) {
                    me.displayGuestPurchasesWarning(true);
                }
            }

            me.saveTicket = function (model, event) {
                me.saveWithoutSendingNotifications(model, event);
            }

            me.ticketName.subscribe(function () {
                if (me.ticketHasPurchases()) {
                    me.displayGuestPurchasesWarning(true);
                }
            });

            me.saveWithoutSendingNotifications = function (model, event) {
                resendGuestNotifications = false;
                save(model, event);
            }

            me.saveAndSendNotifications = function (model, event) {
                resendGuestNotifications = true;
                save(model, event);
            }

            function save(model, event, options) {

                if (!$p.checkValidity(me)) {
                    return;
                }

                var $btn = $(event.target);
                $btn.loadBtn();

                // maps to UpdateEventTicketViewModel.cs
                var data = _.extend(options, {
                    eventTicket: ko.toJS(me)
                });

                adDesignService.editTicket(data)
                    .then(handleResponse)
                    .then(notify)
                    .then(function (pr) {
                        pr.then(function () {
                            me.displayNotificationProgress(false);
                            $btn.resetBtn();
                        });
                    });
            }

            function handleResponse(resp) {

                return new Promise(function (resolve, reject) {

                    if (resp.errors) {

                        if (_.some(resp.errors, ['key', 'GuestCountIncreased'])) {
                            me.displayGuestPurchasesWarning(true);
                            reject(resp);
                        }

                        reject(resp);
                    }

                    if (resendGuestNotifications === false) {
                        toastr.success("Ticket details saved successfully");
                    }

                    resolve(resp);

                });
            }

            function notify() {
                return new Promise(function (resolve, reject) {
                    if (resendGuestNotifications === false) {
                        resolve();
                        me.displayGuestPurchasesWarning(false);
                        return;
                    }

                    eventService.getGuests(me.eventId()).then(function (guests) {
                        if (guests.errors) {
                            reject(guests.errors);
                        }

                        me.displayNotificationProgress(true);
                        me.guestsAffected(guests.length);
                        me.guestsNotified(1);

                        var emailPromises = _.map(guests, function (g) {
                            return adDesignService.resendGuestEmail(g.ticketNumber).then(function() {
                                me.guestsNotified(me.guestsNotified() + 1);
                            });
                        });

                        Promise.all(emailPromises).then(function () {
                            resolve(guests);
                            me.displayGuestPurchasesWarning(false);
                            me.displayNotificationProgress(false);
                        }).catch(function(err) {
                            reject(err);
                        });
                    });
                });
            }
        },
        template: { path: $p.baseUrl + '/Scripts/app/events/ticketEditor/ticket-editor.html' }
    });
})(ko, $paramount, toastr);