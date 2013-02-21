using System;
using System.Web.UI.WebControls;
using Paramount.ApplicationBlock.Configuration;
using Paramount.Common.UI.BaseControls;
using Paramount.DSL.UIController;
using Paramount.DSL.UIController.ViewObjects;
using Telerik.Web.UI;

namespace Paramount.DSL.UI
{
    public class FileUploadControl:ParamountCompositeControl
    {
        protected readonly CustomValidator _validator;
        protected readonly RadUpload _radUpload;
        public event EventHandler<UploadCompleteEventArgs> UploadComplete;
        protected HiddenField documentIdField;

        public FileUploadControl()
        {
            _radUpload = new RadUpload {
                CssClass = "fileUploadControl", 
                ID = "FileUploadControl", 
                MaxFileInputsCount = 1,
                InitialFileInputsCount = 1 ,
                ControlObjectsVisibility = ControlObjectsVisibility.None
            };
            _validator = new CustomValidator();
            _validator.ServerValidate += Validate;
            _validator.ValidationGroup = "DocumentUpload";
            documentIdField=new HiddenField(){ID= "documentIdField"};
            // _radUpload.Load += UploadButtonClick;
        }

        //public string DocumentId { get; set; }

        public string DocumentId
        {
            get
            {

                return this.documentIdField.Value;

            }
            set
            {
                documentIdField.Value = value.ToString();
            }
        }

        private void Validate(object source, ServerValidateEventArgs args)
        {
            if (_radUpload.InvalidFiles.Count > 0)
            {
                // Validation failed, the files they are attempting do not suit the DSL Category - display error message
                // todo (Web Content)
                _validator.ErrorMessage =
                    "One of the files submitted is not valid. Please review the accepted file types and size before submitting your image.";

                args.IsValid = false;
                return;
            }
            if (!this.IsFileValid())
            {
                args.IsValid = false;
                return;
            }
        }

        protected virtual bool IsFileValid()
        {
            return true;
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            Controls.Add(_validator);
            Controls.Add(_radUpload);
            Controls.Add(documentIdField);
        }

        void UploadButtonClick(object sender, EventArgs e)
        {
            if (Upload())
            {
                InvokeUploadComplete(sender, new UploadCompleteEventArgs(DocumentId));
            }
        }

        public bool Upload()
        {
            Page.Validate("DocumentUpload");
            if (!Page.IsValid)
            {
                return false;
            }

            if (_radUpload.UploadedFiles.Count == 0 && string.IsNullOrEmpty(this.DocumentId))
            {
                return false;
            }

            if (_radUpload.UploadedFiles.Count == 0)
            {
                return true;
            }

            string user = Context.User.Identity.Name;
           
            var file = _radUpload.UploadedFiles[0];

            // Save Document to the Dsl Repository
            DocumentId = DslController.UploadDslDocument(DslDocumentCategoryTypeView.BannerAd, file.InputStream, file.ContentLength,
                                                        file.GetName(), file.ContentType, user,
                                                        string.Empty, ConfigSettingReader.ApplicationName, ConfigSettingReader.ClientCode);

            return true;

        }

        private void InvokeUploadComplete(object sender, UploadCompleteEventArgs e)
        {
            EventHandler<UploadCompleteEventArgs> handler = UploadComplete;
            if (handler != null) handler(sender, e);
        }

    }
}