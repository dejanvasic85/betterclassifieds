namespace BetterClassified.UI
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.UI.WebControls;
    using Dsl;
    using Paramount.Betterclassified.Utilities.Configuration;
    using Paramount.Common.UI.BaseControls;
    using Paramount.DSL.UIController;
    using Paramount.DSL.UI;
    using Paramount.Utility;

    public class ImageGallery : ParamountCompositeControl
    {
        private readonly DataList _dataListThumbs;
        private readonly Image _mainImage;
        private readonly Panel _panelContainer;
        private readonly Panel _panelImage;
        private readonly Panel _panelImageList;

        #region Constructor
        public ImageGallery()
        {
            ImageCollection = new List<string>();
            _mainImage = new Image { CssClass = "imagegallery-mainimage" };

            var galleryThumbTemplate = new ImageThumbTemplate();
            galleryThumbTemplate.ThumbImageClick += ImageSelected;

            _dataListThumbs = new DataList
            {
                ItemTemplate = galleryThumbTemplate,
                RepeatDirection = RepeatDirection.Horizontal,
                CellPadding = 3
            };

            _panelContainer = new Panel { CssClass = "imagegallery-container" };
            _panelImage = new Panel { CssClass = "imagegallery-panelimage" };
            _panelImageList = new Panel { CssClass = "imagegallery-panelimagelist" };
        }
        #endregion

        void ImageSelected(object sender, EventArgs e)
        {
            SelectedImageId = ((ImageThumb)sender).DocumentId;
            _dataListThumbs.DataSource = GetSource();
            _dataListThumbs.DataBind();
        }

        internal List<string> GetSource()
        {
            //return AdController.GetAdGraphicDocuments(AdDesignId);
            return ImageList;
        }

        protected override void CreateChildControls()
        {
            // Cast the data source to list of strings (document id's)
            var list = GetSource();
            if (list != null)
            {
                foreach (string s in list)
                {
                    ImageCollection.Add(s);
                }

                if (list.Count > 0)
                {
                    // Set the image to be the first selected
                    SelectedImageId = ImageCollection[0];

                    _panelImage.Controls.Add(_mainImage);
                    _panelContainer.Controls.Add(_panelImage);
                }

                if (list.Count > 1)
                {
                    _dataListThumbs.DataSource = list;
                    _dataListThumbs.DataBind();

                    _panelImageList.Controls.Add(_dataListThumbs);
                    _panelContainer.Controls.Add(_panelImageList);
                }

                Controls.Add(_panelContainer);
            }
        }

        #region Properties

        private List<string> ImageCollection
        {
            get;
            set;
        }

        public List<string> ImageList
        {
            get
            {
                return (List<string>)ViewState["imageGuidList"];
            }
            set
            {
                ViewState["imageGuidList"] = value;
            }
        }

        public int AdDesignId
        {
            get
            {
                return int.Parse(ViewState["AdDesignId"].ToString());
            }
            set
            {
                ViewState["AdDesignId"] = value;
            }
        }

        private string SelectedImageId
        {
            set
            {
                var queryParam = new DslQueryParam(HttpContext.Current.Request.QueryString)
                {
                    DocumentId = value,
                    Entity = CryptoHelper.Encrypt(Paramount.ApplicationBlock.Configuration.ConfigSettingReader.ClientCode),
                    Width = MainImageWidth,
                    Height = MainImageHeight,
                    Resolution = BetterclassifiedSetting.DslDefaultResolution
                };
                string dslHandlerUrl = BetterclassifiedSetting.DslImageUrlHandler;
                _mainImage.ImageUrl = queryParam.GenerateUrl(dslHandlerUrl);
            }
        }

        public int MainImageHeight
        {
            get
            {
                return int.Parse(ViewState["MainImageHeight"].ToString());
            }
            set
            {
                ViewState["MainImageHeight"] = value;
            }
        }

        public int MainImageWidth
        {
            get
            {
                return int.Parse(ViewState["MainImageWidth"].ToString());
            }
            set
            {
                ViewState["MainImageWidth"] = value;
            }
        }

        #endregion
    }
}