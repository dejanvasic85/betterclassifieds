namespace Paramount.Common.UI
{
    using System;
    using BaseControls;
    using Telerik.Web.UI;
    using Common.UIController;

    public abstract class ParamountGrid : ParamountCompositeControl
    {
        private readonly RadGrid _baseGrid;
        public RadGrid Grid
        {
            get
            {
                return _baseGrid;
            }
        }

        protected ParamountGrid(string gridId)
        {
            _baseGrid = new RadGrid { ID = gridId };
            _baseGrid.Init += BaseGridPreRender;
            _baseGrid.NeedDataSource += BaseGridNeedDataSource;
            _baseGrid.ItemCommand += BaseGridItemCommand;
            _baseGrid.ItemDataBound += BaseGridItemDataBound;
        }

        protected virtual void GridItemBound(object sender, GridItemEventArgs e)
        {

        }

        protected virtual void GridItemCommand(object source, GridCommandEventArgs e)
        {

        }

        void BaseGridItemDataBound(object sender, GridItemEventArgs e)
        {
            GridItemBound(sender, e);
        }


        void BaseGridItemCommand(object source, GridCommandEventArgs e)
        {
            GridItemCommand(source, e);
        }

        protected ParamountGrid()
            : this("mygrid1")
        {

        }

        void BaseGridNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            NeedDataSource(true);
        }

        public abstract void NeedDataSource(bool isBind);

        protected abstract PagedSource GetDataSource();

        void BaseGridPreRender(object sender, EventArgs e)
        {
            Grid.AutoGenerateColumns = false;
            AddGridSettings(Grid);
            InsertColumns(Grid.MasterTableView.Columns);
        }

        public abstract void InsertColumns(GridColumnCollection columnCollection);

        protected virtual void AddGridSettings(RadGrid grid)
        {

        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            Controls.Add(_baseGrid);
        }
    }
}
