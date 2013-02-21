using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using Paramount.Common.DataTransferObjects;
using Paramount.Common.DataTransferObjects.Banner;
using Paramount.Common.DataTransferObjects.Banner.Messages;

namespace Paramount.Services.Host
{
    /// <summary>
    /// Summary description for Handler1
    /// </summary>
    public class Handler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
            var service = new BannerService();
           // var request = new GetNextBannerAdRequest();
            //request.GroupId = new Guid("35BD98C7-621C-4E15-8DFA-3997AFDC06B6");
            //request.Parameters = new Collection<CodeDescription>();
            //request.Parameters.Add(new CodeDescription {Code = "Category", Description = "1"});
            //service.GetNextBannerAd(request);

            var response = service.LogBannerAudit(new LogBannerAuditRequest
                                                      {
                                                          ApplicationName = "Abc",
                                                          Audit = new BannerAuditEntity
                                                                      {
                                                                          ActionTypeName = "render",
                                                                          ApplicationName = "spress",
                                                                          BannerId = "dd8b2cf0-c047-4df1-8714-5e42529e76fd",
                                                                          ClientCode = "P000000005",
                                                                          Pageurl = "http:yahoo.com",

                                                                      },
                                                          ClientCode = "P000000005",
                                                          AuditData = new AuditData
                                                                          {
                                                                              BrowserType = "ie",
                                                                              ClientIpAddress = "121221",
                                                                              GroupingId = Guid.NewGuid().ToString(),
                                                                              HostName = "nec",

                                                                          },

                                                      });
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}