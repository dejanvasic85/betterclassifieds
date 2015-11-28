(function ($, ko, $paramount) {
    'use strict';
    function DynamicFieldValue(data) {
        var me = this;
        me.fieldName = ko.observable();
        me.fieldValue = ko.observable();
        me.isRequired = ko.observable();

        if (data) {
            this.bindDynamicFieldValue(data);
        }

        // Validation
        me.validator = ko.validatedObservable({
            fieldValue: me.fieldValue.extend({ required: data.isRequired })
        });
    }

    DynamicFieldValue.prototype.bindDynamicFieldValue = function (data) {
        var me = this;
        me.fieldName(data.fieldName);
        me.fieldValue(data.fieldValue);
        me.isRequired(data.isRequired);
    }

    $paramount.models = $paramount.models || {};
    $paramount.models.DynamicFieldValue = DynamicFieldValue;

})(jQuery, ko, $paramount);