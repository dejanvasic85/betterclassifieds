(function ($, $p, ko) {

    $p.models.ManagePromoCodes = function (data) {
        var service = $p.PromoCodeService(data.eventId);
        var me = this;
        me.adId = data.adId;
        me.eventId = data.eventId;
        me.showAdd = ko.observable(false);
        me.promoCodes = $p.ko.bindArray(data.promoCodes, function (p) {
            return new $p.models.EventPromoCode(p);
        });

        me.newPromoCode = new $p.models.EventPromoCode({
            eventId: data.eventId,
            isDisabled: false
        });

        /*
        * Methods
        */

        me.cancelAdd = function () {
            me.showAdd(false);
        }

        me.displayAddPromo = function () {
            me.showAdd(true);
        }

        me.addPromoCode = function (model, event) {

            if (!$p.checkValidity(me.newPromoCode)) {
                return;
            }

            var $btn = $(event.target);
            $btn.loadBtn();


            var vm = ko.toJS(me.newPromoCode);
            service.add(vm).then(function (response) {
                if (response.errors) {
                    return;
                }

                me.promoCodes.push(new $p.models.EventPromoCode(response));
                toastr.success('Promo code has been added successfully.');
                me.showAdd(false);

            }).always(function() {
                $btn.resetBtn();
            });
        }

        me.remove = function (model, event) {
            var $btn = $(event.target);
            $btn.loadBtn();

            service.remove(model.eventPromoCodeId()).then(function () {
                me.promoCodes.remove(model);
            });
        }
    }

})(jQuery, $paramount, ko);