﻿/*
 * Extends knockout by adding extra bindings for elements
 */

(function (knockout, $) {

    ko.bindingHandlers.time = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            var time = ko.unwrap(valueAccessor());
            $(element)
                .val(time)
                .on('focus', function(event) {
                    event.preventDefault();
                })
                .clockpicker({
                donetext: 'OK',
                autoclose: true
            });
        }
    }

    /*
     * Usage : <input type='text' />
     */
    ko.bindingHandlers.date = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            var date = ko.unwrap(valueAccessor());
            $(element)
                .attr('data-provide', "datepicker")
                .on('focus', function (event) {
                    event.preventDefault();
                })
                .val(date)
                .datepicker({
                    autoclose: true,
                    format: 'dd/mm/yyyy',
                    todateBtn: true,
                    todayHighlight: true,
                    startDate: new Date(),
                    orientation: 'bottom'
                });
        }
    }

})(ko, jQuery);