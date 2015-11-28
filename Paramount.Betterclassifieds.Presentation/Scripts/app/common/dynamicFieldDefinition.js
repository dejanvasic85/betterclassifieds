(function ($, ko, $paramount) {
    'use strict';

    function DynamicFieldDefinition(data) {
        var me = this;
        me.fieldName = ko.observable();
        me.isRequired = ko.observable();

        if (data) {
            this.bindDynamicFieldDefinition(data);
        }

        // Validation
        me.validator = ko.validatedObservable({
            fieldName: me.fieldName.extend({ required: true })
        });
    }

    DynamicFieldDefinition.prototype.bindDynamicFieldDefinition = function (data) {
        var me = this;
        me.fieldName(data.fieldName);
        me.isRequired(data.isRequired);
    }

    $paramount.models = $paramount.models || {};
    $paramount.models.DynamicFieldDefinition = DynamicFieldDefinition;

})(jQuery, ko, $paramount);