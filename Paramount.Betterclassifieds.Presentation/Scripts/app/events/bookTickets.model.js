(function ($, $paramount, ko, moment) {
    'use strict';

    $paramount.models = $paramount.models || {};
    $paramount.models.BookTickets = function (data) {
        var me = this;

        $.extend(data, {});

        // Initially start with 59 seconds so we take 1 min off automatically
        me.minsRemaining = ko.observable(data.reservationExpiryMinutes - 1);
        me.secondsRemaining = ko.observable(59);
        me.secondsRemainingDisplay = ko.computed(function() {
            return ("0" + me.secondsRemaining()).slice(-2);
        });
        var interval = setInterval(function (args) {
            if (me.minsRemaining() === 0 && me.secondsRemaining() === 0) {
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

})(jQuery, $paramount, ko, moment);