/*
** Support for styling the bootstrap elements using jquery unobtrusive validation library
** See http://stackoverflow.com/questions/11468130/twitter-bootstrap-integration-to-asp-net-mvc-validation/14651140#14651140
*/


(function ($) {
    $.validator.setDefaults({
        highlight: function (element, errorClass, validClass) {
            if (element.type === 'radio') {
                this.findByName(element.name).addClass(errorClass).removeClass(validClass);
            } else {
                $(element).addClass(errorClass).removeClass(validClass);
                $(element).closest('.form-group, .form-group-lg').removeClass('has-success').addClass('has-error');
            }
        },
        unhighlight: function (element, errorClass, validClass) {
            if (element.type === 'radio') {
                this.findByName(element.name).removeClass(errorClass).addClass(validClass);
            } else {
                $(element).removeClass(errorClass).addClass(validClass);
                $(element).closest('.form-group, .form-group-lg').removeClass('has-error').addClass('has-success');
            }
        }
    });

    // Wire up the on ready
    $(function () {
        $("span.field-validation-valid, span.field-validation-error").addClass('help-block');
        $("div.form-group").has("span.field-validation-error").addClass('has-error');
        $("div.validation-summary-errors").has("li:visible").addClass("alert alert-block alert-danger");
    });

}(jQuery));