/// <reference path="pathto/script.js"/>
function HideControl(controlId) {
    var control = document.getElementById(controlId);
    control.style.visibility = 'hidden';
}

function DisplayControl(controlId) {
    var control = document.getElementById(controlId);
    control.style.visibility = 'visible';
}

$(document).ready(function () {
    var x = -5;
    var y = -2;
    if ($(".webimagemaker-popup-title .help-context-image")[0] != null) {
        x = 108;
        y = -22;
    }

    $(".help-context-image").tooltip({ offset: [x, y] });

});