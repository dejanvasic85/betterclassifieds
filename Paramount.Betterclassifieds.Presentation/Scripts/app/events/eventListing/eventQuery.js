(function($p) {

    $p.EventQuery = function() {
        var me = this;
        me.query = {};

        return {
            withMax: function (max) {
                me.query.takeMax = max;
                return this;
            },
            withUser: function (user) {
                me.query.user = user;
                return this;
            },
            build: function () {
                return me.query;
            }
        }
    }

})($paramount);