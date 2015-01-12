using System;
using System.Web;

namespace Paramount.Betterclassifieds.DataService.Managers
{
    using Business;
    public class CookiesManager : IClientIdentifierManager
    {
        private const string CookieName = "p_client_id";

        public string Identifier
        {
            get
            {
                if (HttpContext.Current.Request.Cookies[CookieName] == null)
                {
                    var id = Guid.NewGuid().ToString();

                    var idCookie = new HttpCookie(CookieName) {Expires = DateTime.Now.AddDays(2), Value = id};


                    if (HttpContext.Current.Request.Cookies[CookieName] == null)
                    {
                        HttpContext.Current.Response.Cookies.Add(idCookie);
                    }
                    return id;

                }
                else
                {
                    var idCookie = HttpContext.Current.Request.Cookies[CookieName];
                    idCookie.Expires = DateTime.Now.AddDays(1);
                    var id = idCookie.Value;
                    HttpContext.Current.Response.Cookies.Add(idCookie);
                    return id;
                }
            }
        }
    }
}