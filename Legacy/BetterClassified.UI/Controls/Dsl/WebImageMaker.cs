using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using BetterClassified.UI.Dsl;
using Paramount.ApplicationBlock.Configuration;
using Paramount.Betterclassified.Utilities.Configuration;
using Paramount.Common.DataTransferObjects.DSL;
using Paramount.Common.UI;
using Paramount.Common.UI.BaseControls;
using Paramount.Utility;
using Paramount.DSL.UIController;
using Telerik.Web.UI;

[assembly: WebResource("BetterClassified.UI.JavaScript.WebImageMaker_canvas.js", "application/x-javascript")]
[assembly: WebResource("BetterClassified.UI.JavaScript.WebImageMaker_normal.js", "application/x-javascript")]

namespace BetterClassified.UI
{
    public delegate void PurgeMethod(string directoryToClean);

    [DefaultProperty("CurrentImageUrl")]
    [ToolboxData("<{0}:WebImageMaker1 runat=\"server\"><{0}:WebImageMaker1>")]
    public class WebImageMaker : ParamountCompositeControl
    {
        #region Delegates

        public delegate void RemoveCompleteEventHandler(object sender, SelectedDocumentArgs e);

        public delegate void UploadCompleteEventHandler(object sender, SelectedDocumentArgs e);

        #endregion

        public const string WebImageMakerID = "2a8b9574-6620-4df2-a36d-e8012bf624ba";
        public const string RawImageDirName = "raw";
        public const string CanvasImageDirName = "canvas";
        public const string ThumbnailImageDirName = "thumbnails";
        public const string WebImageDirName = "web";
        private IImageProvider _imageProvider;

        private HelpContextControl _popupHelpContext;
        private float aspectRatio;
        private LinkButton btnRemoveImage;

        private Button cancelSelection;
        private HtmlImage canvas;
        private int canvasHeight;
        private string canvasImageName;
        private int canvasWidth;
        private Button confirmSelection;
        private ControlMode controlMode = ControlMode.Normal;
        private ErrorList errorList;
        private string handlerPath;
        private bool hasChanged;
        private HiddenField hiddenField;
        private string imageHeight = "100";
        private string imageWidth = "100";
        private int intImageHeight = -1;
        private int intImageWidth = -1;
        private int isServingImageState = -1; // -1 indicates unset
        private Label lblDebugInfo;
        private Label _lblInstruction;
        private Label lblUserInfo;
        private string originalSrc;
        private Panel pnlButtonUpload;
        private Panel pnlContainer;
        private Panel pnlRemoveImage;
        private Panel pnlUploaded;
        private HtmlGenericControl popupButtons;
        private HtmlGenericControl popupContainer;
        private HtmlGenericControl popupDiv;
        private HtmlGenericControl popupTitle;
        private HtmlGenericControl popupWarning;
        private RadUpload radUpload;
        private int rawHeight;
        private int rawWidth;
        private HtmlGenericControl selectionBox;

        private string serverImgID;
        private HtmlImage targetImage;
        private int thumbnailSize = 64;
        private Button uploadButton;
        private WebImageFormat webImageFormat = WebImageFormat.Jpg;
        private string webImageName;
        private WebImageQuality webImageQuality = WebImageQuality.High;
        private string workingDirectory;

        private bool IsServingImage
        {
            get
            {
                if (isServingImageState == -1)
                {
                    try
                    {
                        isServingImageState = (HttpContext.Current.Request.QueryString["mode" + KeySuffix] != null)
                                                  ? 1
                                                  : 0;
                    }
                    catch
                    {
                        isServingImageState = 0; // probably being called by the designer
                    }
                }
                return (isServingImageState == 1);
            }
        }

        private string KeySuffix
        {
            get { return "_" + WebImageMakerID; }
        }

        #region Properties

        private PurgeMethod _purgeStrategy;

        public PurgeMethod PurgeStrategy
        {
            get { return _purgeStrategy; }
            set { _purgeStrategy = value; }
        }

        [Bindable(false)]
        [Category("Appearance")]
        [Themeable(false)]
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
            set { ViewState["DocumentCategoryType"] = value; }
        }

        [Bindable(false)]
        [Category("Appearance")]
        [DefaultValue("100")]
        [Themeable(false)]
        public string ImageWidth
        {
            get
            {
                imageWidth = ViewState["imageWidth"].ToString();
                return ViewState["imageWidth"].ToString();
            }
            set
            {
                if (Int32.TryParse(value, out intImageWidth) && intImageWidth > 0 && !IsServingImage)
                {
                    EnsureChildControls();
                    //targetImage.Width = intImageWidth;
                    imageWidth = intImageWidth.ToString();
                    ViewState["imageWidth"] = imageWidth;
                }
                else
                {
                    imageWidth = "*";
                    intImageWidth = -1;
                }
            }
        }


        [Bindable(false)]
        [Category("Appearance")]
        [DefaultValue("100")]
        [Themeable(false)]
        public string ImageHeight
        {
            get
            {
                imageHeight = ViewState["imageHeight"].ToString();
                return ViewState["imageHeight"].ToString();
            }
            set
            {
                if (Int32.TryParse(value, out intImageHeight) && intImageHeight > 0 && !IsServingImage)
                {
                    EnsureChildControls();
                    //targetImage.Height = intImageHeight;
                    imageHeight = intImageHeight.ToString();
                    ViewState["imageHeight"] = imageHeight;
                }
                else
                {
                    imageHeight = "*";
                    intImageHeight = -1;
                }
            }
        }

        [Bindable(false)]
        [Category("Appearance")]
        [DefaultValue("Change...")]
        [Themeable(false)]
        public string UploadButtonText
        {
            get
            {
                EnsureChildControls();
                return uploadButton.Text;
            }
            set
            {
                if (!IsServingImage) // child controls won't be instantiated
                {
                    EnsureChildControls();
                    uploadButton.Text = value;
                }
            }
        }

        [Bindable(false)]
        [Category("Appearance")]
        [Themeable(false)]
        public string ImageResolution
        {
            get { return ViewState["imageResolution"].ToString(); }
            set { ViewState["imageResolution"] = value; }
        }

        [Bindable(false)]
        [Category("Appearance")]
        [DefaultValue("Cancel")]
        [Themeable(false)]
        public string CancelButtonText
        {
            get
            {
                EnsureChildControls();
                return cancelSelection.Text;
            }
            set
            {
                if (!IsServingImage) // child controls won't be instantiated
                {
                    EnsureChildControls();
                    cancelSelection.Text = value;
                }
            }
        }

        [Bindable(false)]
        [Category("Appearance")]
        [DefaultValue("Confirm Selection")]
        [Themeable(false)]
        public string ConfirmButtonText
        {
            get
            {
                EnsureChildControls();
                return confirmSelection.Text;
            }
            set
            {
                if (!IsServingImage) // child controls won't be instantiated
                {
                    EnsureChildControls();
                    confirmSelection.Text = value;
                }
            }
        }

        [Bindable(false)]
        [Category("Appearance")]
        [Themeable(false)]
        public string WorkingDirectory
        {
            get
            {
                workingDirectory = ViewState["workingDirectory"].ToString();
                return workingDirectory;
            }
            set
            {
                ViewState["workingDirectory"] = value;
                workingDirectory = value;
            }
        }

        [Bindable(false)]
        [Category("Appearance")]
        [Themeable(false)]
        public string HandlerPath
        {
            get { return handlerPath; }
            set { handlerPath = value; }
        }

        [Bindable(false)]
        [Category("Appearance")]
        [DefaultValue(WebImageFormat.Jpg)]
        [Themeable(false)]
        public WebImageFormat Format
        {
            get { return webImageFormat; }
            set { webImageFormat = value; }
        }

        [Bindable(false)]
        [Category("Appearance")]
        [DefaultValue(WebImageQuality.High)]
        [Themeable(false)]
        public WebImageQuality Quality
        {
            get { return webImageQuality; }
            set { webImageQuality = value; }
        }


        [Bindable(false)]
        [Category("Appearance")]
        [DefaultValue(64)]
        [Themeable(false)]
        public int ThumbnailSize
        {
            get { return thumbnailSize; }
            set { thumbnailSize = value; }
        }

        public string DocumentID
        {
            get
            {
                string docId = string.Empty;
                if (ViewState["documentid"] != null)
                {
                    docId = ViewState["documentid"].ToString();
                }
                return docId;
            }
            set
            {
                string docId = value;
                bool isUploadEnabled = string.IsNullOrEmpty(docId);
                if (!string.IsNullOrEmpty(docId))
                {
                    var queryParam = new DslQueryParam(HttpContext.Current.Request.QueryString)
                                         {
                                             Entity = CryptoHelper.Encrypt(ConfigSettingReader.ClientCode),
                                             Height = 142,
                                             //Decimal.Parse(this.ImageHeight),
                                             Width = 150,
                                             // Decimal.Parse(this.ImageWidth),
                                             Resolution = BetterclassifiedSetting.DslDefaultResolution,
                                             DocumentId = docId
                                         };
                    originalSrc = queryParam.GenerateUrl(BetterclassifiedSetting.DslImageUrlHandler);
                }
                pnlUploaded.Visible = !isUploadEnabled;
                targetImage.Visible = !isUploadEnabled;
                radUpload.Visible = isUploadEnabled;
                pnlButtonUpload.Visible = isUploadEnabled;
                targetImage.Src = originalSrc;
                ViewState["documentid"] = value;
            }
        }

        public string WebImagePath
        {
            get
            {
                if (controlMode == ControlMode.Changed)
                {
                    return Path.Combine(Path.Combine(workingDirectory, WebImageDirName), webImageName);
                }
                else
                {
                    return null;
                }
            }
        }

        // it might be useful to have this available to other controls on the page.
        public string CurrentImageUrl
        {
            get { return targetImage.Src; }
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
            set { ViewState["ReferenceData"] = value; }
        }

        private string FileName
        {
            get { return ViewState["fileName"] != null ? ViewState["fileName"].ToString() : string.Empty; }
            set { ViewState["fileName"] = value; }
        }

        private string FileType
        {
            get { return ViewState["fileType"] != null ? ViewState["fileType"].ToString() : string.Empty; }
            set { ViewState["fileType"] = value; }
        }

        private List<string> SessionImages
        {
            get
            {
                var sessionImages = new List<string>();
                try
                {
                    if (HttpContext.Current != null) // will be null if called by designer
                    {
                        string key = "WEBIMAGEMAKER_IMAGES";
                        sessionImages = (List<string>) HttpContext.Current.Session[key];
                        if (sessionImages == null)
                        {
                            sessionImages = new List<string>();
                            HttpContext.Current.Session[key] = sessionImages;
                        }
                    }
                }
                catch
                {
                }
                return sessionImages;
            }
        }

        private IImageProvider ImageProvider
        {
            get
            {
                if (_imageProvider == null)
                {
                    // For now we'll just instantiate our implementation here as it's the only
                    // one. In the future the ImageProvider could be implemented along 
                    // provider model lines
                    _imageProvider = new SimpleImageProvider();
                    _imageProvider.WorkingDirectory = workingDirectory;
                    _imageProvider.ServerID = serverImgID;
                    _imageProvider.ThumbnailSize = thumbnailSize;
                    _imageProvider.PurgeStrategy = _purgeStrategy;
                }
                return _imageProvider;
            }
        }

        #endregion

        #region Constructor / Init / Rendering

        protected override void OnInit(EventArgs e)
        {
            // here's the dirty code to hijack the page request and
            // serve out the canvas image... If you don't like it,
            // use the handler
            if (Page.Request.QueryString["mode" + KeySuffix] != null)
            {
                var helper = new WebImageMakerImageHelper(workingDirectory, KeySuffix);
                helper.serveImage();
            }

            Page.RegisterRequiresControlState(this);
            base.OnInit(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            String jsCanvasUrl, jsThumbsUrl;

            //cssUrl = Page.ClientScript.GetWebResourceUrl(this.GetType(), "BetterClassified.UI.JavaScript.WebImageMaker.css");
            //string cssLink = "<link href=\"" + cssUrl + "\" rel=\"stylesheet\" type=\"text/css\" />";
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "WebImageMaker_Control_css", "<link href=\"" + cssUrl + "\" rel=\"stylesheet\" type=\"text/css\" />", false);

            if (controlMode == ControlMode.Canvas)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), ClientID, getInitScript(), true);
                jsCanvasUrl = Page.ClientScript.GetWebResourceUrl(GetType(),
                                                                  "BetterClassified.UI.JavaScript.WebImageMaker_canvas.js");
                //jsCanvasUrl = Page.ResolveUrl("~/Resources/Javascript/WebImageMaker_canvas.js");
                Page.ClientScript.RegisterClientScriptInclude("WebImageMaker_Control_Canvas_js", jsCanvasUrl);
            }
            else
            {
                jsThumbsUrl = Page.ClientScript.GetWebResourceUrl(GetType(),
                                                                  "BetterClassified.UI.JavaScript.WebImageMaker_normal.js");
                //jsThumbsUrl = Page.ResolveUrl("~/Resources/Javascript/WebImageMaker_normal.js");
                Page.ClientScript.RegisterClientScriptInclude("WebImageMaker_Control_Thumbs_js", jsThumbsUrl);

                if (SessionImages.Count > 0)
                {
                    // - Uncomment for Previous images
                    //Page.ClientScript.RegisterStartupScript(GetType(), ClientID, "registerThumbnailDiv('" + thumbnailsDiv.ClientID + "');" + Environment.NewLine, true);

                    // If this postback involved uploading a new image, a new thumbnail will have
                    // been created since the set of thumbnails was created in CreateChildControls().
                    // So we might need to add the most recent thumbnail to the collection:
                    // - Uncomment for Previous images
                    //if (SessionImages.Count > thumbnailsDiv.Controls.Count)
                    //{
                    //    thumbnailsDiv.Controls.Add(getThumbnailButton(SessionImages[SessionImages.Count - 1]));
                    //}
                }
            }
        }

        private string getInitScript()
        {
            string s =
                @"
function init{0}()
{{
    initialise('{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}');
}}

window.onload = init{0};

";
            return String.Format(s,
                                 ClientID, popupDiv.ClientID, canvas.ClientID, selectionBox.ClientID,
                                 imageWidth, imageHeight, targetImage.ClientID, confirmSelection.ClientID,
                                 popupContainer.ClientID, lblDebugInfo.ClientID);
        }

        protected override void CreateChildControls()
        {
            if (IsServingImage)
            {
                ChildControlsCreated = true;
                return;
            }

            // Retrieve the dsl Document Category entity details for validation
            DslDocumentCategory dslDocumentCategory = DslController.GetDocumentCategory(DocumentCategory);
            pnlContainer = new Panel {CssClass = "webimagemaker-panelcontainer"};
            pnlButtonUpload = new Panel {CssClass = "webimagemaker-panelbuttonupload"};
            pnlRemoveImage = new Panel {CssClass = "webimagemaker-panelremoveimage"};
            radUpload = new RadUpload
                            {
                                CssClass = "webimagemaker-radupload",
                                MaxFileInputsCount = 1,
                                ControlObjectsVisibility = ControlObjectsVisibility.None,
                                InitialFileInputsCount = 1,
                                AllowedFileExtensions = dslDocumentCategory.AcceptedFileTypes.ToArray(),
                                OnClientFileSelected = "submitPrintImage"
                            };
            pnlUploaded = new Panel {CssClass = "webimagemaker-panelimage"};
            btnRemoveImage = new LinkButton
                                 {ID = ID + "_btnRemove", Text = "Remove", CssClass = "webimagemaker-removeimage"};
            btnRemoveImage.Click += removeImage_Click;

            errorList = new ErrorList();
            targetImage = new HtmlImage {ID = string.Format("{0}_img", ID)};
            uploadButton = new Button {ID = "btnUploadPrintImage"};
            uploadButton.Style.Add("visibility", "hidden");

            hiddenField = new HiddenField();
            popupDiv = new HtmlGenericControl("div") {ID = string.Format("{0}_popup", ID)};
            // - Uncomment for Previous images
            //thumbnailsDiv = new HtmlGenericControl("div");
            //thumbnailButton = new HtmlInputButton();
            popupContainer = new HtmlGenericControl("div") {ID = string.Format("{0}_popupContainer", ID)};
            popupButtons = new HtmlGenericControl("div") {ID = string.Format("{0}_popupButtons", ID)};
            popupWarning = new HtmlGenericControl("div") {ID = string.Format("{0}_popupWarning", ID)};
            popupTitle = new HtmlGenericControl("div")
                             {ID = string.Format("{0}_popupInstruction", ID), InnerText = "Print Image Preparation"};

            canvas = new HtmlImage {ID = ID + "_canvas"};
            selectionBox = new HtmlGenericControl("div") {ID = ID + "_selection"};
            cancelSelection = new Button {ID = ID + "_cancel"};
            confirmSelection = new Button {ID = ID + "_confirm"};
            lblDebugInfo = new Label();

            lblUserInfo = new Label
                              {
                                  Text =
                                      string.Format("Accepted files: [{0}] | Maximum Size: {1} MB",
                                                    dslDocumentCategory.AcceptedFilesToString(),
                                                    dslDocumentCategory.ConvertedMaxSizeToMegabytes()),
                                  CssClass = "webimagemaker-userinfo"
                              };

            _lblInstruction = new Label
                                 {
                                     Text = @"Use the dashed box for panning and rescaling.",
                                     CssClass = "webimagemaker-instructionlabel"
                                 };

            _popupHelpContext = new HelpContextControl
                                    {
                                        ID = "helpContextCtrl",
                                        Position = PopupControlPopupPosition.Bottom,
                                        ImageUrl = "~/Resources/Images/question_button.gif",
                                        ContentTemplate  = new HelpContextTemplate{Text = "This is where you can resize and move your print image to fit in the print area. The red box represents the print area. <br /><b>To RESIZE </b>– move any corner of the red square, click and drag. <br /><b>To MOVE </b>– click anywhere in the red box and drag."} 
                                    };

            popupContainer.Attributes.Add("class", "webimagemaker-popup-container");
            popupButtons.Attributes.Add("class", "webimagemaker-popup-buttons");
            popupWarning.Attributes.Add("class", "webimagemaker-popup-warning");
            popupTitle.Attributes.Add("class", "webimagemaker-popup-title");
            popupDiv.Attributes.Add("class", "webimagemaker-popup");
            canvas.Attributes.Add("class", "webimagemaker-canvas");
            selectionBox.Attributes.Add("class", "webimagemaker-selection");
            cancelSelection.Attributes.Add("class", "webimagemaker-cancel");
            confirmSelection.Attributes.Add("class", "webimagemaker-confirm");

            Controls.Add(popupDiv);
            Controls.Add(hiddenField);
            popupTitle.Controls.Add(_popupHelpContext);
            popupDiv.Controls.Add(popupTitle);
            popupContainer.Controls.Add(canvas);
            popupWarning.Controls.Add(lblDebugInfo);

            popupButtons.Controls.Add(cancelSelection);
            popupButtons.Controls.Add(confirmSelection);
            popupContainer.Controls.Add(popupButtons);
            //popupDiv.Controls.Add(popupInstruction);
            popupDiv.Controls.Add(popupWarning);
            popupDiv.Controls.Add(popupContainer);
            popupDiv.Controls.Add(selectionBox);
            cancelSelection.Click += cancelSelection_Click;
            confirmSelection.UseSubmitBehavior = false;
            confirmSelection.OnClientClick = "storeSelectionInfo('" + hiddenField.ClientID + "')";
            confirmSelection.Click += confirmSelection_Click;

            pnlRemoveImage.Controls.Add(btnRemoveImage);
            pnlUploaded.Controls.Add(targetImage);
            pnlUploaded.Controls.Add(pnlRemoveImage);

            //Controls.Add(errorList);
            //Controls.Add(pnlUploaded);
            //Controls.Add(radUpload);

            // - Uncomment for Previous images
            //foreach (string thumbnailFilename in SessionImages)
            //{
            //    ImageButton thumbBtn = getThumbnailButton(thumbnailFilename);
            //    thumbBtn.Command += new CommandEventHandler(btnThumb_Command);
            //    thumbnailsDiv.Controls.Add(thumbBtn);
            //}

            //thumbnailsDiv.Attributes.Add("class", "webImageMaker_thumbs");
            //this.Controls.Add(thumbnailButton);
            //this.Controls.Add(thumbnailsDiv);
            //thumbnailButton.Attributes.Add("class", "webImageMaker_thumbpicker");
            //thumbnailButton.Value = "Previous Images";
            //thumbnailButton.Attributes.Add("onclick", "showThumbnailDiv('" + thumbnailsDiv.ClientID + "');");


            uploadButton.UseSubmitBehavior = false;
            uploadButton.OnClientClick = "setViewportDimensions('" + hiddenField.ClientID + "')";
            pnlButtonUpload.Controls.Add(uploadButton);

            //this.Controls.Add(uploadButton);
            uploadButton.Click += uploadButton_Click;

            //Controls.Add(pnlButtonUpload);

            pnlContainer.Controls.Add(errorList);
            pnlContainer.Controls.Add(lblUserInfo);
            pnlContainer.Controls.Add(radUpload);
            pnlContainer.Controls.Add(pnlUploaded);
            pnlContainer.Controls.Add(pnlButtonUpload);
            Controls.Add(pnlContainer);

            // show hide the target image
            targetImage.Visible = (!string.IsNullOrEmpty(targetImage.Src));
            ChildControlsCreated = true;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            AddAttributesToRender(writer);
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            pnlContainer.RenderControl(writer);
            //pnlUploaded.RenderControl(writer);

            if (controlMode == ControlMode.Canvas)
            {
                popupDiv.RenderControl(writer);
            }
            hiddenField.RenderControl(writer);
            if (controlMode == ControlMode.Normal)
            {
                targetImage.Src = originalSrc;
            }
            //targetImage.RenderControl(writer);
            writer.WriteBreak();
            if (controlMode != ControlMode.Canvas)
            {
                //lblUserInfo.RenderControl(writer);

                // - Uncomment for Previous Images
                //if (SessionImages.Count > 0)
                //{
                //    thumbnailButton.RenderControl(writer);
                //    writer.WriteBreak();
                //    thumbnailsDiv.RenderControl(writer);
                //}
            }
            //writer.WriteBreak();
            //pnlButtonUpload.RenderControl(writer);

            writer.RenderEndTag();
        }

        protected override void LoadControlState(object savedState)
        {
            var state = (string[]) savedState;
            if (!Int32.TryParse(state[0], out intImageWidth)) intImageWidth = -1;
            if (!Int32.TryParse(state[1], out intImageHeight)) intImageHeight = -1;
            serverImgID = state[2];
            originalSrc = state[3];
            controlMode = (ControlMode) Enum.Parse(typeof (ControlMode), state[4]);
            hasChanged = (state[5] == "1");
            if (!Int32.TryParse(state[6], out canvasWidth)) canvasWidth = 0;
            if (!Int32.TryParse(state[7], out canvasHeight)) canvasHeight = 0;
            workingDirectory = state[8];
            webImageFormat = (WebImageFormat) Enum.Parse(typeof (WebImageFormat), state[9]);
            webImageQuality = (WebImageQuality) Enum.Parse(typeof (WebImageQuality), state[10]);
            if (!Int32.TryParse(state[11], out rawWidth)) rawWidth = 0;
            if (!Int32.TryParse(state[12], out rawHeight)) rawHeight = 0;
            canvasImageName = state[13];
            webImageName = state[14];
            handlerPath = state[15];
            if (!Single.TryParse(state[16], out aspectRatio)) aspectRatio = 0;
            if (!Int32.TryParse(state[17], out thumbnailSize)) thumbnailSize = 64;
        }

        protected override object SaveControlState()
        {
            return new[]
                       {
                           intImageWidth.ToString(),
                           intImageHeight.ToString(),
                           serverImgID,
                           originalSrc,
                           controlMode.ToString(),
                           (hasChanged ? "1" : "0"),
                           canvasWidth.ToString(),
                           canvasHeight.ToString(),
                           workingDirectory,
                           webImageFormat.ToString(),
                           webImageQuality.ToString(),
                           rawWidth.ToString(),
                           rawHeight.ToString(),
                           canvasImageName,
                           webImageName,
                           handlerPath,
                           aspectRatio.ToString(),
                           thumbnailSize.ToString()
                       };
        }

        #endregion

        #region Event Handling

        private void uploadButton_Click(object sender, EventArgs e)
        {
            /*
             * see if we have a file in the form.
             * If we don't, display a notification message.
             * if we do and it's too small, display a notification message
             *  *** Actually we don't care if it's too small - we'll just scale it up
             * If we do and it's exactly the right size, use this image and go directly to changed.
             * If we do and it's bigger, go into Canvas mode and send back the necessary stuff
             */

            // perform validation on the image
            if (radUpload.InvalidFiles.Count > 0)
            {
                // Validation failed, the files they are attempting do not suit the DSL Category - display error message
                // todo (Web Content)
                var errors = new List<string>
                                 {
                                     "The file submitted is not valid. Please review the accepted file types and size before submitting your image."
                                 };
                errorList.DisplayErrors(errors);
                return;
            }

            if (radUpload.UploadedFiles.Count == 0)
            {
                return;
            }

            errorList.HideErrors();

            // is it an image?
            string thumbnailFileName;
            // should the raw image be rescaled to a more usable size for cropping?
            bool imageOK = ImageProvider.SaveRaw(radUpload.UploadedFiles[0], out serverImgID, out rawWidth,
                                                 out rawHeight, out thumbnailFileName);

            // Set the class properties for later use (uploading to DSL)
            FileName = radUpload.UploadedFiles[0].GetName();
            FileType = radUpload.UploadedFiles[0].ContentType;

            // we've got this far, so make a thumbnail and store the Guid in the user's 
            // session, so they can reuse the image later without having to upload it again.
            SessionImages.Add(thumbnailFileName);

            if (UseRaw())
            {
                InvokeUploadComplete(sender, new SelectedDocumentArgs(DocumentID));
            }
        }

        private void confirmSelection_Click(object sender, EventArgs e)
        {
            string[] clientDimensions = hiddenField.Value.Split(new[] {','});
            int x = Convert.ToInt32(clientDimensions[0]);
            int y = Convert.ToInt32(clientDimensions[1]);
            int w = Convert.ToInt32(clientDimensions[2]);
            int h = Convert.ToInt32(clientDimensions[3]);
            // now we have the x,y,w,h of the selection relative to the canvas, so
            // we have to scale the selection so that it is a selection from the 
            // original raw image:
            float scaleFactor = canvasWidth/(float) rawWidth;
            var transformedSelection = new Rectangle(
                (int) (x/scaleFactor),
                (int) (y/scaleFactor),
                (int) (w/scaleFactor),
                (int) (h/scaleFactor));
            // transformedSelection now represents the user's selected crop on the 
            // raw image rather than the canvas image

            // now determine what the dimensions of the final image should be. If 
            // ImageWidth and ImageHeight were both set then we already know, but
            // if one of them was "*" then we need to work out what it should
            // proportionally be from the user's selected crop shape:
            float selectionAspectRatio = w/(float) h;
            int reqdWidth = intImageWidth;
            int reqdHeight = intImageHeight;
            // these should never be both <= 0
            if (reqdWidth <= 0)
            {
                reqdWidth = (int) (selectionAspectRatio*reqdHeight);
            }
            else if (reqdHeight <= 0)
            {
                reqdHeight = (int) (reqdWidth/selectionAspectRatio);
            }

            // now we have everything we need: what area (transformedSelection) to crop 
            // out of the raw image, and what dimensions this cropped area should be
            // resized to:
            string filePath;
            int reqResolution = string.IsNullOrEmpty(ImageResolution) ? 300 : int.Parse(ImageResolution);
            webImageName = ImageProvider.CropAndScale(transformedSelection, webImageFormat, webImageQuality, reqdWidth,
                                                      reqdHeight, reqResolution, out filePath);
            targetImage.Src = getImageSource(webImageName, "web");
            targetImage.Visible = true;
            controlMode = ControlMode.Changed;

            // Check if the document already exists in dsl and session, if so then delete the first one
            if (!string.IsNullOrEmpty(DocumentID))
            {
                DslController.DeleteDocument(DocumentID);
            }
            // Upload the new document from the file system
            DocumentID = UploadToDsl(filePath);

            // bubble up the event to the UI
            InvokeUploadComplete(sender, new SelectedDocumentArgs(DocumentID));
        }

        private void cancelSelection_Click(object sender, EventArgs e)
        {
            controlMode = hasChanged ? ControlMode.Changed : ControlMode.Normal;
        }

        private void removeImage_Click(object sender, EventArgs e)
        {
            // Remove the image from DSL first - and then from the file system
            if (!string.IsNullOrEmpty(DocumentID))
            {
                var documentArgs = new SelectedDocumentArgs(DocumentID);
                DslController.DeleteDocument(DocumentID);
                DocumentID = string.Empty;
                InvokeRemoveComplete(sender, documentArgs);
            }
        }

        #endregion

        #region Event Bubbling

        private void InvokeUploadComplete(object sender, SelectedDocumentArgs args)
        {
            UploadCompleteEventHandler handler = UploadComplete;
            if (handler != null) handler(sender, args);
        }

        private void InvokeRemoveComplete(object sender, SelectedDocumentArgs args)
        {
            RemoveCompleteEventHandler handler = RemoveComplete;
            if (handler != null) handler(sender, args);
        }

        private void btnThumb_Command(object sender, CommandEventArgs e)
        {
            bool imageOK = ImageProvider.UseThumbnailFile(e.CommandArgument.ToString(), out serverImgID, out rawWidth,
                                                          out rawHeight);
            if (!imageOK)
            {
                return;
            }
            UseRaw();
        }

        #endregion

        #region Helper Methods

        private bool UseRaw()
        {
            if (intImageWidth == rawWidth && intImageHeight == rawHeight)
            {
                // the image is already the right size - we'll just save it as the web image,
                // taking the required format and quality into consideration                
                string fullPath;
                int resolution = string.IsNullOrEmpty(ImageResolution) ? 300 : int.Parse(ImageResolution);
                webImageName = ImageProvider.SaveRawImageAsWebImage(webImageFormat, webImageQuality, resolution,
                                                                    out fullPath);
                targetImage.Src = getImageSource(webImageName, "web");
                targetImage.Visible = true;
                controlMode = ControlMode.Changed;
                // check if document ID already exists in DSL - if so then delete
                if (!string.IsNullOrEmpty(DocumentID))
                {
                    DslController.DeleteDocument(DocumentID);
                }
                DocumentID = UploadToDsl(fullPath);
                return true;
            }
            CanvasFromRaw();
            return false;
        }

        private void CanvasFromRaw()
        {
            // we're going to need to do some further work with this image so let's work 
            // out a few things about it
            aspectRatio = rawWidth/(float) rawHeight;

            // the image is not the right size so we need to make a canvas
            string[] clientDimensions = hiddenField.Value.Split(new[] {','});
            // we'll allow the canvas to be up to 80% of the user's current browser window size:
            int clientX = Convert.ToInt32(clientDimensions[0])*4/5;
            int clientY = Convert.ToInt32(clientDimensions[1])*4/5;

            float clientRatio = clientX/(float) clientY;

            // which is the dimension that should constrain the canvas size?
            if (clientRatio > aspectRatio)
            {
                // y axis constrains the canvas size
                canvasHeight = clientY;
                canvasWidth = rawWidth*clientY/rawHeight;
            }
            else
            {
                canvasWidth = clientX;
                canvasHeight = rawHeight*clientX/rawWidth;
            }

            canvasImageName = ImageProvider.CreateCanvas(webImageFormat, webImageQuality, canvasWidth, canvasHeight);
            canvas.Src = getImageSource(canvasImageName, "canvas");
            canvas.Width = canvasWidth;
            canvas.Height = canvasHeight;
            controlMode = ControlMode.Canvas;
        }

        private string UploadToDsl(string filePath)
        {
            // Ensure that the filename and type have been set
            // This may be called from an image session also where the document already exists.
            if (!string.IsNullOrEmpty(FileName) || !string.IsNullOrEmpty(FileType))
            {
                // Open a File Stream and Save into the DSL Library!
                var fileStream = new FileStream(filePath, FileMode.Open);
                DocumentID = DslController.UploadDslDocument(DocumentCategory, fileStream, (int) fileStream.Length,
                                                             FileName, FileType, Membership.GetUser().UserName,
                                                             ReferenceData, ConfigSettingReader.ApplicationName,
                                                             ConfigSettingReader.ClientCode);
            }
            return DocumentID;
        }

        private string getImageSource(string imageName, string mode)
        {
            String queryString = "?mode=" + mode + "&img=" + imageName;
            if (String.IsNullOrEmpty(handlerPath))
            {
                // put a unique identifier into the querystring args to avoid mixing
                // our params with someone else's
                queryString = queryString.Replace("=", KeySuffix + "=");
                return
                    HttpContext.Current.Request.FilePath.Substring(
                        HttpContext.Current.Request.FilePath.LastIndexOf("/") + 1) + queryString;
            }
            else
            {
                return Page.ResolveUrl(HandlerPath) + queryString;
            }
        }

        private ImageButton getThumbnailButton(string thumbnailFilename)
        {
            var btnThumb = new ImageButton();
            btnThumb.ImageUrl = getImageSource(thumbnailFilename, "thumbnail");
            btnThumb.CommandName = "ThumbnailSelect";
            btnThumb.CommandArgument = thumbnailFilename;
            btnThumb.OnClientClick = "setViewportDimensions('" + hiddenField.ClientID + "')";
            return btnThumb;
        }

        #endregion

        public event UploadCompleteEventHandler UploadComplete;
        public event RemoveCompleteEventHandler RemoveComplete;
    }

    public enum ControlMode
    {
        Normal,
        Canvas,
        Changed
    }

    public enum WebImageFormat
    {
        Gif,
        Jpg,
        Png
    }

    public enum WebImageQuality
    {
        High,
        Medium,
        Low
    }
}