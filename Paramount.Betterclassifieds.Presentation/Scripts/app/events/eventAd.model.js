(function ($, $paramount, ko) {

    $paramount.models = $paramount.models || {};

    // Event ad details editing object used for knockout
    $paramount.models.EventAd = function (data, options) {

        var me = this,
            adService = options.adDesignService,
            imageService = options.imageService,
            MAX_TITLE_CHARS = 100;

        me.eventId = ko.observable(data.eventId);
        me.canEdit = ko.observable(data.canEdit);
        me.title = ko.observable(data.title);
        me.titleRemaining = ko.computed(function () {
            if (me.title() === null)
                return 100;
            return MAX_TITLE_CHARS - me.title().length;
        });
        me.description = ko.observable(data.description);
        me.eventPhoto = ko.observable(data.eventPhoto);
        me.eventPhotoUploadError = ko.observable(null);
        me.eventPhotoUrl = ko.computed(function () {
            return imageService.getImageUrl(me.eventPhoto(), { w: 1278, h: 502 });
        });
        me.removeEventPhoto = function () {
            if (me.eventPhoto() !== null) {
                adService.removeOnlineAdImage(me.eventPhoto()).done(function () {
                    me.eventPhoto(null);
                });
            }
            return;
        }
        me.configDurationDays = ko.observable(options.configDurationDays);
        me.adStartDate = ko.observable(data.adStartDate);
        me.venueName = ko.observable(data.venueName);
        me.location = ko.observable(data.location);
        me.locationLatitude = ko.observable(data.locationLatitude);
        me.locationLongitude = ko.observable(data.locationLongitude);
        me.locationFloorPlanDocumentId = ko.observable(data.locationFloorPlanDocumentId);
        me.locationFloorPlanFilename = ko.observable(data.locationFloorPlanFilename);
        me.eventStartDate = ko.observable(data.eventStartDate);
        me.eventEndDate = ko.observable(data.eventEndDate);
        me.organiserName = ko.observable(data.organiserName);
        me.organiserPhone = ko.observable(data.organiserPhone);
        me.ticketingEnabled = ko.observable(data.ticketingEnabled);

        /*
         * Address properties
         */
        me.streetNumber = ko.observable(data.streetNumber);
        me.streetName = ko.observable(data.streetName);
        me.suburb = ko.observable(data.suburb);
        me.state = ko.observable(data.state);
        me.postCode = ko.observable(data.postCode);
        me.country = ko.observable(data.country);


        /*
         * Validation
         */
        me.validator = ko.validatedObservable({
            title: me.title.extend({ required: true, maxLength: MAX_TITLE_CHARS }),
            description: me.description.extend({ required: true }),
            location: me.location.extend({ required: true, maxLength: 200 }),
            eventStartDate: me.eventStartDate.extend({ required: true }),
            eventEndDate: me.eventEndDate
                .extend({ required: true })
                .extend({ mustBeAfter: me.eventStartDate(), message: 'End date must be after start date.' }),
            organiserName: me.organiserName.extend({ required: true }),
            adStartDate: me.adStartDate.extend({ required: true })
        });

        /*
         * Submit changes
         */
        me.submitChanges = function (element, event) {

            var $btn = $(event.target);

            if (!$paramount.checkValidity(me)) {
                return Promise.resolve();
            }

            $btn.loadBtn();
            return adService.updateEventDetails(ko.toJS(me));
        }

        me.submitChangesAndNotify = function(element, event) {
            me.submitChanges(element, event).then(function(res) {
                if (!res.errors) {
                    toastr.success('Details updated successfully');
                    $(event.target).resetBtn();
                }
            });
        }

    };

})(jQuery, $paramount, ko);