using System;
using AutoMapper;
using Paramount.Betterclassifieds.Business.Search;

namespace Paramount.Betterclassifieds.Presentation
{
    public class PlainTextToHtml : ValueResolver<AdSearchResult, string>
    {
        protected override string ResolveCore(AdSearchResult source)
        {
            if (source == null)
                return string.Empty;

            if (source.HtmlText.HasValue())
            {
                return source.HtmlText.Replace("\n", "<br />");
            }

            if (source.Description.HasValue())
            {
                return source.Description.Replace(Environment.NewLine, "<br />");
            }

            return string.Empty;
        }
    }
}