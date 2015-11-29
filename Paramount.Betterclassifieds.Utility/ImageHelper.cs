using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Paramount
{
    public static class ImageHelper
    {
        public static byte[] Resize(byte[] imageToResize, int width, int height)
        {
            using (MemoryStream originalImageStream = new MemoryStream(imageToResize))
            {
                var image = Image.FromStream(originalImageStream);
                var resized = Resize(image, width, height);

                MemoryStream destinationStream = new MemoryStream();
                resized.Save(destinationStream, ImageFormat.Jpeg);
                return destinationStream.ToArray();
            }
        }

        public static Image Resize(this Image imgToResize, int width, int height, int resolution = 94)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercentW = (width / (float)sourceWidth);
            float nPercentH = (height / (float)sourceHeight);
            float nPercent = nPercentH < nPercentW ? nPercentH : nPercentW;

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

        public static Image Crop(this Image img, int x, int y, int width, int height)
        {
            var bitmap = new Bitmap(img);
            var cropped = bitmap.Clone(new Rectangle(x, y, width, height), bitmap.PixelFormat);
            return cropped;
        }
    }
}
