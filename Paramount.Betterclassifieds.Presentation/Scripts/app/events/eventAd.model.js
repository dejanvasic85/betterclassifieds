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
        me.locationGeometry = ko.observable();
        me.reachedMaxImgCount = ko.observable(false);
        me.errorMsg = ko.observable('');
        me.uploadImageInProgress = ko.observable(false);
        me.adImages = ko.observableArray();
    };


})(jQuery, $paramount, ko);