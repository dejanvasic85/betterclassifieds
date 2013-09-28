using System.Web.UI;
using Paramount.Common.DataTransferObjects.DSL;

[assembly: WebResource("BetterClassified.UI.JavaScript.fileuploadvalidation.js", "application/x-javascript")]


namespace BetterClassified.UI
{
    using System;
    using System.Web.UI;
    using System.Collections.Generic;
    using System.Web.Security;
    using System.Web.UI.WebControls;
    using BetterClassified.UI.Dsl;
    using Paramount.ApplicationBlock.Configuration;
    using Paramount.Common.UI.BaseControls;
    using Paramount.DSL.UIController;
    using Telerik.Web.UI;

    public class MultipleFileUpload : ParamountCompositeControl
    {
        private Panel _panelContainer;
        private Panel _panelImages;
        private Panel _panelUserInfo;
        private Panel _panelButtonUpload;
        private ErrorList _errorList;
        private RadUpload _radUpload;
        private DataList _imageList;
        private Button _buttonUpload;
        private Label _userInfo;
        private Label _maxFileUserInfo;
        private DslDocumentCategory _dslDocumentCategory;

        public event RemoveDocumentEventHandler RemoveDocument;
        public event EventHandler UploadComplete;

        public delegate void RemoveDocumentEventHandler(object sender, SelectedDocumentArgs e);

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            // Page.ClientScript.RegisterClientScriptResource(this.GetType(), "BetterClassified.UI.JavaScript.multiplefileupload.js");

            String rsname = "BetterClassified.UI.JavaScript.fileuploadvalidation.js";
            Type rstype = typeof(MultipleFileUpload);

            // Get a ClientScriptManager reference from the Page class.
            ClientScriptManager cs = Page.ClientScript;

            // Register the client resource with the page.
            cs.RegisterClientScriptResource(rstype, rsname);


        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            // Retrieve the dsl Document Category entity details for validation
            _dslDocumentCategory = DslController.GetDocumentCategory(DocumentCategory);

            _radUpload = new RadUpload
            {
                ID = "multiUpload",
                MaxFileInputsCount = MaxFiles,
                InitialFileInputsCount = MaxFiles,
                MaxFileSize = (int)_dslDocumentCategory.MaximumFileSize,
                AllowedFileExtensions = _dslDocumentCategory.AcceptedFileTypes.ToArray(),
                CssClass = "multiplefileupload-radupload",
                ControlObjectsVisibility = ControlObjectsVisibility.None
            };

            var template = new ImageThumbCommandTemplate();
            template.ThumbCommandClick += ThumbCommandClick;

            _imageList = new DataList
            {
                ItemTemplate = template,
                RepeatDirection = RepeatDirection.Horizontal,
                CellPadding = 3,
                CssClass = "multiplefileupload-imagelist"
            };
            _imageList.ItemStyle.VerticalAlign = VerticalAlign.Top;

            _buttonUpload = new Button { ID = "btnSubmitFiles", Text = "Upload", CssClass = "multiplefileupload-buttonupload" };
            _buttonUpload.Click += UploadButtonClick;

            // Contruct the user message for displaying file restrictions
            // todo (Web Content)
            _userInfo = new Label
            {
                Text = string.Format("Accepted files: [{0}] | Maximum Size: {1} MB", _dslDocumentCategory.AcceptedFilesToString(), _dslDocumentCategory.ConvertedMaxSizeToMegabytes()),
                CssClass = "multiplefileupload-userinfo"
            };

            _maxFileUserInfo = new Label
            {
                Text = string.Format("Maximum Files Allowed: [{0}]", MaxFiles),
                CssClass = "multiplefileupload-userinfo"
            };

            _errorList = new ErrorList();

            // Create Panel Containers
            _panelContainer = new Panel { CssClass = "multiplefileupload-panelcontainer" };
            _panelImages = new Panel { CssClass = "multiplefileupload-panelimages" };
            _panelUserInfo = new Panel { CssClass = "multiplefileupload-paneluserinfo" };
            _panelButtonUpload = new Panel { CssClass = "multiplefileupload-panelbuttonupload" };

            // Check whether we need to use fancy upload mechanism
            if (IsUploadOnSelect)
            {
                _buttonUpload.Style.Add("visibility", "hidden");
                _radUpload.OnClientFileSelected = "submitFile";
            }

            EnsureChildControls();
            DataBindImages();

            _panelUserInfo.Controls.Add(_userInfo);
            //_panelUserInfo.Controls.Add(_maxFileUserInfo);
            _panelImages.Controls.Add(_imageList);
            _panelButtonUpload.Controls.Add(_buttonUpload);

            _panelContainer.Controls.Add(_errorList);
            _panelContainer.Controls.Add(_panelUserInfo);
            _panelContainer.Controls.Add(_radUpload);
            _panelContainer.Controls.Add(_panelButtonUpload);
            _panelContainer.Controls.Add(_panelImages);
            Controls.Add(_panelContainer);
        }

        //protected override void Render(HtmlTextWriter writer)
        //{
        //    base.Render(writer);

        //    writer.RenderBeginTag(HtmlTextWriterTag.P);
        //    _userInfo.RenderControl(writer);
        //    writer.RenderEndTag();
        //}

        void UploadButtonClick(object sender, EventArgs e)
        {
            var docList = ImageList;
            int count = 0;

            if (_radUpload.InvalidFiles.Count > 0)
            {
                // Validation failed, the files they are attempting do not suit the DSL Category - display error message
                // todo (Web Content)
                var errors = new List<string>
                                 {
                                     "One of the files submitted is not valid. Please review the accepted file types and size before submitting your image."
                                 };
                _errorList.DisplayErrors(errors);
                return;
            }

            // Hide any previous errors
            _errorList.HideErrors();

            foreach (UploadedFile file in _radUpload.UploadedFiles)
            {
                // Save Document to the Dsl Repository
                var docId = DslController.UploadDslDocument(DocumentCategory, file.InputStream, file.ContentLength,
                                                                file.GetName(), file.ContentType, Membership.GetUser().UserName,
                                                                ReferenceData, ConfigSettingReader.ApplicationName, ConfigSettingReader.ClientCode);
                docList.Add(docId);
                count++;
            }

            // Only continue if more than one file was uploaded
            if (count > 0)
            {
                ImageList = docList;
            }

            DataBindImages();

            // Finally bubble up the event to the UI
            InvokeUploadComplete(sender, e);
        }

        void ThumbCommandClick(object sender, EventArgs e)
        {
            // Remove the selected document
            var documentId = ((ImageThumbCommand)sender).DocumentId;

            // Call service method to delete the document from the Database
            DslController.DeleteDocument(documentId);

            var updatedList = ImageList;
            updatedList.Remove(documentId);
            ImageList = updatedList;

            // Data bind the data list again
            DataBindImages();

            // Finally invoke the handler to bubble up to the Caller
            InvokeOnRemoveDocument(sender, new SelectedDocumentArgs(documentId));
        }

        private void InvokeOnRemoveDocument(object sender, SelectedDocumentArgs e)
        {
            RemoveDocumentEventHandler handler = RemoveDocument;
            if (handler != null) handler(sender, e);
        }

        private void InvokeUploadComplete(object sender, EventArgs e)
        {
            EventHandler handler = UploadComplete;
            if (handler != null) handler(sender, e);
        }

        private void DataBindImages()
        {
            // Set number of max files
            _radUpload.MaxFileInputsCount = MaxFiles - ImageList.Count;
            _radUpload.InitialFileInputsCount = MaxFiles - ImageList.Count;

            // Completed disable the control so that no more images can be uploaded
            bool isUploadEnabled = (MaxFiles > ImageList.Count);
            _radUpload.Enabled = isUploadEnabled;
            _panelButtonUpload.Visible = isUploadEnabled;

            _imageList.DataSource = ImageList;
            _imageList.DataBind();
        }

        #region Properties

        public List<string> ImageList
        {
            get
            {
                var list = new List<string>();
                if (ViewState["ImageList"] != null)
                {
                    list = (List<string>)ViewState["ImageList"];
                }
                return list;
            }
            set
            {
                ViewState["ImageList"] = value;
            }
        }

        public int MaxFiles
        {
            get
            {
                return int.Parse(ViewState["MaxFilesForUpload"].ToString());
            }
            set
            {
                ViewState["MaxFilesForUpload"] = value;
            }
        }

        public DslDocumentCategoryType DocumentCategory
        {
            get
            {
                // Default to general category
                DslDocumentCategoryType categoryType = DslDocumentCategoryType.General;
                if (ViewState["DocumentCategoryType"] != null)
                {
                    categoryType = (DslDocumentCategoryType)ViewState["DocumentCategoryType"];
                }
                return categoryType;
            }
            set
            {
                ViewState["DocumentCategoryType"] = value;
            }
        }

        public string ReferenceData
        {
            get
            {
                string reference = string.Empty;
                if (ViewState["ReferenceData"] != null)
                {
                    reference = ViewState["ReferenceData"].ToString();
                }
                return reference;
            }
            set
            {
                ViewState["ReferenceData"] = value;
            }
        }

        public bool IsUploadOnSelect
        {
            get
            {
                return ViewState["isUploadSelect"] != null && bool.Parse(ViewState["isUploadSelect"].ToString());
            }
            set
            {
                ViewState["isUploadSelect"] = value;
            }
        }

        #endregion
    }
}