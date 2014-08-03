//var checkRequired = false;

$(function () {
    $(".adordersummary-container").everyTime("2s", function (i) {
        if (checkRequired) {
            getPrices();
            checkRequired = false;
        }
    });
});

function getPrices() {
    var webServiceUrl = ajaxWebServiceUrl + "/GetPriceSummary";
    $.ajax({
        type: "POST",
        url: webServiceUrl,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            // Do something with the data here
            $(".adpricesummary-main").html(msg.d);
        },
        error: function (event, request, settings) {
            // do nothing for now
            alert('an error occurred');
            console.log('An error occurred when getting the price summary');
        }
    });
}