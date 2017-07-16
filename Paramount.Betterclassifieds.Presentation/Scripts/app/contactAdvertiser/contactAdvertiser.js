(function ($, $p) {

    function ContactAdvertiser(params) {

        var me = this;
        me.adId = params.adId;
        me.isLoggedIn = params.isLoggedIn || false;
        me.fullName = ko.observable();
        me.email = ko.observable();
        me.question = ko.observable();
        me.submitted = ko.observable(false);

        /*
        *   Validation
        */
        me.validator = ko.validatedObservable({
            fullName: me.fullName.extend({ required: !me.isLoggedIn }),
            email: me.email.extend({ required: !me.isLoggedIn, email: true }),
            question: me.question.extend({ required: true })
        });


        /*
        *   Submit
        */ 
        me.submit = function (model, event) {
            if (!$p.checkValidity(model)) {
                return;
            }

            var $btn = $(event.target);
            $btn.loadBtn();


            $p.httpPost($p.baseUrl + 'listings/adenquiry', {
                adId: me.adId,
                fullName: me.fullName(),
                email: me.email(),
                question: me.question(),
                googleCaptchaResult: $('.g-recaptcha-response').val()
            }).then(function (r) {
                $btn.resetBtn();
                if (r.errors) {
                    return;
                }
                me.submitted(true);
            });
        }
    }

    ko.components.register('contact-advertiser', {
        viewModel: ContactAdvertiser,
        template: {
            path: $p.baseUrl + 'Scripts/app/contactAdvertiser/contactAdvertiser.html'
        }
    });


})(jQuery, $paramount);