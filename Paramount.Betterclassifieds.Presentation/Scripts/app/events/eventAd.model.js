(function ($, $paramount, ko, moment, notifier) {
    'use strict';

    $paramount.models = $paramount.models || {};

    // Event ad details editing object used for knockout
    $paramount.models.EventAd = function (data, options) {

        var me = this,
            adService = options.adDesignService,
            imageService = options.imageService,
            MAX_TITLE_CHARS = 100,
            DATE_FORMAT = 'DD/MM/yyyy';

        // Properties
        me.title = ko.observable(data.title);
        me.titleRemaining = ko.computed(function () {
            if (me.title() === null)
                return 100;
            return MAX_TITLE_CHARS - me.title().length;
        });
        me.description = ko.observable(data.description);
        me.eventPhoto = ko.observable(data.eventPhoto);
        me.eventPhotoUploadError = ko.observable(null);
        me.eventPhotoUrl = ko.computed(function () {
            return imageService.getImageUrl(me.eventPhoto(), { w: 1000, h: 600 });
        });
        me.removeEventPhoto = function () {
            if (me.eventPhoto() !== null) {
                adService.removeOnlineAdImage(me.eventPhoto()).done(function () {
                    me.eventPhoto(null);
                });
            }
            return;
        }
        me.configDurationDays = ko.observable(options.configDurationDays);
        me.adStartDate = ko.observable(data.adStartDate);
        me.adEndDate = ko.computed(function () {
            if (me.adStartDate() === '') {
                return '';
            }
            return moment(me.adStartDate(), DATE_FORMAT).add(options.configDurationDays, 'days').format(DATE_FORMAT.toUpperCase());
        });
        me.location = ko.observable(data.location);
        me.locationLatitude = ko.observable(data.locationLatitude);
        me.locationLongitude = ko.observable(data.locationLongitude);
        me.eventStartDate = ko.observable(data.eventStartDate);
        me.eventStartTime = ko.observable(data.eventStartTime);
        me.eventEndDate = ko.observable(data.eventEndDate);
        me.eventEndDateValidation = ko.computed(function () {
            // Ensure that the start date is less than end date
            if (moment(me.eventStartDate(), DATE_FORMAT).isAfter(moment(me.eventEndDate(), DATE_FORMAT))) {
                return 'End date must be after start date';
            }
            return '';
        });
        me.eventEndTime = ko.observable(data.eventEndTime);
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

        me.organiserName = ko.observable(data.organiserName);
        me.organiserPhone = ko.observable(data.organiserPhone);
        me.ticketingEnabled = ko.observable(data.ticketingEnabled);
        me.submitChanges = function (element, event) {
            var json = ko.toJS(me);
            return adService.updateEventDetails(json)
            .then(function (resp) {
                    if (options.notifyUpdate === true && resp === true) {
                        notifier.success('Details updated successfully');
                    }
                })
            .always(function () {
                var $btn = $(event.target); // Grab the jQuery element from knockout
                    $btn.button('reset');
                });
        }
    };

})(jQuery, $paramount, ko, moment, toastr);