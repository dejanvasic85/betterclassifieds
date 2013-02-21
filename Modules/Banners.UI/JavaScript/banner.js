var bannerPanel;
var bannerParams;
var bannerImagePanel;

$(document).ready(function () {
    bannerPanel = $("div.banner-panel");
    bannerParams = $("div.banner-panel input:hidden[id$='bannerParamsHidden']");
    bannerType = JSON.parse($("div.banner-panel input:hidden[id$='bannerTypeHidden']").val());
    bannerImagePanel = $("div.banner-panel  div.image-panel");

    if (bannerType.IncludeTimer) {
        $(document).everyTime(1000, function (i) {
            Paramount_DisplayBanner(i);
        }, 0);
    }
    else {
        Paramount_DisplayBanner(i);
    }

});

function Paramount_DisplayBanner(i) {
    var params = { params :  bannerParams.val()};
    $.ajax({
        type: "POST",
        url: "Service/AjaxMethods.asmx/GetNextBanner",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            bannerImagePanel.html(msg.d);
            //var result = eval(msg.d);
            //bannerImagePanel.html('');
            $("<img>").attr(result.imagePath).append(bannerImagePanel);
        },
        error: function (event, request, settings) {
            alert(request);
        }
    });
}