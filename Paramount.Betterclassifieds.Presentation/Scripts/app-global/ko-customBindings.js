/*
 * Extends knockout by adding extra bindings for elements
 */

(function (knockout, $) {

    ko.bindingHandlers.time = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            var accessor = valueAccessor();
            var time = ko.unwrap(accessor);
            $(element)
                .val(time)
                .attr('readonly', '')               // Prevents the user from inputting their own value
                .addClass('bs-clock-picker')        // Prevents the user from inputting their own value
                .clockpicker({
                    donetext: 'OK',
                    autoclose: true,
                    afterDone : function() {
                        accessor($(element).val());
                    }
                });
        }
    }

    /*
     * Usage : <input type='text' data-bind='date: modelProperty' />
     */
    ko.bindingHandlers.date = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            var value = valueAccessor();
            var date = ko.unwrap(value);
            $(element)
                .attr('data-provide', "datepicker")
                .attr('readonly', "") // Prevents the user from inputting their own value
                .addClass('bs-date-picker') // Prevents the user from inputting their own value
                .val(date)
                .datepicker({
                    autoclose: true,
                    format: 'dd/mm/yyyy',
                    todateBtn: true,
                    todayHighlight: true,
                    startDate: new Date(),
                    orientation: 'bottom'
                })
                .on('changeDate', function () {
                    var changedDate = $(element).val();
                    value(changedDate);
                });
        }
    }

})(ko, jQuery);