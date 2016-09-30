using System.Drawing.Imaging;
using System.IO;
using Paramount.Betterclassifieds.Business;
using ZXing;
using ZXing.Common;

namespace Paramount.Betterclassifieds.Presentation.Services
{
    public class BarcodeGenerator : IBarcodeGenerator
    {
        public byte[] CreateQr(string barcodeData, int height, int width, int margin = 0)
        {
            // Use the third party library here
            var barcodeWriter = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new EncodingOptions { Height = height, Width = width, Margin = margin }
            };

            using (var bitmap = barcodeWriter.Write(barcodeData))
            {
                using (var stream = new MemoryStream())
                {
                    bitmap.Save(stream, ImageFormat.Gif);
                    return stream.ToArray();
                }
            }
        }
    }
}