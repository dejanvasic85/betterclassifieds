(function ($, $p, ko, moment) {

    $p.models.EventOrganiser = function (data) {

        var me = this;

        me.eventOrganiserId = ko.observable(data.eventOrganiserId);
        me.email = ko.observable(data.email);
        me.lastModifiedDate = moment.utc(data.lastModifiedDateUtc).local().format($p.jsToDisplayDateFormat);
        me.status = ko.observable(getStatus(data));

        me.remove = function(model, event) {
            
        }
    }
    
    function getStatus(organiser) {
        if (organiser.userId !== null && organiser.userId !== '') {
            return "Active";
        }

        if (organiser.inviteToken !== null) {
            return "Invited";
        }

        return null;
    }

    function getStatusClass(status) {
        if (status === 'Active') {
            return "success";
        }

        if (status === 'Invited') {
            return "warning";
        }
    }

})(jQuery, $paramount, ko, moment);