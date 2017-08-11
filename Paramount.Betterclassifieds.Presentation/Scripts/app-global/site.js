/*
** $paramount utility class
*/
(function ($paramount, $window, $, $mobileDetect, htmlEditor) {

    var me = this;
    me.isMobileDevice = null;

    $paramount.jsToServerDateFormat = "YYYY-MM-DD HH:mm";   // How we talk to the server
    $paramount.jsToDisplayDateFormat = "DD/MM/YYYY HH:mm";  // How we display in browser

    $paramount.dateToServerString = function (date) {
        ///<summary>Returns a string ready to be converted to the .Net parsable on the server</summary>
        if (!date) {
            return null;
        }
        return moment(date).format($paramount.jsToServerDateFormat);
    }

    $paramount.dateFromServer = function (date) {
        ///<summary>Returns a moment date object after being parsed from the server</summary>
        if (!date) {
            return null;
        }

        return moment(date, $paramount.jsToServerDateFormat);
    }

    $paramount.dateToDisplay = function (date) {
        if (!date) {
            return '';
        }

        return moment(date).format($paramount.jsToDisplayDateFormat);
    }

    /*
     * Constants
     */
    $paramount.PAYMENT = {
        PAYPAL: 'PayPal',
        DIRECT_DEBIT: 'DirectDebit'
    }

    $paramount.EVENT_PAYMENT_STATUS = {
        CLOSE_EVENT_FIRST: 'Close Event First',
        REQUEST_PENDING: 'Request Pending',
        NOT_AVAILABLE: 'Not Available'
    }

    $paramount.CATEGORY_AD_TYPE = {
        EVENT: 'Event'
    }

    // Lazy loading for mobile checking
    me.evaluateMobile = function () {
        if (isMobileDevice !== null) {
            return isMobileDevice;
        }
        var detector = new $mobileDetect($window.navigator.userAgent);
        me.isMobileDevice = detector.mobile() !== null;
        return me.isMobileDevice;
    };

    /*
     * Utility function to guard against undefined parameters
     */
    $paramount.guard = function (val, name) {
        if (_.isUndefined(val)) {
            throw new Error(name + ' must be defined');
        }
    };

    $paramount.formatCurrency = function (value) {
        if (value === undefined || value === null)
            return '';

        if (typeof value === "string") {
            value = Number.parseFloat(value);
        }

        return "$" + value.toFixed(2);
    };

    $paramount.isMobile = function () {
        return evaluateMobile();
    };

    $paramount.setOnlineEditor = function (element) {
        if (me.evaluateMobile()) {
            return null;
        }

        $paramount.onlineEditor = htmlEditor.replace(element);

        return $paramount.onlineEditor;
    };

    $paramount.delay = (function () {
        var timer = 0;
        return function (callback, ms) {
            clearTimeout(timer);
            timer = setTimeout(callback, ms);
        };
    })();

    /*
     * File upload control
     */
    $paramount.upload = function (options) {

        $paramount.guard(options, 'options');
        $paramount.guard(options.url, 'url');
        $paramount.guard(options.element, '');

        options.progressBar = options.progressBar || $('<div><span class=".progress-bar"></span></div>'); // Todo - popup a real progress bar somewhere
        options.progressBar.hide();
        options.complete = options.complete || function () { };
        options.completeWithDetails = options.completeWithDetails || function () { };
        options.start = options.start || function () { };
        options.error = options.error || function () { };

        var upload = options.element.fileupload({
            dataType: 'json',
            url: options.url,
            autoUpload: true,
            singleFileUploads: false
        });

        upload.on('fileuploaddone', function (e, data) {
            if (data.result.documentId) {
                options.complete(data.result.documentId);
                options.completeWithDetails(data.result);
            } else {
                options.error(data.result.errorMsg);
            }
        });

        upload.on('fileuploadstart', function (data) {
            // Start showing the progress bar
            options.progressBar.show();
            options.start(data);
        });

        upload.on('fileuploadalways', function () {
            // Reset the width of the progress bar
            options.progressBar.hide();
            options.progressBar.find('.progress-bar').width('0');
        });

        upload.on('fileuploadprogressall', function (e, data) {
            var progress = parseInt(data.loaded / data.total * 100, 10);
            options.progressBar.find('.progress-bar').css('width', progress + '%');
        });

        upload.on('fileuploadadd', function (e, data) {
            if (data.files.length > 1) {
                options.error("Please select one image at a time.");
                return false;
            }
            return true;
        });
    };

    /*
     * Puts focus on the first element with an error
     */
    $paramount.goToFirstError = function () {
        $('.has-error input').first().focus();
    }

    /*
     * Checks the knockout object validator property
     */
    $paramount.checkValidity = function () {
        var result = true;
        for (var i = 0; i < arguments.length; i++) {
            var obj = arguments[i];

            if (obj.constructor === Array) {
                for (var j = 0; j < obj.length; j++) {
                    // Recursive call
                    result = $paramount.checkValidity(obj[j]);
                }

            } else {

                // Loop through each property of the object
                for (var p in obj) {
                    if (p === 'validator') {
                        // Execute it
                        if (obj[p].isValid() === false) {
                            result = false;
                            obj[p].errors.showAllMessages();
                        }
                    } else if (isObservableArray(obj[p])) {
                        ko.utils.arrayForEach(obj[p](), function (i) {
                            result = $paramount.checkValidity(i);
                        });
                    }
                }
            }
        };

        if (result === false) {
            // Show all messages (from knockout validation library) would trigger the error msgs to fire
            // So make the call to focus on the first error
            $paramount.goToFirstError();
        }

        return result;
    }

    function isObservableArray(obj) {
        return ko.isObservable(obj) && !(obj.destroyAll === undefined);
    }

    $paramount.notNullOrUndefined = function (arg) {
        if (typeof arg === 'undefined') {
            return false;
        }
        if (arg === null) {
            return false;
        }
        return true;
    }

    $paramount.isNullOrUndefined = function (arg) {
        return $paramount.notNullOrUndefined(arg) === false;
    }

    $paramount.ko = {
        bindArray: function (collection, creator) {
            /// <summary>Creates a new knockout observableArray where each item is created by the creator callback</summary>
            var observableArray = ko.observableArray();
            _.forEach(collection, function (item) {
                observableArray.push(creator(item));
            });
            return observableArray;
        }
    }

    $paramount.encodeQuery = function (obj) {
        var str = [];
        for (var p in obj)
            if (obj.hasOwnProperty(p)) {
                str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
            }
        return str.join("&");
    }

    $paramount.processPromises = function (funcsWithPromises, cb) {
        /// <summary>Will process each promise one by one which is very useufl in http calls to not flood the server.</summary>  
        /// <param name="funcsWithPromises" type="Array">The collection of methods when executed will return a Promise.</param>  
        /// <param name="cb" type="function">Callback function that is executed whenever each promise is returned and will contain the result as the first parameter.</param>  
        /// <returns type="Number">Promise</returns>  

        $paramount.guard(funcsWithPromises);
        $paramount.guard(cb);

        if (!_.isArray(funcsWithPromises)) {
            throw new Error('funcs must be an array');
        }

        return new Promise(function (resolve) {

            var processCount = 0;
            next(); // start the recurse

            function next() {
                if (processCount >= funcsWithPromises.length) {
                    return resolve('done'); // Finished
                }

                return funcsWithPromises[processCount]().then(function (result) {
                    cb(result);
                    processCount++;
                    next();
                });
            }

        });
    }


    return me;

})($paramount, window, jQuery, MobileDetect, CKEDITOR);


