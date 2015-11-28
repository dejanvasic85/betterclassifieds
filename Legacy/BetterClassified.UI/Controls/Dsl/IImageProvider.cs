using System;
using System.Drawing;
using System.Web;
using Telerik.Web.UI;

namespace BetterClassified.UI.Dsl
{
    public interface IImageProvider
    {
        PurgeMethod PurgeStrategy { get; set; }
        string WorkingDirectory { get; set; }
        string ServerID { get; set; }

        int ThumbnailSize { get; set; }

        // returns true if the file is an image that the system can deal with
        // save the uploaded raw image in the <WorkingDirectory>/raw directory
        bool SaveRaw(UploadedFile uploadedFile, out String serverID, out int rawWidth, out int rawHeight, out string thumbnailFileName);

        // save a scaled copy of the raw image in the <WorkingDirectory>/canvas directory
        string CreateCanvas(WebImageFormat format, WebImageQuality quality, int canvasWidth, int canvasHeight);

        // crop the area indicated by transformedSelection from the raw image,
        // then scale it to reqdWidth x reqdHeight,
        // and save it in the <WorkingDirectory>/web
        // directory with the format and quality specified.
        string CropAndScale(Rectangle transformedSelection, WebImageFormat format, WebImageQuality quality, int reqdWidth, int reqdHeight);
        string CropAndScale(Rectangle transformedSelection, WebImageFormat format, WebImageQuality quality, int reqdWidth, int reqdHeight, int reqResolution, out string filePath);

        // if the raw image was the right dimensions, then bypass the canvas stage and
        // use it directly. It still might need format changing or quality alterations.
        // If the quality is high and the format is already the right one, then the 
        // implementation of this should just copy the raw file to the 
        // <WorkingDirectory>/web directory.
        string SaveRawImageAsWebImage(WebImageFormat format, WebImageQuality quality);
        string SaveRawImageAsWebImage(WebImageFormat format, WebImageQuality quality, int reqResolution, out string fullPath);

        // The user has selected a previously generated thumbnail, so work out what
        // raw file should be used. 
        bool UseThumbnailFile(string thumbnailFileName, out String serverID, out int rawWidth, out int rawHeight);
    }
}
