using System;
using System.Net;
using System.Web;
using BetterClassified.UI.Views;
using Microsoft.Practices.Unity;

namespace BetterClassified.UI
{
    public abstract class BasePage<TPresenter, TView> : System.Web.UI.Page, IBaseView where TView : IBaseView
    {
        protected TPresenter Presenter;

        protected BasePage()
        {
            this.Init += (sender, args) => this.Presenter = Unity.DefaultContainer.Resolve<TPresenter>(With.Parameter("view", this));
        }

        public void PrintMessage()
        {
            // Find master and print the message
        }

        public void PrintError()
        {
            // Find master and print the error
        }

        public void NavigateToHome()
        {
            // Navigate to home page (iflog home page)
        }

        public string LoggedInUserName
        {
            get
            {
                if (HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
                    return HttpContext.Current.User.Identity.Name;
                return string.Empty;
            }
        }

        protected T ReadQueryString<T>(string key)
        {
            var value = Request.QueryString.Get(key);
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}