namespace Paramount.Services.Host
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Net;
    using System.Web;
    using System.Web.Services;
    using System.Xml;
    using Common.DataTransferObjects.Broadcast.Messages;
    
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class LocationHandler : IHttpHandler
    {
        private const string IdKey = "id";
        public string IpAddress { get; set; }
        public string ClientPage { get; set;}
        public string Browser { get; set; }
        public string EmailBroadcastEntryId { get; set; }
        private DataTable _dt = new DataTable();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Clear();
            context.Response.ContentType = "image/jpeg";
            var bmp = new Bitmap(context.Server.MapPath("~/Resources/spacer.gif"));
            bmp.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            bmp.Dispose();

            // Set EmailBroadcastEntry Id from query string 
            EmailBroadcastEntryId = context.Request.QueryString[IdKey];
            // Getting ip of visitor
            IpAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(IpAddress))
            IpAddress = context.Request.ServerVariables["REMOTE_ADDR"];

            // Getting the page which called the script
            ClientPage = context.Request.ServerVariables["HTTP_REFERER"];
            if (string.IsNullOrEmpty(ClientPage))
                ClientPage = context.Request.Path;

            // Getting Browser Name of Visitor
            if (context.Request.ServerVariables["HTTP_USER_AGENT"].Contains("MSIE"))
                Browser = "Internet Explorer";
            else
            if (context.Request.ServerVariables["HTTP_USER_AGENT"].Contains("FireFox"))
                Browser = "Fire Fox";
            else 
            if (context.Request.ServerVariables["HTTP_USER_AGENT"].Contains("Opera"))
                Browser = "Opera";

            if (string.IsNullOrEmpty(Browser))
            {
                Browser = context.Request.UserAgent;
            }
            

            _dt = GetLocation(IpAddress);

            if (_dt != null)
            {
                if (_dt.Rows.Count > 0)
                {
                    var proxy = new BroadcastService();
                    try
                    {
                        proxy.CreateEmailBroadcastTrack(new CreateEmailTrackRequest
                        {
                            Browser = this.Browser,
                            City = _dt.Rows[0]["City"].ToString(),
                            ClientPage = ClientPage,
                            Country = _dt.Rows[0]["CountryName"].ToString(),
                            EmailBroadcastEntryId = EmailBroadcastEntryId,
                            IpAddress = IpAddress,
                            Region = _dt.Rows[0]["RegionName"].ToString(),
                            Postcode = _dt.Rows[0]["ZipCode"].ToString(),
                            Latitude = _dt.Rows[0]["Latitude"].ToString(),
                            Longitude = _dt.Rows[0]["Longitude"].ToString(),
                            //TimeZone = _dt.Rows[0]["Timezone"].ToString()
                        });
                    }
                    catch (Exception)
                    {
                    }

                }
                else
                {
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private static DataTable GetLocation(string ipaddress)
        {
            //Create a WebRequest
            var rssReq =WebRequest.Create("http://freegeoip.appspot.com/xml/" + ipaddress);

            //Create a Proxy
            var px = new WebProxy("http://freegeoip.appspot.com/xml/"+ ipaddress, true);

            //Assign the proxy to the WebRequest
            rssReq.Proxy = px;

            //Set the timeout in Seconds for the WebRequest
            rssReq.Timeout = 2000;

            try
            {
                //Get the WebResponse 
                WebResponse rep = rssReq.GetResponse();

                //Read the Response in a XMLTextReader
                var xtr = new XmlTextReader(rep.GetResponseStream());

                //Create a new DataSet
                var ds = new DataSet();

                //Read the Response into the DataSet
                ds.ReadXml(xtr);
                return ds.Tables[0];

            }
            catch
            {
                return null;
            }

        }

    }
}
