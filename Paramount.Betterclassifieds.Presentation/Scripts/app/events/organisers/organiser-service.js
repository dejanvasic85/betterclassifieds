(function ($, $p) {

    $p.OrganiserService = function (eventId) {
        var me = this,
            baseUrl = $p.baseUrl + 'event-dashboard/' + eventId + '/organisers';

        return {

            addOrganiser: function (email) {

                return $p.httpPost(baseUrl + '/invite', { email: email });

            },

            removeOrganiser: function (eventOrganiserId) {
                return $p.httpPost(baseUrl + '/remove', { eventOrganiserId: eventOrganiserId });
            }

        }

    }

})(jQuery, $paramount);