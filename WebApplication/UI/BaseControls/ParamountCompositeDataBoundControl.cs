namespace Paramount.Common.UI.BaseControls
{
    using System.Collections;
    using System.Web.UI.WebControls;
    using WebContent;

    public abstract class ParamountCompositeDataBoundControl : CompositeDataBoundControl
    {
        protected abstract override int CreateChildControls(IEnumerable dataSource, bool dataBinding);
        public string Domain { get; set; }
        public string GetResource(EntityGroup group, ContentItem item, string fieldId)
        {
            return WebContentManager.GetResource(group, item, fieldId);
        }
    }
}
