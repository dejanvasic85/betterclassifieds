(function (ko, $p, toastr) {
    ko.components.register('add-organiser', {

        viewModel: function (params) {
            var organiserService = new $p.OrganiserService(params.eventId);

            var me = this;
            me.email = ko.observable();

            me.submit = function (model, event) {

                if (!$p.checkValidity(me)) {
                    return;
                }
                var $btn = $(event.target);
                $btn.loadBtn();

                organiserService.addOrganiser(me.email())
                    .success(function (r) {
                        if (r.errors) {
                            return;
                        }

                        me.email(null);

                        if (params.onSuccess) {
                            params.onSuccess(r);
                        }
                    });
            }

            me.cancel = function () {
                me.email(null);
                if (params.onCancel) {
                    params.onCancel();
                }
            }

            me.validator = ko.validatedObservable({
                email: me.email.extend({
                    required: true,
                    email: true,
                    maxLength: 100,
                    mustNotEqual: {
                        params: params.ownerEmail,
                        message: 'This email is already the owner'
                    }
                })
            });

        },
        template: { path: $p.baseUrl + 'Scripts/app/events/organisers/add-organiser.html' }
    });
})(ko, $paramount, toastr);