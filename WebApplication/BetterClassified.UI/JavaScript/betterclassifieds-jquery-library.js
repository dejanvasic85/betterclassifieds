$(document).ready(function()
{
    //make all external liks to open in a new window
    $('a[href^="http://"]').attr("target", "_blank");
});

function OnRadWindowLoad(sender)
{
    $('iframe').contents().find("a").attr("target", "_blank");
}