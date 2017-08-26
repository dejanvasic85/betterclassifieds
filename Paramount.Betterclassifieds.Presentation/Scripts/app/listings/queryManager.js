(function ($p) {

    $p.QueryManager = function () {
        var me = this;
        me.query = {};

        return {
            withPageSize: function (pageSize) {
                if (pageSize) {
                    me.query.pageSize = pageSize;
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