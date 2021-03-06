﻿(function ($, $p) {

    $p.PromoCodeService = function (eventId) {
        var me = this,
            baseUrl = $p.baseUrl + 'event-dashboard/' + eventId + '/promos';

        return {

            add: function (eventPromo) {
                return $p.httpPost(baseUrl + '/create', { eventPromo: eventPromo });
            },

            remove: function (eventPromoCodeId) {
                return $p.httpPost(baseUrl + '/remove', { eventPromoCodeId: eventPromoCodeId });
            }

        }

    }

})(jQuery, $paramount);