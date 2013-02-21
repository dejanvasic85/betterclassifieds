namespace BetterClassified.UI
{
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using AjaxControlToolkit;
    using BetterclassifiedsCore;
    using Paramount.Common.UI;
    using Paramount.Common.UI.BaseControls;

    public class AdSearchBox : ParamountCompositeControl
    {
        private readonly TextBox _searchKeywordBox;
        private readonly TextBox _adIdBox;
        private readonly DropDownList _categoryList;
        private readonly DropDownList _subCategoryList;
        private readonly DropDownList _locationList;
        private readonly DropDownList _areaList;
        private readonly Button _searchButton;


        private readonly TextBoxWatermarkExtender _keywordWatermark;


        //public event EventHandler OnRefresh;

        //public void InvokeOnRefresh(EventArgs e)
        //{
        //    EventHandler handler = OnRefresh;
        //    if (handler != null) handler(this, e);
        //}

        public AdSearchBox()
        {
            //_updatePanel = new UpdatePanel {RenderMode = UpdatePanelRenderMode.Inline};
            _searchKeywordBox = new TextBox {ID ="_searchKeywordBox", CssClass = "full"};
            _searchButton = new Button { ID = "searchButton", CssClass = "btn btn-custom radius fr", Text=@"Search >>" };
            _adIdBox = new TextBox{ ID= "adbox"};
            //_updatePanel.UpdateMode = UpdatePanelUpdateMode.Conditional;
            _subCategoryList = new DropDownList
                                   {
                                       CssClass = "full",
                                       ID = "sub",
                                       DataTextField = "Title",
                                       DataValueField = "MainCategoryId"
                                   };

            _locationList = new DropDownList
                                {
                                    CssClass = "full",
                                    ID = "location",
                                    DataTextField = "Title",
                                    DataValueField = "LocationId",
                                    AutoPostBack = true
                                };

            _categoryList = new DropDownList
                                {
                                    CssClass = "full",
                                    ID = "cat",
                                    DataTextField = "Title",
                                    DataValueField = "MainCategoryId",
                                    AutoPostBack= true
                                };

             _areaList = new DropDownList { CssClass = "full", ID="area", AutoPostBack = true, DataTextField = "Title",DataValueField = "LocationAreaId" };
            _adIdBox = new TextBox { CssClass = "hasDatepick", ID = "adbox" };
            //_keywordWatermark = new TextBoxWatermarkExtender{ WatermarkText ="search keywords..."};

            _categoryList.SelectedIndexChanged += CategoryListSelectedIndexChanged;
            _locationList.SelectedIndexChanged += LocationListSelectedIndexChanged;
        }

        void LocationListSelectedIndexChanged(object sender, EventArgs e)
        {
            LoadArea();
        }

        void CategoryListSelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSubCategory();
        }
        
        void LoadCategory()
        {
            _categoryList.DataSource = CategoryController.GetMainParentCategories();
            _categoryList.DataBind();  
            _categoryList.Items.Insert( 0,new ListItem("All Category", "#"));
        }

        void LoadLocation()
        {
            _locationList.DataSource = GeneralController.GetLocations();
            _locationList.DataBind();
            _locationList.Items.Insert(0, new ListItem("All Location", "#"));
        }

        void LoadArea()
        {
            int locationId;
            int.TryParse(_locationList.SelectedValue, out locationId);
            _areaList.DataSource = GeneralController.GetLocationAreas(locationId);
            _areaList.DataBind();
            _areaList.Items.Insert(0, new ListItem("All Area", "#"));
        }

        void LoadSubCategory()
        {
            int categoryId;
            int.TryParse(_categoryList.SelectedValue,out categoryId);
            _subCategoryList.DataSource = CategoryController.GetMainCategoriesByParent(categoryId);
            _subCategoryList.DataBind();
            _subCategoryList.Items.Insert(0, new ListItem("All Sub-Category", "#"));
        }
    
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
           // if (!this.Page.IsPostBack)
            {
                LoadCategory();
                LoadSubCategory();
                LoadLocation();
                LoadArea();
            }
  

            var panel = new ParamountHtmlGenericControl("fieldset"){ CssClass="radius"};
            //_keywordWatermark.TargetControlID = _searchKeywordBox.ClientID;
            panel.Controls.Add(GetControlItem("Search Keyword",_searchKeywordBox));
           // panel.Controls.Add(_keywordWatermark);
            panel.Controls.Add(GetControlItem("Category",_categoryList));
            panel.Controls.Add(GetControlItem("Sub Category",_subCategoryList));
            panel.Controls.Add(GetControlItem("Location",_locationList));
            panel.Controls.Add(GetControlItem("Area", _areaList));

            panel.Controls.Add(GetControlItem("", _adIdBox));

            panel.Controls.Add(_searchButton);
            //_updatePanel.ContentTemplateContainer.Controls.Add(panel);
            Controls.Add(panel);
        }

        static Control GetControlItem(string labelText, Control control)
        {
            var p = new ParamountHtmlGenericControl("p") { CssClass = "full" };
            var label = new Label {AssociatedControlID = control.ClientID, Text = labelText};

            p.Controls.Add(label);
            p.Controls.Add(control);
            return p;
        }
    }
}