﻿/*
 * Extends knockout by adding extra bindings for elements
 */

(function (knockout, $) {


    /*
     * Date Picker DEPRECATED. TODO - replace this with datepicker (see below)
     * Usage : <input type='text' data-bind='date: modelProperty' />
     */
    ko.bindingHandlers.date = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            var value = valueAccessor();
            var date = ko.unwrap(value);

            $(element)
                .attr('data-provide', "datepicker")
                .attr('readonly', "") // Prevents the user from inputting their own value
                .attr('placeholder', 'Click here to show calendar ...')
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
        },
        update: function (element, valueAccessor) {
            var newValue = ko.unwrap(valueAccessor());
            if (newValue === null) {
                $(element).val(''); // Force to clear the textbox
            }
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

            function assignMapValueToObservable(componentType, observable, propertyName, value) {
                if (_.isUndefined(observable)) {
                    return false;
                }
                if (componentType === propertyName) {
                    observable(value);
                }
                return true;
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

                    // Capture every map component
                    _.each(geoData.address_components, function (comp) {
                        _.each(comp.types, function (t) {
                            assignMapValueToObservable(t, viewModel.streetNumber, 'street_number', comp.long_name);
                            assignMapValueToObservable(t, viewModel.streetName, 'route', comp.long_name);
                            assignMapValueToObservable(t, viewModel.suburb, 'locality', comp.long_name);
                            assignMapValueToObservable(t, viewModel.state, 'administrative_area_level_1', comp.long_name);
                            assignMapValueToObservable(t, viewModel.postCode, 'postal_code', comp.long_name);
                            assignMapValueToObservable(t, viewModel.country, 'country', comp.long_name);
                        });
                    });

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

    ko.bindingHandlers.bounceInDown = {
        init: function (element, valueAccessor) {
            var value = valueAccessor();
            $(element).toggle(ko.unwrap(value));
            $(element).addClass('animated');
        },
        update: function (element, valueAccessor) {
            var value = valueAccessor();
            var isVisible = ko.unwrap(value);
            $(element).toggleClass('bounceInDown', isVisible);
            $(element).toggle(isVisible);
        }
    }


    /*
     * New date selector (with time)
     */
    var dateFormat = 'DD/MM/YYYY HH:mm';
    ko.bindingHandlers.datetime = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            //initialize datepicker with some optional options
            var options = allBindingsAccessor().dateTimePickerOptions || {};
            options.format = dateFormat;
            $(element).datetimepicker(options);

            //when a user changes the date, update the view model
            ko.utils.registerEventHandler(element, "dp.change", function (event) {
                var value = valueAccessor();
                if (ko.isObservable(value)) {
                    if (event.date != null && !(event.date instanceof Date)) {
                        value(event.date.toDate());
                    } else {
                        value(event.date);
                    }
                }
            });

            ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                var picker = $(element).data("DateTimePicker");
                if (picker) {
                    picker.destroy();
                }
            });
        },
        update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {

            var picker = $(element).data("DateTimePicker");
            //when the view model is updated, update the widget
            if (picker) {
                var koDate = ko.utils.unwrapObservable(valueAccessor());

                //in case return from server datetime i am get in this form for example /Date(93989393)/ then fomat this
                koDate = (typeof (koDate) !== 'object') ? new Date(parseFloat(koDate.replace(/[^0-9]/g, ''))) : koDate;

                picker.date(koDate);
            }
        }
    };

})(ko, jQuery);