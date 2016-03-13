(function (jQuery, ko, $paramount) {

	function UserNetwork(data) {
		var me = this;

		me.fullName = ko.observable();
		me.email = ko.observable();
		me.selected = ko.observable();
	    me.bindUserNetwork(data);

		/*
         * Validation
         */
		me.validator = ko.validatedObservable({
			fullName: me.fullName.extend({ required: true }),
			email: me.email.extend({ required: true, email: true })
		});
	}

	UserNetwork.prototype.bindUserNetwork = function (data) {
        if (_.isUndefined(data)) {
            return;
        }

        this.fullName(data.fullName);
        this.email(data.email);
	    this.selected(data.selected);
	}

	$paramount.models = $paramount.models || {};
    $paramount.models.UserNetwork = UserNetwork;

})(jQuery, ko, $paramount);