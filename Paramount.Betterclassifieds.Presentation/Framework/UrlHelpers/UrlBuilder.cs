using System;
using System.Collections.Generic;
using System.Monads;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

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
        public string RouteName { get; private set; }
        public bool Encode { get; private set; }

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
            Action = action;
            Controller = controller;
            return this;
        }

        public UrlBuilder WithFullUrl()
        {
            this.IsFullUrl = true;
            return this;
        }

        public UrlBuilder WithRouteName(string routeName)
        {
            RouteName = routeName;
            return this;
        }

        public UrlBuilder WithRouteValues(object routeValues)
        {
            RouteValues = routeValues;
            return this;
        }
        
        public UrlBuilder WithEncoding()
        {
            Encode = true;
            return this;
        }

        public string Build()
        {
            var protocol = UrlHelper.With(u => u.RequestContext)
                .With(r => r.HttpContext)
                .With(h => h.Request)
                .With(r => r.Url)
                .Return(u => u.Scheme, "http");

            if (RouteName.HasValue())
            {
                Path = IsFullUrl
                    ? UrlHelper.RouteUrl(this.RouteName, this.RouteValues, protocol)
                    : UrlHelper.RouteUrl(this.RouteName, this.RouteValues);
            }
            else
            {
                Path = IsFullUrl
                    ? UrlHelper.Action(this.Action, this.Controller, this.RouteValues, protocol)
                    : UrlHelper.Action(this.Action, this.Controller, this.RouteValues);
            }

            if (!Encode)
                return Path;

            return HttpUtility.HtmlEncode(Path);
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
        
        /// <summary>
        /// Converts the required Url information to a redirect result
        /// </summary>
        public RedirectToRouteResult ToRedirectResult(IDictionary<string, object> parameters)
        {
            Guard.NotNull(Controller, "You must set the controller first");
            Guard.NotNull(Action, "You must set the action fisrt");

            var routeValueDictionary = new RouteValueDictionary
            {
                { "controller", Controller },
                { "action", Action },
            };

            if (parameters != null && parameters.Count > 0)
            {
                foreach (var parameter in parameters)
                {
                    routeValueDictionary.AddOrUpdate(parameter.Key, parameter.Value);
                }
            }

            return new RedirectToRouteResult(routeValueDictionary);
        }

        public RedirectToRouteResult ToRedirectResult()
        {
            return new RedirectToRouteResult(null);
        }
    }
}