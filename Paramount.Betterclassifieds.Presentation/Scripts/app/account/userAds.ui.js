(function ($p, $, ko) {

    $p.ui = $p.ui || {};

    $p.ui.userAdsUI = {

        init: function () {

            var $eventList = $('#eventList');
            if ($eventList.length > 0) {
                ko.applyBindings({}, $eventList.get(0));
            }

            var $adList = $('#adList');
            if ($adList.length > 0) {
                ko.applyBindings({}, $adList.get(0));
            }

        }
    }

})($paramount, jQuery, ko);