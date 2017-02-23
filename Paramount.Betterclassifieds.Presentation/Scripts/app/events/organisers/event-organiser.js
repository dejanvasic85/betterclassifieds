(function($, $p, ko, moment) {
    
    $p.models.EventOrganiser = function(data) {

        var me = this;

        me.eventOrganiserId = ko.observable(data.eventOrganiserId);
        me.eventId = ko.observable(data.eventId);
        me.userId = ko.observable(data.userId);
        me.email = ko.observable(data.email);
        me.lastModifiedDate = moment.utc(data.lastModifiedDateUtc).local();

    }

})(jQuery, $paramount, ko, moment);