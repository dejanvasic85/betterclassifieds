 (function (ko, $p) {

    $p.ListingService = function (baseUrl) {
        var me = this;
        me.baseUrl = baseUrl;

        me.search = function (query) {
            var url = this.baseUrl + 'api/listings/search';
            
            if (!query) {
                return $paramount.httpGet(url);
            }

            var queryEncoded = $paramount.encodeQuery(query);
            url += '?' + queryEncoded;

            return $paramount.httpGet(url);
        }
    }


    var listingService = new $p.ListingService($p.baseUrl);

    $p.models.Ads = function (params) {
        var me = this;
        me.user = params.user;  
        me.ads = ko.observableArray();

        var query = new $p.QueryManager()
            .withPageSize(params.pageSize)
            .withUser(params.user)
            .build();

        listingService.search(query).then(function (response) {
            if (response.errors) {
                return;
            }
            
            if (!Array.isArray(response)) {
                throw new Error("The response does not contain an array of events.");
            }

            _.each(response, function (item) {
                me.ads.push(item);
            });
        });
    };

    ko.components.register('ad-list', {
        viewModel: $p.models.Ads,
        template: { path: $p.baseUrl + 'Scripts/app/listings/ads.html' }
    });
    

})(ko, $paramount);