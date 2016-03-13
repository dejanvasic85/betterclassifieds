(function (jQuery, ko, $paramount) {

    $paramount.ui.notifyNetworkAdUi = {
        init: function (data) {
            $(function () {
                var $userNetworkUi = $('#userNetwork');
                if ($userNetworkUi.length === 0) {
                    throw "userNetwork element is missing for ko applyBindings";
                }

                var viewModel = new $paramount.models.UserNetworkAdNotifier(data);
                ko.applyBindings(viewModel, $userNetworkUi[0]);
            });
        }
    }

})(jQuery, ko, $paramount);