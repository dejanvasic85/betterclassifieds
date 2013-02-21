using System;
using System.Globalization;
using Paramount.Banners.UIController;
using Paramount.Common.UI.BaseControls;
using Telerik.Web.UI;

namespace Paramount.Banners.UI.BannerGroup
{
    public class Groups : ParamountCompositeControl
    {
        private readonly RadGrid grid;
        public event EventHandler EditGroup;

        public void InvokeEditGroup(EventArgs e)
        {
            EventHandler handler = EditGroup;
            if (handler != null) handler(this, e);
        }

        public Groups()
        {
            grid = new RadGrid() { ID = "grid", AutoGenerateColumns = false, AllowAutomaticUpdates = false};
            grid.MasterTableView.Columns.Add(new GridBoundColumn() { DataField = "Title", HeaderText = "Title"});
            grid.MasterTableView.Columns.Add(new GridBoundColumn() { DataField = "BannerHeight", HeaderText = "Banner Height" });
            grid.MasterTableView.Columns.Add(new GridBoundColumn() { DataField = "BannerWidth", HeaderText = "Banner Width" });
            grid.MasterTableView.Columns.Add(new GridCheckBoxColumn() { DataField = "IncludeTimer", HeaderText = "Include Timer" });
            grid.MasterTableView.Columns.Add(new GridButtonColumn() { ButtonType = GridButtonColumnType.LinkButton, CommandName = "EditBanner", Text = "Edit"});
            grid.MasterTableView.Columns.Add(new GridButtonColumn() { ButtonType = GridButtonColumnType.LinkButton, CommandName = "Delete", Text = "Delete" });
            grid.MasterTableView.DataKeyNames = new[] { "GroupId" };
            grid.ItemCommand += ItemCommand;
        }

        private void ItemCommand(object source, GridCommandEventArgs e)
        {
            Guid groupId;
            if (e.CommandName == "EditBanner")
            {

                try
                {
                    groupId = new Guid(grid.MasterTableView.DataKeyValues[e.Item.ItemIndex]["GroupId"].ToString());
                }
                catch
                {
                    return;
                }

                SessionManager.GroupId = groupId.ToString();
                InvokeEditGroup(EventArgs.Empty);
            }
            else if (e.CommandName=="Delete")
            {

                BannerController.DeleteBanner(new Guid(grid.MasterTableView.DataKeyValues[e.Item.ItemIndex]["GroupId"].ToString()));
            }
        }


        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            this.Controls.Add(grid);
        }

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);
            var groups = BannerController.GetBannerGroups();
            this.grid.DataSource = groups;
            this.grid.DataBind();
        }
    }
}
