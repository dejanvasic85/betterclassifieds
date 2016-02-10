using System.Monads;
using System.Web.Mvc;

namespace Paramount
{
    /// <summary>
    /// Allows the ability to build outgoing URL's for controller actions including full URI including schema and protocol
    /// </summary>
    public class UrlBuilder
    {
        public UrlHelper UrlHelper { get; private set; }
        public string Controller { get; private set; }
        public string Action { get; private set; }
        public object RouteValues { get; private set; }
        public string Path { get; private set; }
        public bool IsFullUrl { get; private set; }

        public UrlBuilder(UrlHelper urlHelper)
        {
            UrlHelper = urlHelper;
        }

        public UrlBuilder(UrlHelper urlHelper, string action, string controller, object routeValues = null)
        {
            UrlHelper = urlHelper;
            Controller = controller;
            Action = action;
            RouteValues = routeValues;
        }

        public UrlBuilder WithAction(string action, string controller)
        {
            this.Path = UrlHelper.Action(action, controller);
            return this;
        }

        public UrlBuilder WithFullUrl()
        {
            this.IsFullUrl = true;
            return this;
        }

        public string Build()
        {
            var protocol = UrlHelper.With(u => u.RequestContext)
                .With(r => r.HttpContext)
                .With(h => h.Request)
                .With(r => r.Url)
                .Return(u => u.Scheme, "http");

            this.Path = IsFullUrl
                ? UrlHelper.Action(this.Action, this.Controller, this.RouteValues, protocol)
                : UrlHelper.Action(this.Action, this.Controller, this.RouteValues);

            return this.Path;
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Path))
            {
                Path = Build();
            }
            return Path;
        }

        public static implicit operator string(UrlBuilder urlBuilder)
        {
            return urlBuilder.ToString();
        }

        public UrlBuilder WithContent(string relativeContentPath)
        {
            this.Path = UrlHelper.Content(relativeContentPath);
            return this;
        }
    }
}