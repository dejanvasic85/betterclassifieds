(function ($, $paramount, ko, moment) {
    'use strict';

    $paramount.models = $paramount.models || {};

    // Knockout Model
    $paramount.models.EventAd = function (data, options) {

        var me = this,
            adService = new $paramount.adService(options.ServiceEndpoint),
            MAX_TITLE_CHARS = 100,
            DATE_FORMAT = 'DD/MM/yyyy';

        // Properties
        me.title = ko.observable(data.Title);
        me.titleRemaining = ko.computed(function () {
            if (me.title() === null)
                return 100;
            return MAX_TITLE_CHARS - me.title().length;
        });
        me.description = ko.observable(data.Description);
        me.eventPhoto = ko.observable(data.EventPhoto);
        me.eventPhotoUploadError = ko.observable(null);
        me.eventPhotoUrl = ko.computed(function () {
            return $paramount.imageService.getImageUrl(me.eventPhoto());
        });
        me.removeEventPhoto = function () {
            if (me.eventPhoto() !== null) {
                adService.removeOnlineAdImage(me.eventPhoto()).done(function () {
                    me.eventPhoto(null);
                });
            }
            return;
        }
        me.configDurationDays = ko.observable(options.ConfigDurationDays);
        me.adStartDate = ko.observable(data.AdStartDate);
        me.adEndDate = ko.computed(function () {
            if (me.adStartDate() === '') {
                return '';
            }
            return moment(me.adStartDate(), DATE_FORMAT).add(options.ConfigDurationDays, 'days').format(DATE_FORMAT.toUpperCase());
        });
        me.location = ko.observable(data.Location);
        me.locationLatitude = ko.observable(data.LocationLatitude);
        me.locationLongitude = ko.observable(data.LocationLongitude);
        me.eventStartDate = ko.observable(data.EventStartDate);
        me.eventStartTime = ko.observable(data.EventStartTime);
        me.eventEndDate = ko.observable(data.EventEndDate);
        me.eventEndDateValidation = ko.computed(function () {
            // Ensure that the start date is less than end date
            if (moment(me.eventStartDate(), DATE_FORMAT).isAfter(moment(me.eventEndDate(), DATE_FORMAT))) {
                return 'End date must be after start date';
            }
            return '';
        });
        me.eventEndTime = ko.observable(data.EventEndTime);
        me.eventEndTimeValidation = ko.computed(function () {
            if (me.eventEndDateValidation().length > 0) {
                return '';
            }

            var startTimeValues = me.eventStartTime().split(':'),
                endTimeValues = me.eventEndTime().split(':');

            var startDate = moment(me.eventStartDate(), DATE_FORMAT).hours(startTimeValues[0]).minutes(startTimeValues[1]),
                endDate = moment(me.eventEndDate(), DATE_FORMAT).hours(endTimeValues[0]).minutes(endTimeValues[1]);

            if (startDate.isAfter(endDate)) {
                return 'End time must be after the start time';
            }

            return '';
        });

        me.organiserName = ko.observable(data.OrganiserName);
        me.organiserPhone = ko.observable(data.OrganiserPhone);

        // Ticketing
        me.tickets = ko.observableArray();
        $.each(data.Tickets, function (idx, item) {
            var ticketType = new $paramount.models.EventTicket(item);
            me.tickets.push(ticketType);
        });
        me.ticketingEnabled = ko.observable(data.TicketingEnabled);
        me.addTicketType = function () {
            var t = new $paramount.models.EventTicket({ ticketName: '', availableQuantity: 0, price: 0 });
            me.tickets.push(t);
        }
        me.removeTicketType = function (ticket) {
            me.tickets.remove(ticket);
        };

        me.submitChanges = function () {
            var json = ko.toJS(me);
            return adService.updateEventDetails(json);
        }
    };

    $paramount.models.EventTicket = function (data) {
        this.ticketName = ko.observable(data.TicketName);
        this.availableQuantity = ko.observable(data.AvailableQuantity);
        this.price = ko.observable(data.Price);
    }

})(jQuery, $paramount, ko, moment);