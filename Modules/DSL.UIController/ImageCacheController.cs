using System.IO;
using Paramount.ApplicationBlock.Configuration;
using System.Drawing;

namespace Paramount.DSL.UIController
{
    public static class ImageCacheController
    {
        const string RawFilePathFormat = "{0}_RAW.jpg";

        public static Stream Get(string documentId, decimal? width, decimal? height, int resolution, out bool isRaw)
        {
            isRaw = false;

            var fileInfo = GetCacheFile(documentId, width, height, resolution);

            // Check if the exact document exists
            if (!fileInfo.Exists)
            {
                // Check if the raw image exists at least return that
                fileInfo = GetRawCacheFile(documentId);

                if (!fileInfo.Exists)
                {
                    return null;
                }

                isRaw = true;
            }

            FileStream stream = new FileStream(fileInfo.FullName, FileMode.Open);

            return stream;
        }

        public static void Put(Image image, string documentId, decimal? width, decimal? height, int resolution)
        {
            var documentFileInfo = GetCacheFile(documentId, width, height, resolution);
            
            if (!documentFileInfo.Exists)
            {
                //int length = (int)documentStream.Length;
                //using (FileStream fileStream = File.Create(documentFileInfo.FullName, length))
                //{
                //    byte[] bytesInStream = new byte[length];
                //    documentStream.Read(bytesInStream, 0, length);
                //    fileStream.Write(bytesInStream, 0, length);
                //}
                image.Save(documentFileInfo.FullName);
            }

        }

        public static FileInfo GetCacheFile(string documentId, decimal? width, decimal? height, int resolution)
        {
            string filePathFormat = "{0}_{1}_{2}_{3}.jpg";
            string fullPath = Path.Combine(ConfigSettingReader.DslImageCacheDirectory.FullName, string.Format(filePathFormat, documentId, width, height, resolution));
            FileInfo info = new FileInfo(fullPath);
            return info;
        }

        public static FileInfo GetRawCacheFile(string documentId)
        {
            string filePathFormat = "{0}_RAW.jpg";
            string fullPath = Path.Combine(ConfigSettingReader.DslImageCacheDirectory.FullName, string.Format(filePathFormat, documentId));
            FileInfo info = new FileInfo(fullPath);
            return info;
        }
    }
}
