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

/******************
* Helper objects *
******************/

// returns an object that represents the position relative to
// popupOrigin that event e occurred.
function point(e) {
    if (e.pageX || e.pageY) {
        this.x = e.pageX;
        this.y = e.pageY;
    }
    else if (e.clientX || e.clientY) {
        // need to use both document.body and document.documentElement to cater for various
        // combinations of IE 5/6 in normal and quirksmode
        this.x = e.clientX + document.body.scrollLeft + document.documentElement.scrollLeft;
        this.y = e.clientY + document.body.scrollTop + document.documentElement.scrollTop;
    }
    // give the point position relative to the popup
    this.x -= popupOrigin.x;
    this.y -= popupOrigin.y;
}


// returns an object that represents the size and position of the element oBlock
function rectangle(oBlock) {
    return {
        x: parseInt(oBlock.style.left),
        y: parseInt(oBlock.style.top),
        w: parseInt(oBlock.style.width),
        h: parseInt(oBlock.style.height)
    };
}


// returns an object that represents the absolute position of element obj
function findPos(obj) {
    // return a "point" for ease of use...
    return { x: findPosX(obj), y: findPosY(obj) };
}


// returns an object that represents the top left point of the viewport
// with respect to the document as a whole
function findScrollOffset() {
    // from Quirksmode
    var _x, _y;
    if (self.pageYOffset) // all except Explorer
    {
        _x = self.pageXOffset;
        _y = self.pageYOffset;
    }
    else if (document.documentElement && document.documentElement.scrollTop)
    // Explorer 6 Strict
    {
        _x = document.documentElement.scrollLeft;
        _y = document.documentElement.scrollTop;
    }
    else if (document.body) // all other Explorers
    {
        _x = document.body.scrollLeft;
        _y = document.body.scrollTop;
    }

    // return a "point" for ease of use...
    return { x: _x, y: _y };
}

// from Quirksmode
function findPosX(obj) {
    var curleft = 0;
    if (obj.offsetParent) {
        while (obj.offsetParent) {
            curleft += obj.offsetLeft
            obj = obj.offsetParent;
        }
    }
    else if (obj.x)
        curleft += obj.x;
    return curleft;
}

function findPosY(obj) {
    var curtop = 0;
    if (obj.offsetParent) {
        while (obj.offsetParent) {
            curtop += obj.offsetTop
            obj = obj.offsetParent;
        }
    }
    else if (obj.y)
        curtop += obj.y;
    return curtop;
}



/******************
* Globals        *
******************/
var oPopup, oCanvas, oSelection, oTargetImage, oConfirmButton, oDebugInfo, oPopupContainer;
var aspectRatio = 0;
var bConstrain = false;
var bCanMove = false;
var bMoving = false;
var bResizing = false;
var iReqdWidth = -1;
var iReqdHeight = -1;
var downPoint = null;
var popupOrigin = null;
var originalRect = null;
var canvasRect = null;
var resizeXMode, resizeYMode;
var minimumSelectionSize = 40;
var bInMove = false;



function initialise(popupID, canvasID, selectionBoxID, reqdWidth, reqdHeight,
        targetImageID, confirmButtonID, popupContainerID, debugID) {
    oPopup = document.getElementById(popupID); // this is our container for the canvas, selection box and buttons
    oCanvas = document.getElementById(canvasID);
    oSelection = document.getElementById(selectionBoxID);
    oTargetImage = document.getElementById(targetImageID);
    oConfirmButton = document.getElementById(confirmButtonID);
    oDebugInfo = document.getElementById(debugID);
    oPopupContainer = document.getElementById(popupContainerID);

    // make the popup a little larger than the canvas, for prettiness
    oPopup.style.width = (oCanvas.width + 60) + "px";
    oPopup.style.height = (oCanvas.height + 70) + "px";

    // we want to put oPopup at the top of the viewport, with a small offset.
    // But we have no idea where our popup is in relation to other elements on the page.

    // where is it now, relative to the origin?
    var popPos = findPos(oPopup);

    // and where is the viewport, relative to the origin?
    var vpPos = findScrollOffset();

    // given those two locations we can now offset the popup div so it appears
    // at the right place in the browser window
    oPopup.style.top = (vpPos.y - popPos.y + 30) + "px";
    oPopup.style.left = (vpPos.x - popPos.x + 30) + "px";

    // now store the position of the popup div so that we can use it to determine event locations later:
    popupOrigin = findPos(oPopup);

    // and keep a record of the canvas dimensions:
    oPopupContainer.style.height = oCanvas.height + 40 + "px";
    oPopupContainer.style.width = oCanvas.width + 50 + "px";
    oCanvas.style.top = "30px";
    oCanvas.style.left = "15px";
    oCanvas.style.width = oCanvas.width + "px"; // need to force the img width and height attrs into CSS attrs
    oCanvas.style.height = oCanvas.height + "px";
    canvasRect = rectangle(oCanvas);

    // see if the selection should be constrained to a specific aspect ratio
    // (i.e., both width and height have been specified)
    if (!isNaN(parseInt(reqdWidth))) iReqdWidth = parseInt(reqdWidth);
    if (!isNaN(parseInt(reqdHeight))) iReqdHeight = parseInt(reqdHeight);

    if (iReqdWidth == -1 && iReqdHeight == -1) {
        alert("Fatal error - must specify at least one dimension");
        return;
    }

    if (iReqdWidth > 0 && iReqdHeight > 0) {
        bConstrain = true;
        aspectRatio = iReqdWidth / iReqdHeight;
    }

    // now initialise the selection at a sensible starting position    
    //    oSelection.style.left = (Math.floor(oCanvas.width / 5) + 15) + "px";
    //    oSelection.style.top = (Math.floor(oCanvas.height / 5) + 30) + "px";
    //    oSelection.style.width = (Math.floor(oCanvas.width / 5) * 3) + "px";
    //    oSelection.style.height = (Math.floor(oCanvas.height / 5) * 3) + "px";

    oSelection.style.left = (Math.floor(oCanvas.width / 10) + 15) + "px";
    oSelection.style.top = (Math.floor(oCanvas.height / 10) + 30) + "px";
    oSelection.style.width = (Math.floor(oCanvas.width / 5) * 4) + "px";
    oSelection.style.height = (Math.floor(oCanvas.height / 5) * 4) + "px";


    var selection = rectangle(oSelection);
    constrain(selection);
    setSelection(selection);

    // now hook up some event handling:
    document.onmousemove = move;
    document.onmouseup = up;
    document.onmousedown = down;
    oCanvas.ondrag = function () { return false; }
    oSelection.ondrag = function () { return false; }
    document.ondrag = function () { return false; }
}


// ensures that the supplied rectangle object rect is the correct aspect ratio
function constrain(rect) {
    if (bConstrain && rect) {
        var newRatio = rect.w / rect.h;
        if (newRatio > aspectRatio) {
            // it's too "landscapey" - keep the height the same but reduce the width 
            // in accordance with the required aspectRatio
            var correctWidth = Math.round(aspectRatio * rect.h);
            if (correctWidth >= minimumSelectionSize) {
                if (resizeXMode == "W") {
                    var rightPos = rect.x + rect.w;
                    rect.x = rightPos - correctWidth;
                }
                rect.w = correctWidth;
            }
            else {
                // the constrained selection will be too small
                rect.w = minimumSelectionSize;
                var newH = Math.round(minimumSelectionSize / aspectRatio);
                var dy = newH - rect.h;
                rect.h = newH;
                if (resizeYMode == "N") {
                    rect.y -= dy;
                }
            }
        }
        else {
            // it's too "portraity" - keep the width the same but reduce the height 
            // in accordance with the required aspectRatio
            var correctHeight = Math.round(rect.w / aspectRatio);
            if (correctHeight >= minimumSelectionSize) {
                if (resizeYMode == "N") {
                    var bottomPos = rect.y + rect.h;
                    rect.y = bottomPos - correctHeight;
                }
                rect.h = correctHeight;
            }
            else {
                // the constrained selection will be too small
                rect.h = minimumSelectionSize;
                var newW = Math.round(minimumSelectionSize * aspectRatio);
                var dx = newW - rect.w;
                rect.w = newW;
                if (resizeXMode == "W") {
                    rect.x -= dx;
                }
            }
        }
    }
}

/*
function confine(rect)
{
// leave this for a bit - the combination of confining and constraining still gives problems
    
// ensure the selection doesn't stray outside the borders of the canvas
window.status = "canvasRect: x:" + canvasRect.x + " y:" + canvasRect.y + " w:" + canvasRect.w + " h:" + canvasRect.h;
window.status += "   rect: x:" + rect.x + " y:" + rect.y + " w:" + rect.w + " h:" + rect.h;
    
if((rect.x) < canvasRect.x - 2) 
rect.x = canvasRect.x - 2;
if(rect.y < canvasRect.y - 2) 
rect.y = canvasRect.y - 2;
if((rect.x + rect.w) > (canvasRect.x + canvasRect.w)) 
rect.x -= (rect.x + rect.w) - (canvasRect.x + canvasRect.w);
if((rect.y + rect.h) > (canvasRect.y + canvasRect.h)) 
rect.y -= (rect.y + rect.h) - (canvasRect.y + canvasRect.h);
}
*/

function checkConfine(rect) {
    var msg = "";
    if ((rect.x) < canvasRect.x)
        msg += "Selection extends to the left of the canvas. ";
    if (rect.y < canvasRect.y)
        msg += "Selection extends above canvas. ";
    if ((rect.x + rect.w) > (canvasRect.x + canvasRect.w))
        msg += "Selection extends to the right of the canvas. ";
    if ((rect.y + rect.h) > (canvasRect.y + canvasRect.h))
        msg += "Selection extends below the canvas. ";

    if (msg) {
        oConfirmButton.disabled = true;
        oDebugInfo.innerHTML = "WARNING: " + msg;
    }
    else {
        oConfirmButton.disabled = false;
        oDebugInfo.innerHTML = "";
    }
}

// applies the size and position information in the rectangle rect to the selection div
function setSelection(rect) {
    oSelection.style.left = rect.x + "px";
    oSelection.style.top = rect.y + "px";
    oSelection.style.width = rect.w + "px";
    oSelection.style.height = rect.h + "px";
}

// event handler for mousemove event
function move(e) {
    if (bInMove) return; // we're already processing a mousemove event

    bInMove = true;
    oCanvas.style.cursor = "auto";
    oSelection.style.cursor = "auto";

    bCanMove = false;
    if (!e) var e = window.event;
    var p = new point(e);
    var selection = rectangle(oSelection);

    //debugDisplay(p, selection);

    if (bMoving) {
        var dx = p.x - downPoint.x;
        var dy = p.y - downPoint.y;
        selection.x = 0 + originalRect.x + dx;
        selection.y = 0 + originalRect.y + dy;
        //confine(selection);
        //constrain(selection);	
        setSelection(selection);
        checkConfine(selection);
    }
    else if (bResizing) {
        var dx = p.x - downPoint.x;
        var dy = p.y - downPoint.y;

        if (resizeXMode == "E") {
            selection.w = 0 + originalRect.w + dx;
            if (selection.w < minimumSelectionSize) {
                selection.w = minimumSelectionSize;
                bResizing = false;
            }
        }
        if (resizeXMode == "W") {
            selection.w = 0 + originalRect.w - dx;
            selection.x = 0 + originalRect.x + dx;
            if (selection.w < minimumSelectionSize) {
                dx = selection.w - minimumSelectionSize;
                selection.w = minimumSelectionSize;
                selection.x += dx;
                bResizing = false;
            }
        }
        if (resizeYMode == "S") {
            selection.h = 0 + originalRect.h + dy;
            if (selection.h < minimumSelectionSize) {
                selection.h = minimumSelectionSize;
                bResizing = false;
            }
        }
        if (resizeYMode == "N") {
            selection.h = 0 + originalRect.h - dy;
            selection.y = 0 + originalRect.y + dy;
            if (selection.h < minimumSelectionSize) {
                dy = selection.h - minimumSelectionSize;
                selection.h = minimumSelectionSize;
                selection.y += dy;
                bResizing = false;
            }
        }

        //confine(selection);			
        constrain(selection);
        setSelection(selection);
        checkConfine(selection);
    }
    else {
        resizeXMode = "";
        resizeYMode = "";

        var targetSize = 15;
        if (p.x >= selection.x && p.x <= (selection.x + selection.w) && p.y >= selection.y && p.y <= (selection.y + selection.h)) {
            // cursor is inside the selection
            // default to move behaviour
            oSelection.style.cursor = "move";
            oCanvas.style.cursor = "move";
            bCanMove = true;
            // only display N, E, W, S cursors for unconstrained selections - 
            // constrained selections should only get NW, NE, SW, SE cursors.
            if (p.x <= (selection.x + targetSize)) {
                oSelection.style.cursor = bConstrain ? "Auto" : "W-resize";
                oCanvas.style.cursor = bConstrain ? "Auto" : "W-resize";
                resizeXMode = "W";
                if (p.y <= (selection.y + targetSize)) {
                    oSelection.style.cursor = "NW-resize";
                    oCanvas.style.cursor = "NW-resize";
                    resizeYMode = "N";
                }
                else if (p.y >= (selection.y + (selection.h - targetSize))) {
                    oSelection.style.cursor = "SW-resize";
                    oCanvas.style.cursor = "SW-resize";
                    resizeYMode = "S";
                }
            }
            else if (p.x > (selection.x + targetSize) && p.x <= (selection.x + (selection.w - targetSize))) {
                if (p.y <= (selection.y + targetSize)) {
                    oSelection.style.cursor = bConstrain ? "Auto" : "N-resize";
                    oCanvas.style.cursor = bConstrain ? "Auto" : "N-resize";
                    resizeYMode = "N";
                }
                else if (p.y >= (selection.y + (selection.h - targetSize))) {
                    if (!bConstrain) {
                        oSelection.style.cursor = bConstrain ? "Auto" : "S-resize";
                        oCanvas.style.cursor = bConstrain ? "Auto" : "S-resize";
                    }
                    resizeYMode = "S";
                }
                else {
                    oSelection.style.cursor = "move";
                    oCanvas.style.cursor = "move";
                }
            }
            else if (p.x > (selection.x + (selection.w - targetSize))) {
                if (!bConstrain) {
                    oSelection.style.cursor = bConstrain ? "Auto" : "E-resize";
                    oCanvas.style.cursor = bConstrain ? "Auto" : "E-resize";
                }
                resizeXMode = "E";
                if (p.y <= (selection.y + targetSize)) {
                    oSelection.style.cursor = "NE-resize";
                    oCanvas.style.cursor = "NE-resize";
                    resizeYMode = "N";
                }
                else if (p.y >= (selection.y + (selection.h - targetSize))) {
                    oSelection.style.cursor = "SE-resize";
                    oCanvas.style.cursor = "SE-resize";
                    resizeYMode = "S";
                }
            }
        }
    }

    bInMove = false;
}

function down(e) {
    if (!e) var e = window.event;
    downPoint = new point(e);
    originalRect = rectangle(oSelection);

    if (bCanMove && resizeXMode == "" && resizeYMode == "") {
        bMoving = true;
    }
    if (resizeXMode != "" || resizeYMode != "") {
        bResizing = true;
    }
    return false;
}

function up() {
    bMoving = false;
    bResizing = false;
}



function debugDisplay(pp, ss) {
    oDebugInfo.innerHTML = "&nbsp;&nbsp;point:" + pp.x + "," + pp.y + " &nbsp;&nbsp;sel:" + ss.x + "," + ss.y + "," + ss.w + "," + ss.h + " bMoving:" + bMoving + " bResizing:" + bResizing + " resizeXMode:" + resizeXMode + " resizeYMode:" + resizeYMode;
}


// encode the size and position of the current selection in a form element
function storeSelectionInfo(hiddenFieldID) {
    var field = document.getElementById(hiddenFieldID);

    // get the selection dimensions relative to the canvas - x,y,w,h    
    var selection = rectangle(oSelection);
    field.value = (selection.x - canvasRect.x) + "," + (selection.y - canvasRect.y) + "," + selection.w + "," + selection.h;
}




