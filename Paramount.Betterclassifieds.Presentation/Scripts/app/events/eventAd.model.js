(function ($, $paramount, ko) {
    'use strict';

    $paramount.models = $paramount.models || {};

    // Knockout Model
    $paramount.models.EventAd = function (options) {

        // Properties
        this.location = ko.observable();
        this.locationGeometry = ko.observable();
        
    };


})(jQuery, $paramount, ko);