namespace BetterClassified.UI
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Paramount.Common.UI.BaseControls;

    public class ActionButton : ParamountCompositeControl
    {
        public event EventHandler<ActionEventArgs> ActionClick;

        private void InvokeActionClick(ActionEventArgs e)
        {
            EventHandler<ActionEventArgs> handler = ActionClick;
            if (handler != null) handler(this, e);
        }

        private const string BackgroundCss = "background:transparent url({0}) no-repeat scroll left top;";

        private readonly LinkButton _markButton;
        private readonly LinkButton _deleteButton;
        private readonly LinkButton _addButton;
        private readonly LinkButton _editButton;

        public bool ShowMarkButton { get; set; }
        public bool ShowDeleteButton { get; set; }
        public bool ShowEditButton { get; set; }
        public bool ShowAddButton { get; set; }
        public string AddButtonToolTip
        {
            set { _addButton.ToolTip = value; }
        }
        public string DeleteButtonToolTip
        {
            set { _deleteButton.ToolTip = value; }
        }

        public string MarkButtonToolTip
        {
            set { _markButton.ToolTip = value; }
        }

        public int? Key
        {
            get { return ViewState["key"] as int?; }
            set { ViewState["key"] = value; }
        }



        public ActionButton()
        {
            this._markButton = new LinkButton {CssClass = "action-button", ID ="mark"};
            this._markButton.Click += MarkButtonClick;
            this._deleteButton = new LinkButton {CssClass = "action-button", ID="delete"};
            this._deleteButton.Click += DeleteButtonClick;
            this._addButton = new LinkButton {CssClass = "action-button"};
            this._addButton.Click += AddButtonClick;
            this._editButton = new LinkButton {CssClass = "action-button"};
            this._editButton.Click += EditButtonClick;
        }


        void EditButtonClick(object sender, EventArgs e)
        {
            InvokeActionClick(new ActionEventArgs { ButtonType = ActionButtonType.Edit, Key = this.Key });
        }

        void AddButtonClick(object sender, EventArgs e)
        {
            InvokeActionClick(new ActionEventArgs { ButtonType = ActionButtonType.Add, Key = this.Key });
        }

        void DeleteButtonClick(object sender, EventArgs e)
        {
            InvokeActionClick(new ActionEventArgs { ButtonType = ActionButtonType.Delete, Key = this.Key });
        }

        void MarkButtonClick(object sender, EventArgs e)
        {
            InvokeActionClick(new ActionEventArgs {ButtonType = ActionButtonType.Mark, Key = this.Key});
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            const string includeTemplate = "<link rel='stylesheet' text='text/css' href='{0}' />";
            string includeLocation = Page.ClientScript.GetWebResourceUrl(this.GetType(), "BetterClassified.UI.Resources.action-button.css");
            var include = new LiteralControl(String.Format(includeTemplate, includeLocation));
            Page.Header.Controls.Add(include);

            this._addButton.Attributes.Add("style", string.Format(BackgroundCss, GetAddImage()));
            this._deleteButton.Attributes.Add("style", string.Format(BackgroundCss, GetDeleteImage()));
            this._editButton.Attributes.Add("style", string.Format(BackgroundCss, GetEditImage()));
            this._markButton.Attributes.Add("style", string.Format(BackgroundCss, GetMarkImage()));
        }

        private string GetAddImage()
        {
            return Page.ClientScript.GetWebResourceUrl(this.GetType(), "BetterClassified.UI.Resources.action-add.gif");
        }

        private string GetDeleteImage()
        {
            return Page.ClientScript.GetWebResourceUrl(this.GetType(), "BetterClassified.UI.Resources.action-delete.gif");
        }

        private string GetEditImage()
        {
            return Page.ClientScript.GetWebResourceUrl(this.GetType(), "BetterClassified.UI.Resources.action-edit.gif");
        }

        private string GetMarkImage()
        {
            return Page.ClientScript.GetWebResourceUrl(this.GetType(), "BetterClassified.UI.Resources.action-mark.gif");
        }

        protected override void CreateChildControls()
        {
            if (this.ShowMarkButton)
            {
                this.Controls.Add(this._markButton);
            }

            if(this.ShowAddButton)
            {
                this.Controls.Add(this._addButton);
            }

            if(this.ShowDeleteButton)
            {
                this.Controls.Add(this._deleteButton);
            }

            if(this.ShowEditButton)
            {
                this.Controls.Add(this._editButton);
            }
        }

        public AttributeCollection Keys
        {
            get
            {
                return this.Attributes;
            }
        }
    }

    public class ActionEventArgs:EventArgs
    {
        public ActionButtonType ButtonType { get; set; }
        public int? Key { get; set; }
    }

    public enum ActionButtonType
    {
        Edit,
        Add,
        Mark,
        Delete
    }
}