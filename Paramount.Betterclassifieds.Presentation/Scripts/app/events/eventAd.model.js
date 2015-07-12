(function ($, $paramount, ko, moment) {
    'use strict';

    $paramount.models = $paramount.models || {};

    // Knockout Model
    $paramount.models.EventAd = function (data, options) {
        
        var me = this,
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
        me.configDurationDays = ko.observable(options.ConfigDurationDays);
        me.adStartDate = ko.observable(data.AdStartDate);
        me.adEndDate = ko.computed(function () {
            if (me.adStartDate() === '') {
                return '';
            }
            return moment(me.adStartDate(), DATE_FORMAT).add(options.ConfigDurationDays, 'days').format(DATE_FORMAT.toUpperCase());
        });

        me.location = ko.observable(data.Location);
        me.locationLat = ko.observable(data.LocationLatitude);
        me.locationLong = ko.observable(data.LocationLongitude);
        
        me.startDate = ko.observable(data.EventStartDate);
        me.startTime = ko.observable(data.EventStartTime);
        me.endDate = ko.observable(data.EventEndDate);
        me.endDateValidation = ko.computed(function() {
            // Ensure that the start date is less than end date
            if (moment(me.startDate(), DATE_FORMAT).isAfter( moment(me.endDate(), DATE_FORMAT) )) {
                return 'End date must be after start date';
            }
            return '';
        });
        me.endTime = ko.observable(data.EventEndTime);
        me.endTimeValidation = ko.computed(function () {
            if (me.endDateValidation().length > 0) {
                return '';
            }

            var startTimeValues = me.startTime().split(':'),
                endTimeValues = me.endTime().split(':');

            var startDate = moment(me.startDate(), DATE_FORMAT).hours(startTimeValues[0]).minutes(startTimeValues[1]),
                endDate = moment(me.endDate(), DATE_FORMAT).hours(endTimeValues[0]).minutes(endTimeValues[1]);

            if (startDate.isAfter(endDate)) {
                return 'End time must be after the start time';
            }

            return '';
        });

        me.organiserName = ko.observable(data.OrganiserName);
        me.organiserPhone = ko.observable(data.OrganiserPhone);
    };

})(jQuery, $paramount, ko, moment);