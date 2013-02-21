namespace Paramount.Betterclassified.Utilities.HttpModules
{
    using System;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class ApplicationInformationModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.PreRequestHandlerExecute += delegate
                                                    {
                                                        var page = context.Context.Handler as Page;
                                                        if (page == null)
                                                        {
                                                            return;
                                                        }
                                                        page.Load += delegate
                                                                         {
                                                                             if (page.Header == null)
                                                                             {
                                                                                 return;
                                                                             }
                                                                             try
                                                                             {
                                                                                 var ajaxWebServiceUrl = page.ResolveClientUrl("~/Service/AjaxMethods.asmx");
                                                                                 var info = page.GetType().BaseType.Assembly.GetName();
                                                                                 var scriptStringBuilder = new System.Text.StringBuilder();

                                                                                 scriptStringBuilder.AppendFormat(
                                                                                     "<script type=\"text/javascript\"> window.defaultStatus ='Paramount IT: {0} {1}'; </script> ", info.Name, info.Version);
                                                                                 string meta =
                                                                                    string.Format("<meta name=\"Product\" content=\"{0} {1}\" />", info.Name, info.Version);
                                                                                 scriptStringBuilder.AppendLine();
                                                                                 scriptStringBuilder.AppendLine("<script type=\"text/javascript\">");
                                                                                 scriptStringBuilder.AppendFormat("var ajaxWebServiceUrl = '{0}';", ajaxWebServiceUrl);
                                                                                 scriptStringBuilder.AppendLine("</script>");

                                                                                 page.Header.Controls.Add(new Literal { Text = scriptStringBuilder.ToString() });
                                                                                 page.Header.Controls.Add(new Literal { Text = meta });
                                                                             }
                                                                             catch (Exception)
                                                                             {

                                                                             }
                                                                         };
                                                    };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}