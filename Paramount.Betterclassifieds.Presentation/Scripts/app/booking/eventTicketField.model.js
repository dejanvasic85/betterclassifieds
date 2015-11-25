(function ($, ko, $paramount) {

    function EventTicketField(data) {
        var me = this;
        me.fieldName = ko.observable();
        me.isRequired = ko.observable();

        if (data) {
            me.EventTicketField(data);
        }
    }

    EventTicketField.prototype.bindEventTicketField = function(data) {
        var me = this;
        me.fieldName(data.fieldName);
        me.isRequired(data.isRequired);
    }
    
    $paramount.models = $paramount.models || {};
    $paramount.models.EventTicketField = EventTicketField;

})(jQuery, ko, $paramount);