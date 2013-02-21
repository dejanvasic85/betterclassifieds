function submitFile(sender, e) {
    var button = $telerik.$("[id$='btnSubmitFiles']");
    var input = e.get_fileInputField();
    
    if (!sender.isExtensionValid(input.value)) {
        alert("One of the files submitted is not valid. Please review the accepted file types and size before submitting your image.");
    }
    else {
        button.click();
    }
}