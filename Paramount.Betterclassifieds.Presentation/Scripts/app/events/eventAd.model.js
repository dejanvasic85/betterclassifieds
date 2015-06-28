(function ($, $paramount, ko) {
    'use strict';

    $paramount.models = $paramount.models || {};

    // Knockout Model
    $paramount.models.EventAd = function (data) {
        var me = this;

        // Properties
        me.title = ko.observable(data.Title);
        me.description = ko.observable(data.Description);
        me.location = ko.observable(data.Location);
        me.locationLat = ko.observable(data.LocationLat);
        me.locationLong = ko.observable(data.LocationLong);
        me.reachedMaxImgCount = ko.observable(false);
        
        me.uploadImageInProgress = ko.observable(false);
        me.adImages = ko.observableArray();

        me.startDate = ko.observable(data.StartDate);
        me.startTimeHours = ko.observable();
        me.startTimeMinutes = ko.observable();
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