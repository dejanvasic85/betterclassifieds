﻿using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace Paramount.Betterclassifieds
{
    public class ClientViewEngine : RazorViewEngine
    {
        // Constructor chaining ( default as if it's a normal application - non module)
        public ClientViewEngine()
            : this("~")
        { }

        public ClientViewEngine(string baseUrl)
        {
            // Set default view location format
            ViewLocationFormats = new[]
            {
               GetViewPath(baseUrl, "Views/{1}/{0}"),
               GetViewPath(baseUrl, "Views/Shared/{1}/{0}"),
               GetViewPath(baseUrl, "Views/Shared/{0}")
            };

            // Set default master location formats
            MasterLocationFormats = new[]
            {
                GetViewPath(baseUrl, "Views/Shared{0}"),
                GetViewPath(baseUrl, "Views/{0}")
            };

            AddViewPath(baseUrl);
        }

        public void AddViewPath(string baseUrl)
        {
            if (baseUrl.HasValue())
            {
                this.ViewLocationFormats = this.ViewLocationFormats.Select(v => v.Replace("~/", string.Format("{0}/", baseUrl))).ToArray();
                this.MasterLocationFormats = this.MasterLocationFormats.Select(v => v.Replace("~/", string.Format("{0}/", baseUrl))).ToArray();
            }

            // Setup client specific view conventions
            var brand = ConfigurationManager.AppSettings["Brand"];
            if (brand.IsNullOrEmpty())
                throw new ConfigurationErrorsException("brand must be set as an Application Setting");

            if (!string.IsNullOrEmpty(brand))
            {
                this.ViewLocationFormats = new[]
                {
                    GetBrandViewPath(baseUrl, brand,"Views/{1}/{0}"),
                    GetBrandViewPath(baseUrl, brand,"Views/Shared/{1}/{0}"),
                    GetBrandViewPath(baseUrl, brand, "Views/Shared/{0}")
                }.Union(this.ViewLocationFormats).ToArray();

                this.MasterLocationFormats = new[]
                {
                    GetBrandViewPath(baseUrl, brand,"Views/Shared/{0}"),
                    GetBrandViewPath(baseUrl, brand,"Views/{0}")
                }
                .Union(this.MasterLocationFormats)
                .ToArray();
            }

            // Partial formats are just same as view formats
            PartialViewLocationFormats = this.ViewLocationFormats;
            
        }


        #region Private Methods
        string GetBrandViewPath(string baseUrl, string brand, string viewpath)
        {
            return string.Format("{0}/{1}.{2}.cshtml", baseUrl, viewpath, brand);
        }

        string GetViewPath(string baseUrl, string viewpath)
        {
            return string.Format("{0}/{1}.cshtml", baseUrl, viewpath);
        }

        #endregion
    }
}
