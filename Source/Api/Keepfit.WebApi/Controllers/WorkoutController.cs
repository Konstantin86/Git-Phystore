using System.Web.Http;
using Keepfit.WebApi.Controllers.Base;

namespace Keepfit.WebApi.Controllers
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