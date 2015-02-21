using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using Telerik.Web.UI;

namespace BetterClassified.UI.Dsl
{
    public class SimpleImageProvider : IImageProvider
    {
        private string workingDirectory;
        private string serverID;
        private int thumbnailSize;
        private ImageFormat rawFormat;

        public string WorkingDirectory
        {
            get { return workingDirectory; }
            set
            {
                workingDirectory = value;
                ensureWorkingDirectories();
            }
        }

        public int ThumbnailSize
        {
            get { return thumbnailSize; }
            set { thumbnailSize = value; }
        }

        private void ensureWorkingDirectories()
        {
            if (!Directory.Exists(workingDirectory))
            {
                // this will almost certainly fail if the code ever gets here
                // ASP.NET is unlikely to have write permission on a non existent dir
                Directory.CreateDirectory(workingDirectory);
            }
            String path = Path.Combine(workingDirectory, WebImageMaker.RawImageDirName);
            foreach (string dir in workingDirectories())
            {
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            }
        }

        private IEnumerable<string> workingDirectories()
        {
            yield return Path.Combine(workingDirectory, WebImageMaker.RawImageDirName);
            yield return Path.Combine(workingDirectory, WebImageMaker.CanvasImageDirName);
            yield return Path.Combine(workingDirectory, WebImageMaker.ThumbnailImageDirName);
            yield return Path.Combine(workingDirectory, WebImageMaker.WebImageDirName);
        }

        public string ServerID
        {
            get { return serverID; }
            set { serverID = value; }
        }

        private string getRawFilePath()
        {
            string rawDir = Path.Combine(workingDirectory, WebImageMaker.RawImageDirName);
            return Path.Combine(rawDir, serverID);
        }

        private string getFilePath(string subdir, WebImageFormat format)
        {
            string dir = Path.Combine(workingDirectory, subdir);
            string fp = Path.Combine(dir, serverID);
            fp = appendExtension(fp, format);
            if (!File.Exists(fp))
            {
                return fp;
            }
            else
            {
                // a file already exists with this name. This will happen when we're
                // reusing a raw file.
                // need to save it with a versionnumber appended, e.g. test(2).gif
                int version = 2;
                int extPos = fp.LastIndexOf(".");
                String preext = fp.Substring(0, extPos);
                String ext = fp.Substring(extPos);
                fp = preext + "(" + version + ")" + ext;
                while (File.Exists(fp))
                {
                    fp = preext + "(" + (++version) + ")" + ext;
                }
                return fp;
            }
        }

        private string appendExtension(string path, WebImageFormat format)
        {
            switch (format)
            {
                case WebImageFormat.Gif:
                    path += ".gif";
                    break;

                case WebImageFormat.Jpg:
                    path += ".jpg";
                    break;

                case WebImageFormat.Png:
                    path += ".png";
                    break;
            }
            return path;
        }

        private void setGraphicsQuality(Graphics g, WebImageQuality quality)
        {
            switch (quality)
            {
                case WebImageQuality.High:
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    break;

                case WebImageQuality.Medium:
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    break;

                case WebImageQuality.Low:
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                    break;
            }
        }

        private ImageFormat getGDIFormat(WebImageFormat format)
        {
            ImageFormat fmt = null;
            switch (format)
            {
                case WebImageFormat.Gif:
                    fmt = ImageFormat.Gif;
                    break;

                case WebImageFormat.Jpg:
                    fmt = ImageFormat.Jpeg;
                    break;

                case WebImageFormat.Png:
                    fmt = ImageFormat.Png;
                    break;
            }
            return fmt;
        }

        private PurgeMethod purgeStrategy;

        public PurgeMethod PurgeStrategy
        {
            get { return purgeStrategy; }
            set { purgeStrategy = value; }
        }

        private void defaultPurge(string directoryToClean)
        {
            // delete any files that are older than 24 hrs
            DateTime dayAgo = DateTime.Now.AddDays(-1);
            DirectoryInfo df = new DirectoryInfo(directoryToClean);
            Array.ForEach<FileInfo>(Array.FindAll<FileInfo>(
                df.GetFiles(), delegate(FileInfo fi) { return fi.CreationTime < dayAgo; }),
                    delegate(FileInfo fiOld) { fiOld.Delete(); });

            // if there are still more than 100 files left, delete the oldest
            FileInfo[] files = df.GetFiles();
            if (files.Length > 100)
            {
                Array.Sort<FileInfo>(files,
                    delegate(FileInfo x, FileInfo y)
                    { return x.CreationTime < y.CreationTime ? -1 : 1; });
                for (int i = files.Length - 1; i > 100; i--)
                {
                    files[i].Delete();
                }
            }
        }

        private void purge(string dirname)
        {
            // clean out old raw and canvas files that have been uploaded
            string directoryToClean = Path.Combine(workingDirectory, dirname);
            if (PurgeStrategy == null)
            {
                PurgeStrategy = defaultPurge;
            }

            PurgeStrategy(directoryToClean);
        }

        public bool SaveRaw(UploadedFile uploadedFile, out string outServerID, out int rawWidth, out int rawHeight, out string thumbnailFileName)
        {
            purge(WebImageMaker.RawImageDirName);
            this.serverID = outServerID = Guid.NewGuid().ToString();
            string filepath = getRawFilePath();
            uploadedFile.SaveAs(filepath);
            return getRawInfo(filepath, out rawWidth, out rawHeight, true, out thumbnailFileName);
        }

        public bool SaveRaw(UploadedFile uploadedFile, out string outServerID, out int rawWidth, out int rawHeight, out string thumbnailFileName, bool doRescale)
        {
            purge(WebImageMaker.RawImageDirName);
            this.serverID = outServerID = Guid.NewGuid().ToString();
            string filePath = getRawFilePath();
            if (doRescale)
            {
                Image rawImage = Image.FromStream(uploadedFile.InputStream);
                Image rawImageMaximised = Paramount.ImageHelper.Resize(rawImage, 600, 600, 300);
                rawImageMaximised.Save(filePath);
            }
            else
            {
                uploadedFile.SaveAs(filePath);
            }
            return getRawInfo(filePath, out rawWidth, out rawHeight, true, out thumbnailFileName);
        }

        public bool UseThumbnailFile(string thumbnailFileName, out String outServerID, out int rawWidth, out int rawHeight)
        {
            this.serverID = thumbnailFileName.Substring(0, thumbnailFileName.LastIndexOf("."));
            outServerID = serverID;
            string dummy; // getRawInfo needs a variable to put the thumbnail name in, but we don't want a thumbnail
            return getRawInfo(getRawFilePath(), out rawWidth, out rawHeight, false, out dummy);
        }

        private bool getRawInfo(string filepath, out int rawWidth, out int rawHeight, bool createThumbnail, out string thumbnailFileName)
        {
            thumbnailFileName = "";
            bool result = false;
            rawWidth = 0;
            rawHeight = 0;
            try
            {
                using (Image img = Image.FromFile(filepath))
                {
                    rawWidth = img.Width;
                    rawHeight = img.Height;
                    rawFormat = img.RawFormat;
                    result = true;
                    if (createThumbnail)
                    {
                        thumbnailFileName = CreateThumbnail(img, WebImageFormat.Jpg);
                    }
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        private string CreateThumbnail(Image img, WebImageFormat format)
        {
            purge(WebImageMaker.ThumbnailImageDirName);
            string thumbFileName = null;
            Rectangle rawRect = new Rectangle(0, 0, img.Width, img.Height);
            Rectangle thumbRect = new Rectangle();
            float fWidth = (float)img.Width;
            float fHeight = (float)img.Height;
            float fThumbSize = (float)thumbnailSize;
            float aspectRatio = fWidth / fHeight;
            if (aspectRatio > 1)
            {
                thumbRect.Width = thumbnailSize;
                thumbRect.X = 0;
                thumbRect.Height = Convert.ToInt32((fThumbSize / fWidth) * fHeight);
                thumbRect.Y = (thumbnailSize - thumbRect.Height) / 2;
            }
            else
            {
                thumbRect.Height = thumbnailSize;
                thumbRect.Y = 0;
                thumbRect.Width = Convert.ToInt32((fThumbSize / fHeight) * fWidth);
                thumbRect.X = (thumbnailSize - thumbRect.Width) / 2;
            }
            using (Bitmap thumb = new Bitmap(thumbnailSize, thumbnailSize, PixelFormat.Format24bppRgb))
            {
                using (Graphics g = Graphics.FromImage(thumb))
                {
                    setGraphicsQuality(g, WebImageQuality.High);
                    g.Clear(Color.White);
                    g.DrawImage(img, thumbRect, rawRect, GraphicsUnit.Pixel);
                    string filepath = getFilePath(WebImageMaker.ThumbnailImageDirName, format);
                    thumb.Save(filepath, getGDIFormat(format));
                    thumbFileName = Path.GetFileName(filepath);
                }
            }
            return thumbFileName;
        }

        public string CreateCanvas(WebImageFormat format, WebImageQuality quality, int canvasWidth, int canvasHeight)
        {
            purge(WebImageMaker.CanvasImageDirName);
            string canvasFileName = null;
            using (Image rawImg = Image.FromFile(getRawFilePath()))
            {
                using (Bitmap canvas = new Bitmap(canvasWidth, canvasHeight, PixelFormat.Format24bppRgb))
                {
                    using (Graphics g = Graphics.FromImage(canvas))
                    {
                        setGraphicsQuality(g, quality);
                        g.DrawImage(rawImg, 0, 0, canvasWidth, canvasHeight);
                        string filePath = getFilePath(WebImageMaker.CanvasImageDirName, format);
                        canvas.Save(filePath, getGDIFormat(format));
                        canvasFileName = Path.GetFileName(filePath);
                    }
                }
            }
            return canvasFileName;
        }

        public string CropAndScale(System.Drawing.Rectangle transformedSelection, WebImageFormat format, WebImageQuality quality, int reqdWidth, int reqdHeight)
        {
            purge(WebImageMaker.WebImageDirName);
            string webFileName = null;
            Rectangle dest = new Rectangle(0, 0, reqdWidth, reqdHeight);
            using (Image rawImg = Image.FromFile(getRawFilePath()))
            {
                using (Bitmap webImage = new Bitmap(reqdWidth, reqdHeight, PixelFormat.Format24bppRgb))
                {
                    using (Graphics g = Graphics.FromImage(webImage))
                    {
                        setGraphicsQuality(g, quality);
                        g.DrawImage(rawImg, dest, transformedSelection, GraphicsUnit.Pixel);
                        string filePath = getFilePath(WebImageMaker.WebImageDirName, format);
                        webImage.Save(filePath, getGDIFormat(format));
                        webFileName = Path.GetFileName(filePath);
                    }
                }
            }
            return webFileName;
        }

        public string CropAndScale(Rectangle transformedSelection, WebImageFormat format, WebImageQuality quality, int reqdWidth, int reqdHeight, int reqResolution, out string imageFilePath)
        {
            purge(WebImageMaker.WebImageDirName);
            string webFileName = null;
            Rectangle dest = new Rectangle(0, 0, reqdWidth, reqdHeight);
            using (Image rawImg = Image.FromFile(getRawFilePath()))
            {
                using (Bitmap webImage = new Bitmap(reqdWidth, reqdHeight, PixelFormat.Format24bppRgb))
                {
                    webImage.SetResolution(reqResolution, reqResolution);
                    using (Graphics g = Graphics.FromImage(webImage))
                    {
                        setGraphicsQuality(g, quality);
                        g.DrawImage(rawImg, dest, transformedSelection, GraphicsUnit.Pixel);
                        string filePath = getFilePath(WebImageMaker.WebImageDirName, format);
                        webImage.Save(filePath, getGDIFormat(format));
                        webFileName = Path.GetFileName(filePath);
                        imageFilePath = filePath;
                    }
                }
            }
            return webFileName;
        }

        public string SaveRawImageAsWebImage(WebImageFormat format, WebImageQuality quality)
        {
            purge(WebImageMaker.WebImageDirName);
            string filePath = getFilePath(WebImageMaker.WebImageDirName, format);
            using (Image img = Image.FromFile(getRawFilePath()))
            {
                // if the raw image is the same format then just copy the raw image:
                if (getGDIFormat(format).Equals(img.RawFormat))
                {
                    File.Copy(getRawFilePath(), filePath);
                }
                else
                {
                    // we need to do some conversion, but don't bother with quality settings - 
                    // if it's the wrong format then the quality isn't going to be great anyway
                    img.Save(filePath, getGDIFormat(format));
                }
            }
            return Path.GetFileName(filePath);
        }

        public string SaveRawImageAsWebImage(WebImageFormat format, WebImageQuality quality, int reqResolution, out string fullPath)
        {
            purge(WebImageMaker.WebImageDirName);
            string filePath = getFilePath(WebImageMaker.WebImageDirName, format);
            using (Image image = Bitmap.FromFile(getRawFilePath()))
            {
                Bitmap img = (Bitmap)image;
                // we need to do some conversion, but don't bother with quality settings - 
                // if it's the wrong format then the quality isn't going to be great anyway
                img.SetResolution(reqResolution, reqResolution);
                img.Save(filePath, getGDIFormat(format));
                fullPath = filePath;
            }
            return Path.GetFileName(filePath);
        }
    }
}
