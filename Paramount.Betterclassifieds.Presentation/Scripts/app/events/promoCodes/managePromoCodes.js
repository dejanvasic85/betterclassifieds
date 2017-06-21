(function ($, $p, ko) {

    $p.models.ManagePromoCodes = function (data) {
        var service = $p.PromoCodeService(data.eventId);
        var me = this;
        me.adId = data.adId;
        me.eventId = data.eventId;
        me.showAdd = ko.observable();
        me.promoCodes = $p.ko.bindArray(data.promoCodes, function(p) {
            return new $p.models.EventPromoCode(p);
        });

        me.add = function(model, event) {
            var $btn = $(event.target);
            $btn.btnLoad();
            var vm = ko.toJS(model);
            service.add(vm)
                .then(function(r) {
                    if (r.errors) {
                        return;
                    }

                    me.promoCodes.push(new $p.models.EventPromoCode(r.eventPromoCode));
                    toastr.success('Promo code added successfully');
                });
        }

        me.remove = function(model, event) {
            console.log()
        }
    }

})(jQuery, $paramount, ko);