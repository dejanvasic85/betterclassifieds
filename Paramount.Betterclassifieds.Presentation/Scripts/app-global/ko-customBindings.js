/*
 * Extends knockout by adding extra bindings for elements
 */

(function (knockout, $) {


    /*
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
    };
    ko.validation.makeBindingHandlerValidatable('date');

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
                    country: "AU",
                    type: ['(regions)'],
                    map: $map,
                    markerOptions: {
                        draggable: false
                    }
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
    ko.validation.makeBindingHandlerValidatable('googleMap');

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

    /*
    *   Bounce in animation
    */
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
    ko.bindingHandlers.datetime = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            //initialize datepicker with some optional options

            var options = allBindingsAccessor().dateTimePickerOptions || {};
            options.format = $paramount.jsToDisplayDateFormat;
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

            if (picker) {
                var koDate = ko.utils.unwrapObservable(valueAccessor());

                //in case return from server datetime i am get in this form for example /Date(93989393)/ then fomat this
                //koDate = (typeof (koDate) !== 'object') ? new Date(parseFloat(koDate.replace(/[^0-9]/g, ''))) : koDate;

                koDate = $paramount.dateFromServer(koDate);

                picker.date(koDate);
            }
        }
    };

    /*
    *   Type Ahead - https://github.com/bassjobsen/Bootstrap-3-Typeahead
    */
    ko.bindingHandlers.typeahead = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            var $element = $(element);
            var allBindings = allBindingsAccessor();
            var source = ko.utils.unwrapObservable(valueAccessor());
            var items = ko.utils.unwrapObservable(allBindings.items) || 8;

            var valueChange = function (item) {
                if (allBindings.onItemSelected) {
                    allBindings.onItemSelected(item);
                }
                return item;
            };

            var options = {
                source: source,
                items: items,
                updater: valueChange
            };

            $element
                .attr('autocomplete', 'off')
                .typeahead(options);
        }
    };


    /*
    *   This is very important and it is what wires up all our validation 
    */
    ko.validation.makeBindingHandlerValidatable('datetime');


    /*
    *   Charts
    */

    ko.bindingHandlers.chart = {
        update: function (element, valueAccessor, allBindings) {

            var ctx = element.getContext("2d");
            var chartTypeValue = valueAccessor();
            var chartType = ko.unwrap(chartTypeValue);
            var chartData = ko.unwrap(allBindings.get('data'));

            var chart = new Chart(ctx, {
                type: chartType,
                data: chartData
            });
        }
    }


    /*
    *
    */
    var imageService = new $paramount.ImageService();

    ko.bindingHandlers.upload = {
        init: function (element, valueAccessor) {
            var $rootElement = $(element);
            var $uploadElement = $rootElement.find('input[type=file]');
            var $progressElement = $rootElement.find('.upload-progress');
            
            $paramount.upload({
                url: imageService.getUploadOnlineImageUrl(),
                element: $uploadElement,
                progressBar: $progressElement,
                complete: function (documentId) {
                    var value = valueAccessor();
                    value(documentId);
                },
                error: function (errorMsg) {
                    console.error('error', errorMsg);
                }
            });
        }
    }

})(ko, jQuery);