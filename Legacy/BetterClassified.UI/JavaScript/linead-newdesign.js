var timount_number = 1;
var selectedHeader = null;
var selectedBorder = null;
var selectedBackground = null;

function changeHeader(sender, eventArgs) {
    selectedHeader = sender.get_selectedColor();
}

function loadHeader(sender, eventArgs) {
    selectedHeader = sender.get_selectedColor();
}

function changeBorder(sender, eventArgs) {
    selectedBorder = sender.get_selectedColor();
}

function loadBorder(sender, eventArgs) {
    selectedBorder = sender.get_selectedColor();
}

function changeBackground(sender, eventArgs) {
    selectedBackground = sender.get_selectedColor();
}

function loadBackground(sender, eventArgs) {
    selectedBackground = sender.get_selectedColor();
}

// Line Ad JQuery
$(document).ready(function () {
    $(".messagepanel-headercolour").hide();
    $(".messagepanel-bordercolour").hide();
    $(".messagepanel-backgroundcolour").hide();
    $('input[chkSuperHeader=aspCheckBox]')[0].disabled = !$('input[chkNormalHeader=aspCheckBox]')[0].checked;

    //
    // Normal Header Functions
    //
    $("input[chkNormalHeader=aspCheckBox]").click(function (args) {
        if (checkRequired != undefined) {
            checkRequired = true;
        }

        var normalHeaderSelected = false;
        if ($('input[chkNormalHeader=aspCheckBox]:checked').val() == "on") {
            var normalHeaderSelected = true;
        }
        else {
            $(".adheadertext").val('');
            SuperBoldHeaderClicked(false);
            $('input[chkSuperHeader=aspCheckBox]')[0].checked = normalHeaderSelected;
        }

        $('input[chkSuperHeader=aspCheckBox]')[0].disabled = !normalHeaderSelected;
        NormalHeaderClicked(normalHeaderSelected);
    });

    $(".adheadertext").keypress(function () {
        if (checkRequired != undefined) {
            checkRequired = true;
        }
        $('input[chkNormalHeader=aspCheckBox]')[0].checked = true;
        $('input[chkSuperHeader=aspCheckBox]')[0].disabled = false;
        NormalHeaderClicked(true);
    });

    //
    // Super Bold Header Functions
    //
    $("input[chkSuperHeader=aspCheckBox]").click(function (args) {
        if (checkRequired != undefined) {
            checkRequired = true;
        }

        var isSelected = false;
        if ($('input[chkSuperHeader=aspCheckBox]:checked').val() == "on") {
            var isSelected = true;
        }

        SuperBoldHeaderClicked(isSelected);
    });

    //
    // Colour Heading Functions
    //
    $("input[chkColourheader=aspCheckBox]").click(function (args) {
        if (checkRequired != undefined) {
            checkRequired = true;
        }

        var isColourHeader = $('input[chkColourheader=aspCheckBox]:checked').val() == "on";
        if ($('input[chkColourheader=aspCheckBox]:checked').val() == "on") {
            GetHeaderColourSuggestion();
        }
        else {
            selectedHeader = null;
        }
        ColourHeaderClicked(isColourHeader);
        $(".headerColourPanel").toggle();
    });

    if ($('input[chkColourheader=aspCheckBox]:checked').val() == "on") {
        $(".headerColourPanel").show();
    }
    else {
        $(".headerColourPanel").hide();
    }

    //
    // Colour Border Functions
    //
    $("input[chkColourBorder=aspCheckBox]").click(function (args) {
        if (checkRequired != undefined) {
            checkRequired = true;
        }
        var isSelected = $('input[chkColourBorder=aspCheckBox]:checked').val() == "on";
        if (isSelected == true) {
            GetBorderSuggestion();
        }
        else {
            selectedBorder = null;
        }
        ColourBorderClicked(isSelected);
        $(".borderColourPanel").toggle();
    });

    if ($('input[chkColourBorder=aspCheckBox]:checked').val() == "on") {
        $(".borderColourPanel").show();
    }
    else {
        $(".borderColourPanel").hide();
    }

    //
    // Colour Background Functions
    //
    $("input[chkColourBackground=aspCheckBox]").click(function (args) {
        if (checkRequired != undefined) {
            checkRequired = true;
        }
        var isSelected = $('input[chkColourBackground=aspCheckBox]:checked').val() == "on";

        if (isSelected) {
            GetBackgroundColourSuggestion();
        }
        else {
            selectedBackground = null;
        }
        ColourBackgroundClicked(isSelected);
        $(".backgroundColourPanel").toggle();
    });

    if ($('input[chkColourBackground=aspCheckBox]:checked').val() == "on") {
        $(".backgroundColourPanel").show();
    }
    else {
        $(".backgroundColourPanel").hide();
    }

    SubmitWordCount();
    $(".adtext").keyup(function (args) {
        window.clearTimeout(timount_number);
        timount_number = setTimeout(SubmitWordCount, 500);
    });
});

function NormalHeaderClicked(isSelected)
{
    var params = { isHeaderSelected: isSelected };
    var webServiceUrl = ajaxWebServiceUrl + "/LineAdBoldHeaderClicked";
    $.ajax({
        type: "POST",
        url: webServiceUrl,
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
        },
        error: function (event, request, settings) { }
    });
}

function SuperBoldHeaderClicked(isSelected) {
    var params = { isSuperHeaderSelected: isSelected };
    var webServiceUrl = ajaxWebServiceUrl + "/SuperBoldHeaderClicked";
    $.ajax({
        type: "POST",
        url: webServiceUrl,
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
        },
        error: function (event, request, settings) { }
    });
}

//
// Ajax Calls
//
function SubmitWordCount() {
    var params = { adText: $(".adtext").val() };
    var webServiceUrl = ajaxWebServiceUrl + "/GetAdWordCount";
    $.ajax({
        type: "POST",
        url: webServiceUrl,
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('.adword-count').text(msg.d);
        },
        error: function (event, request, settings) { }
    });

    if (checkRequired != undefined) {
        checkRequired = true;
    }
}

function GetBorderSuggestion() {
    if (selectedHeader != null || selectedBackground != null) {
        var params = { headerColour: selectedHeader, backgroundColour: selectedBackground };
        var method = "GetBorderColourSuggestion";
        var webServiceUrl = ajaxWebServiceUrl + "/" + method;
        $.ajax({
            type: "POST",
            url: webServiceUrl,
            data: JSON.stringify(params),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d.length > 0) {
                    var suggestion = msg.d.split("|");
                    $("div .messagepanel-bordercolour > .genericmessagepanel-msg").text(suggestion[0]);
                    $(".messagepanel-bordercolour").show()
                }
                else {
                    $(".messagepanel-bordercolour").hide();
                }
            },
            error: function (event, request, settings) {
                alert(event);
            }
        });
    }
    else {
        $(".messagepanel-bordercolour").hide();
    }
}

function ColourBorderClicked(isBorderSelected) {
    var params = { isSelected: isBorderSelected };
    var method = "LineAdBorderColourClicked";
    var webServiceUrl = ajaxWebServiceUrl + "/" + method;
    $.ajax({
        type: "POST",
        url: webServiceUrl,
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) { },
        error: function (event, request, settings) { }
    });
}

function GetHeaderColourSuggestion() {
    if (selectedBorder != null || selectedBackground != null) {
        var params = { borderColour: selectedBorder, backgroundColour: selectedBackground };
        var method = "GetHeaderColourSuggestion";
        var webServiceUrl = ajaxWebServiceUrl + "/" + method;
        $.ajax({
            type: "POST",
            url: webServiceUrl,
            data: JSON.stringify(params),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d.length > 0) {
                    var suggestion = msg.d.split("|");
                    $("div .messagepanel-headercolour > .genericmessagepanel-msg").text(suggestion[0]);
                    $(".messagepanel-headercolour").show();
                }
                else {
                    $(".messagepanel-headercolour").hide();
                }
            },
            error: function (event, request, settings) { }
        });
    }
    else {
        $(".messagepanel-headercolour").hide();
    }
}

function ColourHeaderClicked(isColourHeader) {
    var params = { isSelected: isColourHeader };
    var method = "LineAdHeaderColourClicked";
    var webServiceUrl = ajaxWebServiceUrl + "/" + method;
    $.ajax({
        type: "POST",
        url: webServiceUrl,
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) { },
        error: function (event, request, settings) { }
    });
}

function GetBackgroundColourSuggestion() {
    if (selectedHeader != null || selectedBorder != null) {
        var params = { headerColour : selectedHeader, borderColour : selectedBorder };
        var method = "GetBackgroundColourSuggestion";
        var webServiceUrl = ajaxWebServiceUrl + "/" + method;
        $.ajax({
            type: "POST",
            url: webServiceUrl,
            data: JSON.stringify(params),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d.length > 0){
                    var suggestion = msg.d.split("|");
                    $("div .messagepanel-backgroundcolour > .genericmessagepanel-msg").text(suggestion[0]);
                    $(".messagepanel-backgroundcolour").show();
                }
                else {
                    $(".messagepanel-backgroundcolour").hide();
                }
            },
            error: function (event, request, settings) { }
        });
    }
    else {
        $(".messagepanel-backgroundcolour").hide();
    }
}

function ColourBackgroundClicked(isBackgroundSelected) {
    var params = { isSelected: isBackgroundSelected };
    var method = "LineAdBackgroundColourClicked";
    var webServiceUrl = ajaxWebServiceUrl + "/" + method;
    $.ajax({
        type: "POST",
        url: webServiceUrl,
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) { },
        error: function (event, request, settings) { }
    });
}
