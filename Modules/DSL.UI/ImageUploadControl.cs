using System;
using System.Globalization;
using System.Web.UI.WebControls;
using Paramount.ApplicationBlock.Configuration;
using Paramount.Utility;
using Telerik.Web.UI;

namespace Paramount.DSL.UI
{
    public class ImageUploadControl : FileUploadControl
    {
        protected Image image;


        public ImageUploadControl()
        {
            image = new Image {ID = "image", Visible = false};
        }
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            this.Controls.Add(this.image);
        }

        public int? MaxHeight { get; set; }
        public int? MaxWidth { get; set; }

        public bool ExactDimensions { get; set; }

        public string ImageHandler { get; set; }

        protected override bool IsFileValid()
        {
            if (!MaxHeight.HasValue && !MaxWidth.HasValue)
            {
                return true;
            }

            foreach (UploadedFile uploadedFile in _radUpload.UploadedFiles)
            {
                System.Drawing.Image imageFile = System.Drawing.Image.FromStream(uploadedFile.InputStream);
                if (ExactDimensions)
                {
                    if (imageFile.Width == MaxWidth)
                    {
                        _validator.ErrorMessage = string.Format(CultureInfo.InvariantCulture,
                                                                "Image width has to be {0}px.", MaxWidth);
                        return false;
                    }

                    if (imageFile.Height == MaxHeight)
                    {
                        _validator.ErrorMessage = string.Format(CultureInfo.InvariantCulture,
                                                                "Image height has to be {0}px.", MaxHeight);
                        return false;
                    }
                }
                else
                {
                    if (imageFile.Width > MaxWidth)
                    {
                        _validator.ErrorMessage = string.Format(CultureInfo.InvariantCulture,
                                                                "Image width has to be less than {0}px.", MaxWidth);
                        return false;
                    }

                    if (imageFile.Height > MaxHeight)
                    {
                        _validator.ErrorMessage = string.Format(CultureInfo.InvariantCulture,
                                                                "Image height has to be less than {0}.px", MaxHeight);
                        return false;
                    }
                }
            }
            return true;
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (!string.IsNullOrEmpty(DocumentId))
            {
                image.ImageUrl = ConfigSettingReader.DslImageHandler +
                                 string.Format(CultureInfo.InvariantCulture,
                                               "?docid={0}&height=100&width=100&entity={1}",
                                               DocumentId,
                                               CryptoHelper.Encrypt(
                                                   ConfigSettingReader.
                                                       ClientCode));
                image.Visible = true;
            }
        }
    }
}