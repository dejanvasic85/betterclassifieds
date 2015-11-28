/*
 * Contains any knockout extensions we may need
 */

(function (knockout) {

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

})(ko);