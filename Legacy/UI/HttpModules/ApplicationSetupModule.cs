namespace Paramount.Common.UI.HttpModules
{
    using System;
    using System.Web;
    using System.Web.UI;

    public class ApplicationSetupModule:IHttpModule
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
                        page.Header.Controls.Add(new ScriptsControl());
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