using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;

namespace Paramount.Betterclassifieds.Tests.Integration
{
    [TestFixture]
    public class MailGunIntegrationTest
    {
        [Test]
        public void Send()
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator =
                new HttpBasicAuthenticator("api",
                                            "key-518e9736ee87c705773eaa1a967778da");

            RestRequest request = new RestRequest();
            request.AddParameter("domain", "kandobay.com.au", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Excited User <mailgun@kandobay.com.au>");
            request.AddParameter("to", "dejan.vasic@paramountit.com.au");
            request.AddParameter("subject", "Hi there from mailgun");
            request.AddParameter("text", "Testing some Mailgun awesomness!");
            request.Method = Method.POST;
            client.Execute(request);
        }
    }

}
