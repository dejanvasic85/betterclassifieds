(function () {
    
    function Pagination(param) {

        var me = this;
        me.currentPage = ko.observable(1);
        me.pages = ko.observableArray();
        me.totalPageCount = ko.observable();
        me.start = ko.observable(0);
        me.end = ko.observable(param.maxPagesToDisplay);

        param.dataPromise.then(function (items) {
            me.totalPageCount(Math.ceil(items.length / param.pageSize));
            if (me.totalPageCount() < param.maxPagesToDisplay) {
                me.end(me.totalPageCount());
            }

            for (var i = 1; i <= me.totalPageCount() ; i++) {
                me.pages.push(new Page({
                    number: i,
                    selectPage: function (pageNum) {
                        me.currentPage(pageNum);
                        param.changePage(pageNum);
                    }
                }));
            }
        });

        me.next = function () {
            if (me.end() === me.totalPageCount()) {
                return;
            }
            me.start(me.start() + 1);
            me.end(me.end() + 1);
        }

        me.prev = function () {
            if (me.start() === 0) {
                return;
            }
            me.start(me.start() - 1);
            me.end(me.end() - 1);
        }

        me.hidePrev = ko.computed(function () {
            return me.start() <= 0;
        });

        me.hideNext = ko.computed(function () {
            return me.end() === me.totalPageCount();
        });

        me.pagesFiltered = ko.computed(function () {
            // Slice method is 0-index based
            var pages = _.slice(me.pages(), me.start(), me.end());
            return pages;
        });
    }

    function Page(data) {
        this.number = ko.observable(data.number);
        this.selectPage = function (model) {
            if (data.selectPage) {
                data.selectPage(model.number());
            }
        }
    }

    ko.components.register('pager', {
        viewModel: Pagination,
        template: {
            path: $paramount.baseUrl + '/Scripts/app/pagination/pagination.html'
        }
    });
})(ko);