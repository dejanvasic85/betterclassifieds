using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using ImageProcessor;
using ImageProcessor.Imaging;
using Paramount.Betterclassifieds.Business;

namespace Paramount.Betterclassifieds.Presentation.Framework
{
    public class ImageResult : FilePathResult
    {
        private readonly string _filePath;
        private readonly int _width;
        private readonly int _height;

        public ImageResult(string filePath, int width = 0, int height = 0)
            : base(filePath, contentType: "jpeg")
        {
            _filePath = filePath;
            _width = width;
            _height = height;
        }

        public static ImageResult FromDocument(Func<Document> fetcher, string targetFilePath, int width = 0, int height = 0)
        {
            if (File.Exists(targetFilePath))
            {
                // The original file has already been cached so just get that one
                return new ImageResult(targetFilePath, width, height);
            }

            // Fetch it from the Db
            var document = fetcher();

            if (document == null)
                return null;

            // Store the original image to the disk
            try
            {
                File.WriteAllBytes(targetFilePath, document.Data);
            }
            catch
            {
                // Image is already currently in middle of storing
                // Wait until it's available
                int attempts = 0;
                while (!File.Exists(targetFilePath) && attempts < 20)
                {
                    attempts++;
                    Thread.Sleep(100);
                }
            }

            // Use the image result that will do the work and caching!
            return new ImageResult(targetFilePath, width, height);
        }

        protected override void WriteFile(HttpResponseBase response)
        {
            string resizedFilePath = GetResizedImagePath(_filePath, _width, _height);
            response.SetDefaultImageHeaders(resizedFilePath);
            WriteFileToResponse(resizedFilePath, response);
        }

        private static void WriteFileToResponse(string filePath, HttpResponseBase response)
        {
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                const int bufferLength = 65536;
                var buffer = new byte[bufferLength];

                while (true)
                {
                    int bytesRead = fs.Read(buffer, 0, bufferLength);

                    if (bytesRead == 0)
                    {
                        break;
                    }

                    response.OutputStream.Write(buffer, 0, bytesRead);
                }
            }
        }

        private static string GetResizedImagePath(string filepath, int width, int height)
        {
            string resizedPath = filepath;

            if (width > 0 || height > 0)
            {
                resizedPath = filepath.GetPathForResizedImage(width, height);

                if (!Directory.Exists(resizedPath))
                    Directory.CreateDirectory(new FileInfo(resizedPath).DirectoryName);

                if (!File.Exists(resizedPath))
                {
                    ResizeImageToFile(filepath, width, height, resizedPath);
                }
            }
            return resizedPath;
        }

        // This method might be better abstracted to an interface
        private static void ResizeImageToFile(string filepath, int width, int height, string resizedPath)
        {
            byte[] photoBytes = File.ReadAllBytes(filepath);
            const int quality = 70;
            ImageFormat format = ImageFormat.Jpeg;
            Size size = new Size(width, height);
            var layer = new ResizeLayer(size, backgroundColor: Color.White, resizeMode: ResizeMode.Pad);

            using (var inStream = new MemoryStream(photoBytes))
            {
                using (var memOutStream = new MemoryStream())
                using (var outStream = new FileStream(resizedPath, FileMode.CreateNew))
                {
                    // Initialize the ImageFactory using the overload to preserve EXIF metadata.
                    using (var imageFactory = new ImageFactory(preserveExifData: true))
                    {
                        // Load, resize, set the format and quality and save an image.
                        imageFactory.Load(inStream)
                            .Resize(layer)
                            .Format(format)
                            .Quality(quality)
                            .Save(memOutStream);

                        memOutStream.WriteTo(outStream);
                    }
                }
            }
        }
    }
}