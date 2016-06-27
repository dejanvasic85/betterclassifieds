(function($, ko, $p) {
    'use strict';

    function ManageGroups(data) {
        var me = this;

        me.eventGroups = ko.observableArray();
        me.tickets = ko.observableArray();

        if (data) {
            me.bind(data);
        }
    }

    ManageGroups.prototype.bind = function(data) {
        var me = this;

        _.each(data.eventGroups, function(gr) {
            me.eventGroups.push(new $p.models.EventGroup(gr));
        });

    }

    $paramount.models.ManageGroups = ManageGroups;

})(jQuery, ko, $paramount);