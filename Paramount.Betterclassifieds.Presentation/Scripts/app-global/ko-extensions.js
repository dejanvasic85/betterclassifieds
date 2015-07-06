/*
 * Contains any knockout extensions we may need
 */

(function (knockout, $) {

    
    /*
     * subscribeChanged method exposes the old and new value in the callback
     * e.g. this.observableValue.subscribeChanged(function(oldValue, newValue){ })
     */
    knockout.subscribable.fn.subscribeChanged = function(callback) {
        var oldValue;
        this.subscribe(function(_oldValue) {
            oldValue = _oldValue;
        }, this, 'beforeChange');

        var subscription = this.subscribe(function(newValue) {
            callback(oldValue, newValue);
        });

        return subscription;
    };

    //knockout.bindingHandlers.clockpicker = {
    //    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
    //        $(element).clockpicker();
    //        // This will be called when the binding is first applied to an element
    //        // Set up any initial state, event handlers, etc. here
    //    },
    //    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
    //        // This will be called once when the binding is first applied to an element,
    //        // and again whenever any observables/computeds that are accessed change
    //        // Update the DOM element based on the supplied values here.
    //    }
    //}

})(ko, jQuery);