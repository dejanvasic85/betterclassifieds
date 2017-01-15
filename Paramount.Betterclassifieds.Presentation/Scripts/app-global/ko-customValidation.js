/*
 * Sets up the global settings for the bootstrap validation styling
 */


(function (ko, dateService) {

    ko.validation.init({
        errorElementClass: 'has-error',
        errorMessageClass: 'help-block',
        decorateInputElement: true,
        grouping: {
            deep: true,
            live: true,
            observable: true
        }
    });


    /*
     * Min date validation
     * example : 
     * me.validator = ko.validatedObservable({
            closingDate: me.closingDate.extend({
                pmtMinDate: new Date(data.adStartDate)
            })
        });
     */

    ko.validation.rules['pmtMinDate'] = {
        validator: function (val, otherVal) {
            if (typeof val === 'undefined' || val === null) {
                return true;
            }

            var minDate = dateService(otherVal);
            var selectedDate = dateService(val, "DD/MM/YYYY");

            return minDate.isBefore(selectedDate);
        },
        message: 'Must be a later date'
    }

    ko.validation.rules['mustBeAfter'] = {
        validator: function (val, comparisonVal) {
            if (!val) {
                return false;
            }

            if (!comparisonVal) {
                return false;
            }

            return moment(comparisonVal).isBefore(moment(val));
        },
        message: 'Must be a later date'
    };

    ko.validation.registerExtenders();
    
})(ko, moment);