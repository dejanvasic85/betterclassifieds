(function ($, $p) {
    'use strict';
    var $view = $('.add-guest-view');
    $p.ui = $p.ui || {};
    $p.ui.addGuest = {
        init: function (vm) {
            var addGuestModel = new $p.models.AddGuest();
            addGuestModel.bindData(vm);
            
            ko.applyBindings(addGuestModel, $view[0]);
        }
    }
})(jQuery, $paramount);