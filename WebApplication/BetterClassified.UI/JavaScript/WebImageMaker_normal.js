function submitPrintImage(sender, e) {
    var button = $telerik.$("[id$='btnUploadPrintImage']");
    var input = e.get_fileInputField();

    if (!sender.isExtensionValid(input.value)) {
        alert("One of the files submitted is not valid. Please review the accepted file types and size before submitting your image.");
    }
    else {
        button.click();
    }
}

function setViewportDimensions(hiddenFieldID)
{
    var field = document.getElementById(hiddenFieldID);
    var width;
    var height;
    
    // see http://evolt.org/node/30655
    
    if (window.innerWidth)
    {
	    width = window.innerWidth;
	    height = window.innerHeight;
    }
    else if (document.documentElement && document.documentElement.clientWidth)
    {
	    width = document.documentElement.clientWidth;
	    height = document.documentElement.clientHeight;
    }
    else if (document.body)
    {
	    width = document.body.clientWidth;
	    height = document.body.clientHeight;
    }

    field.value = width + "," + height;
}

var thumbnailDivs = {};

function registerThumbnailDiv(thumbnailDivId)
{
    thumbnailDivs[thumbnailDivId] = document.getElementById(thumbnailDivId);
    thumbnailDivs[thumbnailDivId].style.visibility = "hidden";
}

function hideThumbnailDivs()
{
    for(var divId in thumbnailDivs)
    {
        thumbnailDivs[divId].style.visibility = "hidden";
    }        
    document.onclick = null;
}

function showThumbnailDiv(thumbnailDivId)
{
    hideThumbnailDivs();
    thumbnailDivs[thumbnailDivId].style.visibility = "";
    window.setTimeout("document.onclick = hideThumbnailDivs", 300);
}   

