
// Open dialog

function openRadWindow(url, pageName) {
    var oWnd = radopen(url, pageName);
    oWnd.center();
}


// Cancel dialog (take no action)

function cancelRadWindow() {
    var oWindow = getCurrentRadWindow();
    oWindow.argument = null;
    oWindow.close();
}


// Close dialog

function closeWindowWithSuccess() {
    var oWindow = getCurrentRadWindow();
    oWindow.close(-1);
}

function closeWindowWithSuccess(wdw) {
    var oWindow = getCurrentRadWindow(wdw);
    oWindow.close(-1);
}

// Telerik rad window management, should be called from this file only

function getCurrentRadWindow() {
    var oWindow = null;
    if (window.radWindow)
        oWindow = window.radWindow;
    else if (window.frameElement.radWindow)
        oWindow = window.frameElement.radWindow;
    return oWindow;
}
