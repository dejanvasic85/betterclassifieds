var para = para || {};

(function (ko) {

    var url = window.location.pathname;

    para.BookingCart = {
        CategoryId: ko.observable(),
        SubCategoryId: ko.observable()
    };

})(ko);