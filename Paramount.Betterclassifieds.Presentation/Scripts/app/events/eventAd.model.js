(function ($, $paramount, ko, moment) {
    'use strict';

    $paramount.models = $paramount.models || {};

    // Knockout Model
    $paramount.models.EventAd = function (data) {
        
        var me = this,
            endDateChanged = false,
            maxTitleChars = 100;

        // Properties
        me.title = ko.observable(data.Title);
        me.titleRemaining = ko.computed(function () {
            if (me.title() === null)
                return 100;
            return maxTitleChars - me.title().length;
        });
        me.description = ko.observable(data.Description);
        me.eventPhoto = ko.observable(data.EventPhoto);
        me.adStartDate = ko.observable();

        me.location = ko.observable(data.Location);
        me.locationLat = ko.observable(data.LocationLatitude);
        me.locationLong = ko.observable(data.LocationLongitude);
        
        me.startDate = ko.observable(data.EventStartDate);
        me.startTime = ko.observable(data.EventStartTime);
        me.endDate = ko.observable(data.EventEndDate);
        me.endDateValidation = ko.computed(function() {
            // Ensure that the 
            var startDate = me.startDate();
            var endDate = me.endDate();
            console.log('start ' + startDate, ' end ', + endDate);
        });
        me.endTime = ko.observable(data.EventEndTime);

        me.organiserName = ko.observable(data.OrganiserName);
        me.organiserPhone = ko.observable(data.OrganiserPhone);
    };

})(jQuery, $paramount, ko, moment);