(function () {

    'use strict';

    function Pagination(param) {
        var me = this;
        me.pages = ko.computed(function () {
            var arr = ko.observableArray();
            for (var i = 0; i <= param.data().length; i++) {
                arr.push(new Page({
                    number: i + 1,
                    selectPage: function (pageNum) {
                        me.currentPage(pageNum);
                        param.changePage(pageNum);
                    }
                }));
            }
            return arr;
        });

        me.totalPages = ko.computed(function() {
            return Math.ceil(me.pages().length / param.pageSize);
        });

        me.start = ko.observable(0);
        //me.endInital = ko.computed(function () {
        //    var endInitial = me.totalPages();
        //    if (param.maxPagesToDisplay < me.totalPages()) {
        //        endInitial = param.maxPagesToDisplay;
        //    }
        //    return endInitial;
        //});
        me.end = ko.observable(param.maxPagesToDisplay);
        me.currentPage = ko.observable(1);
        me.showNextButton = ko.observable(me.totalPages() < param.maxPagesToDisplay);
        
        me.next = function () {
            if (me.end() === me.totalPages()) {
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
            return me.end() === me.totalPages();
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
            path: '/Scripts/app/pagination/pagination.html'
        }
    });
})(ko);