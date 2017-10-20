(function ($p) {

    $p.EventQuery = function () {
        var me = this;
        me.query = {};

        return {
            withMax: function (max) {
                if (max) {
                    me.query.pageSize = max;
                }
                return this;
            },
            withUser: function (user) {
                if (user) {
                    me.query.user = user;
                }
                return this;
            },
            build: function () {
                return me.query;
            }
        }
    }

})($paramount);