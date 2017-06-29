/*
 * ViewEventAd.cshtml - ui hooks
 */
(function ($, ko, $paramount) {

    $paramount.ui = $paramount.ui || {};
    $paramount.ui.eventView = {
        init: function (options) {
            $(function () {
                var ticketingInterface = document.getElementById('ticketing');
                if (ticketingInterface) {
                    ko.applyBindings({}, ticketingInterface);
                }

                var guestsDialogInterface = document.getElementById('guestsDialog');
                if (guestsDialogInterface) {
                    ko.applyBindings({}, guestsDialogInterface);
                }

                var ticketSeatingInterface = document.getElementById('ticketSeating');
                if (ticketSeatingInterface) {
                    ko.applyBindings({}, ticketSeatingInterface);
                }

                var contactAdvertiserInterface = document.getElementById('contactAdvertiserForm');
                if (contactAdvertiserInterface) {
                    ko.applyBindings({}, contactAdvertiserInterface);
                }

                if (options.floorPlanDocumentId === '') {
                    $('.floor-plan').hide();
                } else if (options.floorPlanFileName.endsWith('.pdf')) {
                    var url = $paramount.baseUrl + 'Document/File/' + options.floorPlanDocumentId;
                    $('#btnViewFloorplan')
                        .removeAttr('data-target')
                        .removeAttr('data-toggle')
                        .attr('target', '_blank')
                        .attr('href', url);
                }

                $('.ticket-booth').affix({
                    offset: {
                        top: 300
                    }
                });
            });
        }
    }

})(jQuery, ko, $paramount)