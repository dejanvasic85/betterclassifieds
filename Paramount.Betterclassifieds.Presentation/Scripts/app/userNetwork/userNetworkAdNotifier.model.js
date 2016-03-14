(function (jQuery, ko, $paramount) {

    function UserNetworkAdNotifier(data) {
        var userNetworkService = new $paramount.UsernetworkService();

        var me = this;
        me.adId = ko.observable(data.adId);
        me.notified = ko.observable(false);
        me.users = ko.observableArray();
        _.each(data.users, function (u) {
            me.users.push(new $paramount.models.UserNetwork(u));
        });

        me.newFriendName = ko.observable();
        me.newFriendEmail = ko.observable();

        /*
         * Validation
         */
        me.validator = ko.validatedObservable({
            newFriendName: me.newFriendName.extend({ required: true }),
            newFriendEmail: me.newFriendEmail.extend({ required: true, email: true })
        });

        /*
         * Submit  
         */
        me.notifyFriends = function () {
            var usersJs = ko.toJS(me.users());
            var selectedUsers = _.filter(usersJs, 'selected', true);

            userNetworkService.notifyFriends({
                adId: me.adId(),
                userNetworkUsers: selectedUsers
            });
        }

        /*
         * add friend
         */
        me.addFriend = function () {
            if ($paramount.checkValidity(me)) {
                var newUserNetwork = new $paramount.models.UserNetwork({
                    fullName: me.newFriendName(),
                    email: me.newFriendEmail(),
                    selected: true
                });

                var promise = userNetworkService.create(ko.toJS(newUserNetwork));
                promise.then(function () {
                    me.users.push(newUserNetwork);
                    me.clearValues();
                });

                return promise;
            }
            return null;
        }

        me.clearValues = function () {
            me.newFriendName(null);
            me.newFriendName.clearError();

            me.newFriendEmail(null);
            me.newFriendEmail.clearError();
        }

        me.no = ko.observable(false);
        me.anyUserSelected = ko.computed(function () {
            var anySelected = _.some(me.users(), function (u) {
                return u.selected() === true;
            });
            console.log(anySelected);
            return anySelected;
        });
    }

    $paramount.models = $paramount.models || {};
    $paramount.models.UserNetworkAdNotifier = UserNetworkAdNotifier;

})(jQuery, ko, $paramount);