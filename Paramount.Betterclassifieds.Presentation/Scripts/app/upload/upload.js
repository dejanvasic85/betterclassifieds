(function (ko, $p) {

    var imageService = new $p.ImageService();

    ko.components.register('upload', {
        viewModel: function (params) {

            this.id = params.id;

        },
        template: { path: $p.baseUrl + 'Scripts/app/upload/upload.html' }
    });


})(ko, $paramount);