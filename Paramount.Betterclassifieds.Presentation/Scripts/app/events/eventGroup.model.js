(function ($, ko, $p) {

    function EventGroup(data) {
        var me = this;
        me.eventGroupId = ko.observable();
        me.eventId = ko.observable();
        me.groupName = ko.observable();
        me.maxGuests = ko.observable();
        me.guestCount = ko.observable();


        if (data) {
            me.bind(data);
        }

        me.maxGuestsText = ko.computed(function () {
            return me.maxGuests() === null ? "Unlimited" : me.maxGuests();
        });

        me.groupNameWithCount = ko.computed(function() {
            if (!me.maxGuests()) {
                return me.groupName();
            }

            return me.groupName() + " (" + me.guestCount() + " / " + me.maxGuests() + ")";
        });

    }

    EventGroup.prototype.bind = function (data) {
        this.eventGroupId(data.eventGroupId);
        this.eventId(data.eventId);
        this.groupName(data.groupName);
        this.maxGuests(data.maxGuests);
        this.guestCount(data.guestCount);
    }
    $p.models.EventGroup = EventGroup;
})(jQuery, ko, $paramount)