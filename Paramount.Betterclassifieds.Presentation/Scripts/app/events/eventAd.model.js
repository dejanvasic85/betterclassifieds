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
        me.startTimeHours = ko.observable(data.EventStartTimeHours);
        me.startTimeMinutes = ko.observable(data.EventStartTimeMinutes);
        me.endDate = ko.observable();
        me.endTimeHours = ko.observable();
        me.endTimeMinutes = ko.observable();
        me.images = ko.observableArray();
        me.imageCountLimit = ko.observable(options.maxImages);
        me.organiserName = ko.observable();
        me.organiserPhone = ko.observable();
        me.adStartDate = ko.observable();

        me.errorMsg = ko.observable('');
    };


})(jQuery, $paramount, ko);