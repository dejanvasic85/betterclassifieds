/*
 * Contains any knockout extensions we may need
 */

(function (knockout, $) {


    /*
     * subscribeChanged method exposes the old and new value in the callback
     * e.g. this.observableValue.subscribeChanged(function(oldValue, newValue){ })
     */
    knockout.subscribable.fn.subscribeChanged = function (callback) {
        var oldValue;
        this.subscribe(function (_oldValue) {
            oldValue = _oldValue;
        }, this, 'beforeChange');

        var subscription = this.subscribe(function (newValue) {
            callback(oldValue, newValue);
        });

        return subscription;
    };

    ko.bindingHandlers.time = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            var time = ko.unwrap(valueAccessor());
            $(element).val(time).clockpicker({ donetext: 'OK' });
        }
    }

    ko.bindingHandlers.date = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            var date = ko.unwrap(valueAccessor());
            $(element)
                .attr('data-provide', "datepicker")
                .val(date)
                .datepicker({
                    autoclose: true,
                    format: 'dd/mm/yyyy',
                    todateBtn: true,
                    todayHighlight: true,
                    startDate: new Date()
                });
        }
    }

})(ko, jQuery);