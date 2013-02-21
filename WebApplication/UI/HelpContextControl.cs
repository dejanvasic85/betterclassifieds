namespace Paramount.Common.UI
{
    using BaseControls;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using AjaxControlToolkit;

    public class HelpContextControl : ParamountCompositeControl
    {
        protected Image image;
        protected HelpContentPanel contentPanel;

        public HelpContextControl()
        {
            image = new Image { ID = "helpImage", CssClass = "help-context-image" };
            contentPanel = new HelpContentPanel { ID = "contentPanel", CssClass = "help-content-panel" };
            //var t = new Label { Text = GetResource(EntityGroup.Admin, ContentItem.Homepage, "submit.Text") };
        }

        [UrlPropertyAttribute]
        public string ImageUrl { get; set; }

        [TemplateContainer(typeof(HelpContentPanel))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public ITemplate ContentTemplate { get; set; }

        public PopupControlPopupPosition Position { get; set; }

        protected override void CreateChildControls()
        {
            image.Style.Add("cursor", "pointer");
            //_popupControlExtender.PopupControlID = contentPanel.UniqueID;
            //_popupControlExtender.TargetControlID = image.UniqueID;
            //_popupControlExtender.Position = Position;
            image.ImageUrl = ImageUrl;
            if (ContentTemplate != null)
            {
                ContentTemplate.InstantiateIn(contentPanel);
            }

            var panel = new Panel { CssClass = "image-panel" };
            panel.Controls.Add(image);
            Controls.Add(panel);
            Controls.Add(contentPanel);
            //Controls.Add(_popupControlExtender);
        }

        protected class HelpContentPanel : Panel, INamingContainer
        {

        }

        protected override void OnPreRender(System.EventArgs e)
        {
            base.OnPreRender(e);
            Page.ClientScript.RegisterClientScriptResource(GetType(),
                       "Paramount.Common.UI.JavaScript.tooltip.min.js");
            Page.ClientScript.RegisterClientScriptResource(GetType(),
                       "Paramount.Common.UI.JavaScript.HelpContextControl.js");
            //image.Attributes.Add("onmouseout", string.Format("HideControl('{0}')", contentPanel.ClientID));
            //image.Attributes.Add("onmouseover", string.Format("DisplayControl('{0}')", contentPanel.ClientID));
        }

    }
}
