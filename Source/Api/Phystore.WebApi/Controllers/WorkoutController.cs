using System.Web.Http;

using Phystore.WebApi.Controllers.Base;

namespace Phystore.WebApi.Controllers
{
  [RoutePrefix("api/workout")]
  public class WorkoutController : BaseApiController
  {
    [Authorize(Roles = "user")]
    [Route("")]
    public IHttpActionResult Get()
    {
      return Ok("Hello from workouts controller");
    }
  }
}