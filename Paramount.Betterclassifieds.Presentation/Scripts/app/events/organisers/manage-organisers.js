(function($, $p, ko, toastr) {
    
    $p.models.ManageOrganisers = function(data) {

        var me = this;
        me.showAddOrganiser = ko.observable(false);
        me.organisers = $p.ko.bindArray(data.organisers, function(orgData) {
            return new $p.models.EventOrganiser(orgData);
        });
        
        // Triggers when add-organiser component fires
        me.organiserAdded = function (organiser) {
            toastr.success('Organiser has been invited.');
            console.log(organiser);
            me.organisers.push(new $p.models.EventOrganiser(organiser));
            me.showAddOrganiser(false);
        }

        me.cancelAdd = function() {
            me.showAddOrganiser(false);
        }

        me.addOrganiser = function() {
            me.showAddOrganiser(true);
        }
    }

})(jQuery, $paramount, ko, toastr);