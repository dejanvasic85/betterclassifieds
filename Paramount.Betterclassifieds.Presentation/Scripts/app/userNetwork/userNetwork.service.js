(function () {

    var baseUrl = $paramount.baseUrl + 'UserNetwork';

    function UserNetworkService() { }

    UserNetworkService.prototype.create = function (userNetwork) {
        return $paramount.httpPost(baseUrl + '/Create', userNetwork);
    };

    UserNetworkService.prototype.notifyFriends = function (notification) {
        return $paramount.httpPost(baseUrl + '/NotifyAd', notification);
    };

    $paramount.UserNetworkService = UserNetworkService;
})();