using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace Paramount.Utility
{
    public static class ImageHelper
    {
        public static Image ResizeFixedSize(Image imgToResize, Size size)
        {
            int destWidth = imgToResize.Width;
            int destHeight = imgToResize.Height;

            // only resize if the original is bigger (do not blow it up)
            if (imgToResize.Width > size.Width || imgToResize.Height > size.Height)
            {
                float nPercent = 0;
                float nPercentW = 0;
                float nPercentH = 0;

                nPercentW = ((float)size.Width / (float)destWidth);
                nPercentH = ((float)size.Height / (float)destHeight);

                if (nPercentH < nPercentW)
                    nPercent = nPercentH;
                else
                    nPercent = nPercentW;

                destWidth = (int)(destWidth * nPercent);
                destHeight = (int)(destHeight * nPercent);
            }
            Bitmap b = new Bitmap(destWidth, destHeight);
            b.SetResolution(96, 96);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;
        }

        public static Image ResizeFixedSize(Image imgToResize, int width, int height, int resolution)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)width / (float)sourceWidth);
            nPercentH = ((float)height / (float)sourceHeight);
            nPercent = nPercentH < nPercentW ? nPercentH : nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(resolution, resolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.Red);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grPhoto.SmoothingMode = SmoothingMode.HighQuality;
            grPhoto.PixelOffsetMode = PixelOffsetMode.HighQuality;
            grPhoto.CompositingQuality = CompositingQuality.HighQuality;
            grPhoto.DrawImage(imgToResize, 0, 0, destWidth, destHeight);

            grPhoto.Dispose();
            return bmPhoto;
        }

        /// <summary>
        /// Converts the image to resize to the required width and height values and retaining high quality.
        /// </summary>
        /// <param name="imgToResize">The Image object that needs to be analyzed and converted.</param>
        /// <param name="width">New Width for the image.</param>
        /// <param name="height">New Height for the image.</param>
        /// <param name="resolution">Required Resolution for the image.</param>
        /// <param name="isLeaveOriginal">Flag to indicate whether to leave original dimensions if the image will get blown up from original.</param>
        /// <returns></returns>
        public static Image ResizeFixedSize(Image imgToResize, int width, int height, int resolution, bool isLeaveOriginal)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)width / (float)sourceWidth);
            nPercentH = ((float)height / (float)sourceHeight);
            nPercent = nPercentH < nPercentW ? nPercentH : nPercentW;

            // check the flag whether to blow up the image or leave the same size
            int destWidth = isLeaveOriginal && nPercent > 1 ? sourceWidth : (int)(sourceWidth * nPercent);
            int destHeight = isLeaveOriginal && nPercent > 1 ? sourceHeight : (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(resolution, resolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.Red);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grPhoto.SmoothingMode = SmoothingMode.HighQuality;
            grPhoto.PixelOffsetMode = PixelOffsetMode.HighQuality;
            grPhoto.CompositingQuality = CompositingQuality.HighQuality;
            grPhoto.DrawImage(imgToResize, 0, 0, destWidth, destHeight);

            grPhoto.Dispose();
            return bmPhoto;
        }

        public static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }
    }
}
