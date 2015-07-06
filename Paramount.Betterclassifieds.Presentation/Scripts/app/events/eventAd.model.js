(function ($, $paramount, ko) {
    'use strict';

    $paramount.models = $paramount.models || {};

    // Knockout Model
    $paramount.models.EventAd = function (data, options) {
        var me = this;

        // Properties
        me.title = ko.observable(data.Title);
        me.description = ko.observable(data.Description);
        me.location = ko.observable(data.Location);
        me.locationLat = ko.observable(data.LocationLatitude);
        me.locationLong = ko.observable(data.LocationLongitude);
        me.reachedMaxImgCount = ko.observable(false);
        
        me.uploadImageInProgress = ko.observable(false);
        me.eventPhoto = ko.observable(data.EventPhoto);

        me.startDate = ko.observable(data.EventStartDate);
        me.startTime = ko.observable(data.EventStartTime);
        me.startTimeHours = ko.observable(data.EventStartTimeHours);
        me.startTimeMinutes = ko.observable(data.EventStartTimeMinutes);

        me.endDate = ko.observable(data.EventEndDate);
        me.endTimeHours = ko.observable(data.EventEndTimeHours);
        me.endTimeMinutes = ko.observable(data.EventEndTimeMinutes);
        me.images = ko.observableArray();
        me.imageCountLimit = ko.observable(options.maxImages);
        me.organiserName = ko.observable();
        me.organiserPhone = ko.observable();
        me.adStartDate = ko.observable();

        // Options
        me.hourOptions = ko.observableArray(generateNumbers(1, 24));
        me.minuteOptions = ko.observableArray(generateNumbers(1, 60));
    };

    function generateNumbers(seed, max) {
        var values = [];
        for (var i = seed; i <= max; i++) {
            if (i < 10) {
                i = "0" + i;
            }
            values.push("" + i);
        }
        return values;
    }

})(jQuery, $paramount, ko);