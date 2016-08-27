(function ($, ko, $paramount) {
    'use strict';

    function DynamicFieldDefinition(parent, data) {
        var me = this;
        me.fieldName = ko.observable().extend({
            validation: {
                validator: function (name, params) {
                    var allFields = params.parent.eventTicketFields.peek();
                    
                    var otherFields = _.filter(allFields, function (row) {
                        return row !== params.currentRow;
                    });

                    var otherFieldNames = _.map(otherFields, function(f) {
                        return f.fieldName.peek();
                    });
                    
                    return !_.contains(otherFieldNames, name);
                    
                },
                message: 'Field Name must be unique',
                params: {
                    currentRow: me,
                    parent: parent
                }
            }
        });
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