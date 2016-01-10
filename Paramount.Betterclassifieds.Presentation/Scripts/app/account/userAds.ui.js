(function ($p, $, ko) {

    $p.ui = $p.ui || {};
    $p.ui.userAdsUI = {

        init: function () {
            var userAdService = new $p.UserAdService();
            var $rootElement = $('.user-ads');

            userAdService.getAdsForUser().done(function (data) {
                $rootElement.find('.progress').hide(500, function () {
                    var bookingsView = new $p.models.UserAdsView(data);
                    $rootElement.find('#adList').show();
                    $rootElement.find('#noAdsMsg').show();
                    ko.applyBindings(bookingsView, $rootElement.get(0));
                });
            });

            $rootElement.find('#adList').on('click', '.view-messages', function () {
                var context = ko.contextFor(this);
                context.$parent.userEnquiries(context.$data.messages());
                $rootElement.find('#userEnquiriesDialog').modal('show');
            });

            $rootElement.find('#adList').on('click', '.cancel-ad', function () {
                var context = ko.contextFor(this);
                context.$parent.setSelectedAd(context.$data.adId());
                $rootElement.find('#cancelAdDialog').modal('show');
            });

            $rootElement.find('#adList').on('click', '.book-again', function () {
                var context = ko.contextFor(this);
                context.$parent.setSelectedAd(context.$data.adId());
                $rootElement.find('#bookAgainDialog').modal('show');
            });

            $rootElement.find('#cancelAdDialog').on('click', '.confirm-cancel', function () {
                var context = ko.contextFor(this);
                var $btn = $(this);

                userAdService.cancelAd(context.$data.selectedAd()).done(function () {
                    $btn.button('reset');
                    $rootElement.find('#cancelAdDialog').modal('hide');
                    context.$data.confirmCancel();
                });
            });

            $rootElement.find('#bookAgainDialog').on('click', '.confirm-book-again', function () {
                var context = ko.contextFor(this);
                userAdService.startNewBookingFromTemplate(context.$data.selectedAd()).done(function (response) {
                    window.location = response.startUrl;
                });
            });
        }
    }

})($paramount, jQuery, ko);