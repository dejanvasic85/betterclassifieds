using NUnit.Framework;
using Paramount.Betterclassifieds.Presentation;
using System;

namespace Paramount.Betterclassifieds.Tests.Utility
{
    [TestFixture]
    internal class AdTextTests
    {
        [Test]
        public void FromHtml_SetsProperties()
        {
            var htmlSample = "<script>alert('hello world')</script>" + Environment.NewLine + "Test only <strong>CMON</strong>";

            var adText = AdText.FromHtml(htmlSample);

            adText.HtmlText.IsEqualTo("<script>alert('hello world')</script>\r\nTest only <strong>CMON</strong>");
            adText.HtmlTextEncoded.IsEqualTo("&lt;script&gt;alert(&#39;hello world&#39;)&lt;/script&gt;<br />Test only &lt;strong&gt;CMON&lt;/strong&gt;");
            adText.Plaintext.IsEqualTo("alert('hello world') Test only CMON");

        }

        [Test]
        public void FromHtml_EmptyText_SetsProperties()
        {
            var adText = AdText.FromHtml(string.Empty);

            adText.HtmlText.IsEqualTo("");
            adText.HtmlTextEncoded.IsEqualTo("");
            adText.Plaintext.IsEqualTo("");

        }

        [Test]
        public void FromHtml_NullText_SetsProperties()
        {
            var adText = AdText.FromHtml(null);

            adText.HtmlText.IsEqualTo("");
            adText.HtmlTextEncoded.IsEqualTo("");
            adText.Plaintext.IsEqualTo("");

        }

        [Test]
        public void FromHtmlEncoded_SetsProperties()
        {
            var htmlSample = "&lt;script&gt;alert(&#39;hello world&#39;)&lt;/script&gt;<br />Test only &lt;strong&gt;CMON&lt;/strong&gt;";

            var adText = AdText.FromHtmlEncoded(htmlSample);

            adText.HtmlText.IsEqualTo("<script>alert('hello world')</script>\r\nTest only <strong>CMON</strong>");
            adText.HtmlTextEncoded.IsEqualTo("&lt;script&gt;alert(&#39;hello world&#39;)&lt;/script&gt;<br />Test only &lt;strong&gt;CMON&lt;/strong&gt;");
            adText.Plaintext.IsEqualTo("alert('hello world') Test only CMON");

        }

        [Test]
        public void FromHtmlEncoded_EmptyText_SetsProperties()
        {
            var adText = AdText.FromHtmlEncoded(string.Empty);

            adText.HtmlText.IsEqualTo("");
            adText.HtmlTextEncoded.IsEqualTo("");
            adText.Plaintext.IsEqualTo("");

        }


        [Test]
        public void FromHtmlEncoded_NullText_SetsProperties()
        {
            var adText = AdText.FromHtmlEncoded(null);

            adText.HtmlText.IsEqualTo("");
            adText.HtmlTextEncoded.IsEqualTo("");
            adText.Plaintext.IsEqualTo("");

        }
    }
}
