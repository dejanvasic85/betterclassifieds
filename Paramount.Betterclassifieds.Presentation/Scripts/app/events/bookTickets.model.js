(function ($, $paramount, ko) {
    'use strict';

    $paramount.models = $paramount.models || {};
    $paramount.models.BookTickets = function (data) {
        var me = this;

        $.extend(data, {});
        
        me.minsRemaining = ko.observable(data.reservationExpiryMinutes);
        me.secondsRemaining = ko.observable(data.reservationExpirySeconds);
        me.secondsRemainingDisplay = ko.computed(function() {
            return ("0" + me.secondsRemaining()).slice(-2);
        });
        var interval = setInterval(function () {
            if (me.minsRemaining() === 0 && me.secondsRemaining() === 1) {
                window.clearInterval(interval);
            }

            if (me.secondsRemaining() > 0) {
                var updatedSecs = me.secondsRemaining() - 1;
                me.secondsRemaining(updatedSecs);
            } else {
                var updatedMins = me.minsRemaining() - 1;
                me.minsRemaining(updatedMins);
                me.secondsRemaining(59);
            }
        }, 1000);
    }

})(jQuery, $paramount, ko);