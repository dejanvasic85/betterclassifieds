
/*
** Supports the login view functionality
*/

(function ($) {

    // JQuery on-ready
    $(function () {

        // Forgot password functionality
        var $firstStep = $('#passwordResetModal #passwordReset');
        var $btn = $('#passwordResetModal #btnSubmit');
        var $emailInput = $('#passwordResetModal #EmailForRecovery');
        var $successMessage = $('#passwordResetModal #forgotPassword_Success');
        var $failMessage = $('#passwordResetModal #forgotPassword_Fail');
        
        $successMessage.hide();
        $failMessage.hide();

        $btn.on('click', function () {
            $btn.button('loading');
            $.post($firstStep.attr('data-url'), { email: $emailInput.val() })
            .done(function (data) {
                if (data.Error !== undefined) {
                    $successMessage.hide();
                    $failMessage.text(data.Error);
                    $failMessage.show();
                    return;
                }
                $successMessage.show();
                $failMessage.hide();

                $btn.hide();
            })
            .always(function () {
                $btn.button('reset');
            });
        });
    });

})(jQuery);