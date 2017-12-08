(function (ko, $p) {

    var imageService = new $p.ImageService();

    ko.components.register('image-viewer', {
        viewModel: function (params) {
            
            this.hasPhoto = ko.computed(function() {
                return $p.notNullOrUndefined(params.id());
            });

            this.imageUrl = ko.computed(function() {
                if ($p.notNullOrUndefined(params.id())) {
                    return imageService.getImageUrl(params.id());
                }
                return '';
            });

            this.remove = function () {
                if (params.onRemove) {
                    params.onRemove(params.id());
                }
            }
        },
        template: { path: $p.baseUrl + 'Scripts/app/imageViewer/imageViewer.html' }
    });


})(ko, $paramount);