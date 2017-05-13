(function(ko, $p) {

    ko.components.register('progress-bar', {
        viewModel: function(params) {

            this.message = params.message;
            this.percentComplete = ko.computed(function() {
                
                // We expect that params contains observable values

                var perc = (params.processed() / params.howMany()) * 100;

                return perc + '%';

            });

        },
        template: { path: $p.baseUrl + '/Scripts/app/progress-bar/progress-bar.html' }
    });


})(ko, $paramount);