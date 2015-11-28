var timount_number = 1;

// Line Ad JQuery
$(document).ready(function () {

    SubmitWordCount();
    $(".adBodyTextBox").keyup(function (args) {
        window.clearTimeout(timount_number);
        timount_number = setTimeout(SubmitWordCount, 500);
    });
});


//
// Ajax Calls
//
function SubmitWordCount() {
    var params = { adText: $(".adBodyTextBox").val() };
    var webServiceUrl = ajaxWebServiceUrl + "/GetLineAdWordCount";
    $.ajax({
        type: "POST",
        url: webServiceUrl,
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('.wordCountValue').text(msg.d);
        },
        error: function (event, request, settings) {
            alert('Network Issue: Unable to count words.');
        }
    });
}

