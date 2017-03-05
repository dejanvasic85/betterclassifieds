(function ($, $p, ko, toastr) {

    $p.models.ManageOrganisers = function (data) {

        var me = this;
        var organiserService = new $p.OrganiserService(data.eventId);

        me.showAddOrganiser = ko.observable(false);
        me.organisers = $p.ko.bindArray(data.organisers, function (orgData) {
            return new $p.models.EventOrganiser(orgData);
        });

        // Triggers when add-organiser component fires
        me.organiserAdded = function (organiser) {
            toastr.success('Organiser has been invited.');
            me.organisers.push(new $p.models.EventOrganiser(organiser));
            me.showAddOrganiser(false);
        }

        me.cancelAdd = function () {
            me.showAddOrganiser(false);
        }

        me.addOrganiser = function () {
            me.showAddOrganiser(true);
        }

        me.removeOrganiser = function (model) {
            var eventOrganiserId = model.eventOrganiserId();
            organiserService.removeOrganiser(eventOrganiserId)
                .then(function (resp) {
                    if (resp === true) {
                        me.organisers.remove(model);
                        toastr.warning("Organiser " + model.email() + " has been removed");
                    }
                });
        }
    }

})(jQuery, $paramount, ko, toastr);