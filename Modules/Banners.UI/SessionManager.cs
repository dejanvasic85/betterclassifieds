using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace Paramount.Banners.UI
{
    public static class SessionManager
    {
        public static string GroupId
        {
            get
            {
                return GetFromSession("GroupId").ToString();
            }
            set
            {
                AddToSession("GroupId", value);
            }
        }

        public static NameValueCollection BannerTags
        {
            get { return (NameValueCollection)GetFromSession("BannerTags"); }
            set
            {
                AddToSession("BannerTags" , value);
            }
        }

        //public static int? BannerId
        //{
        //    get
        //    {
        //        return GetIntFromSession("BannerId");
        //    }
        //    set
        //    {
        //        AddToSession("BannerId", value);
        //    }
        //}

        public static void AddToSession(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }

        public static object GetFromSession(string key)
        {
            return HttpContext.Current.Session[key];
        }


        public static int? GetIntFromSession(string key)
        {
            var result = GetFromSession(key);
            int? intVal = null;
            if (result != null)
            {
                int temp;
                if (int.TryParse(result.ToString(), out temp))
                {
                    intVal = temp;
                }
            }
            return intVal;
        }
    }
}
