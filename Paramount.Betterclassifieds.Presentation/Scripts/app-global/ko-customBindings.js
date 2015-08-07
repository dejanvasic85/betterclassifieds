/*
 * Extends knockout by adding extra bindings for elements
 */

(function (knockout, $) {

    /*
     * Time - Clock picker
     */
    ko.bindingHandlers.time = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            var accessor = valueAccessor();
            var time = ko.unwrap(accessor);
            $(element)
                .val(time)
                .attr('readonly', '')               // Prevents the user from inputting their own value
                .addClass('bs-clock-picker')        // Prevents the user from inputting their own value
                .clockpicker({
                    donetext: 'OK',
                    autoclose: true,
                    afterDone: function () {
                        accessor($(element).val());
                    }
                });
        }
    }


    /*
     * Date Picker
     * Usage : <input type='text' data-bind='date: modelProperty' />
     */
    ko.bindingHandlers.date = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            var value = valueAccessor();
            var date = ko.unwrap(value);
            $(element)
                .attr('data-provide', "datepicker")
                .attr('readonly', "") // Prevents the user from inputting their own value
                .addClass('bs-date-picker') // Prevents the user from inputting their own value
                .val(date)
                .datepicker({
                    autoclose: true,
                    format: 'dd/mm/yyyy',
                    todateBtn: true,
                    todayHighlight: true,
                    startDate: new Date(),
                    orientation: 'bottom'
                })
                .on('changeDate', function () {
                    var changedDate = $(element).val();
                    value(changedDate);
                });
        }
    }

    /*
     * Google Map
     * Usage : <input type='text' data-bind="googleMap: modelProperty, mapElement : '#LocationMap'" />
     */
    ko.bindingHandlers.googleMap = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            var existingValue = valueAccessor();
            var address = ko.unwrap(existingValue);

            var $map = $(allBindings.get('mapElement'));

            if ($map.length === 0) {
                throw "googleMap binding requires a map binding";
            }

            var googleMap = $(element).val(address)
                .geocomplete({
                    map: $map,
                    markerOptions: {
                        draggable: false
                    },
                })
                .bind('geocode:result', function (event, geoData) {
                    viewModel.location(geoData["formatted_address"]);
                    viewModel.locationLatitude(geoData.geometry.location.lat());
                    viewModel.locationLongitude(geoData.geometry.location.lng());
                });

            // Bind the current address if any
            if (address) {
                googleMap.geocomplete('find', address);
            }
        }
    }

    /*
     * Toggle button
     */
    ko.bindingHandlers.toggle = {
        init: function (element, valueAccessor) {
            var $element = $(element),
                observable = valueAccessor();
            
            // Set the current value
            if (observable() === true) {
                $element.attr('checked', 'checked');
            }

            $element.bootstrapToggle().on('change', function (e) {
                observable($element.prop('checked'));
            });
        }
    }
    
})(ko, jQuery);