(function ($, $p, ko, moment) {

    $p.models.EventOrganiser = function (data) {

        var me = this;

        me.eventOrganiserId = ko.observable(data.eventOrganiserId);
        me.email = ko.observable(data.email);
        me.lastModifiedDate = moment.utc(data.lastModifiedDateUtc).local().format($p.jsToDisplayDateFormat);
        me.status = ko.observable(getStatus(data));
        me.userId = ko.observable(data.userId);
        me.inviteToken = ko.observable(data.inviteToken);

        me.statusClass = ko.computed(function () {
            console.log(me.status());
            if (me.status() === 'Active') {
                return "label-success";
            }

            return 'label-warning';
        });
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

})(jQuery, $paramount, ko, moment);