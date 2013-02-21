namespace BetterClassified.UI
{
    using BaseControl;
    using Paramount.Common.UI.BaseControls;

    public class FrameAutoSizeControl:ParamountCompositeControl
    {
        protected override void OnPreRender(System.EventArgs e)
        {
            base.OnPreRender(e);
            Page.ClientScript.RegisterClientScriptResource(this.GetType(),
                                                           "BetterClassified.UI.JavaScript.auto-resize-frame.js");

            
            
        }
    }
}
