using System.Web.Http;

namespace Paramount.Betterclassifieds.Presentation.Api
{
    public class HealthController : ApiController
    {
        [Route("api/status")]
        public IHttpActionResult GetStatus()
        {
            return Ok(new {Status = "Ok"});
        }
    }
}
