<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ImageSpecification.ascx.vb"
    Inherits="BetterclassifiedsWeb.ImageSpecification" %>
<div id="modalImageContainer">
    <div class="header">
        Image Specifications</div>
    <div class="subHeader">
        Maximum Image Size</div>
    <div class="body">
        4mb is the maximum file size that iFlog will receive for image uploads</div>
    <div class="subHeader">
        Acceptable Image Types</div>
    <div class="body">
        .jpg .jpeg .gif .bmp .tiff .tif .png</div>
    <div class="subHeader">
        Print Advertisement Specs</div>
    <div class="body">
        <strong>Height: 30mm</strong>
        <br>
        <strong>Width: 28mm</strong>
        <br>
        <strong>Resolution (DPI): 300 DPI</strong>
        <br>
        <br>
        <em>Please note that images will be reformatted into this height, width and size regardless
            of the original image height, width and size. Please try and keep your images as
            close to these specifications as possible to ensure best results.</em>
        <br>
    </div>
    <div class="subHeader">
        Online Advertisement Specs</div>
    <div class="body">
        All images uploaded for online advertisements will maintain the original aspect
        ratio (i.e. the same height : width ratio) as the original uploaded image, but will
        be reduced to 72 DPI (the standard resolution for web sites).<br>
        <br>
        <em>To gain the best results from your uploaded images, try to keep the picture as close
            to a square or standard postcard aspect ratio. Standard postcard ratio is 1 : 1.5.</em>
    </div>
</div>
