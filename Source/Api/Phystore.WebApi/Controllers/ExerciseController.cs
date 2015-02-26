using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using Phystore.DAL.Entities;
using Phystore.WebApi.Controllers.Base;
using Phystore.WebApi.Models.Request;

namespace Phystore.WebApi.Controllers
{
  [RoutePrefix("api/exercise")]
  public class ExerciseController : BaseApiController
  {
    List<Exercise> _exercises = new List<Exercise>
      {
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Name = "Pushups", Description = "Pushups description", Category = "Breast" },
        new Exercise { Id = 44, Name = "LastPushUps", Description = "Pushups description", Category = "Breast" },
      };

    [Route("")]
    [HttpGet]
    public IHttpActionResult Get(int id)
    {
      return Ok(_exercises.FirstOrDefault(r => r.Id == id));
    }

    [Route("")]
    [HttpGet]
    public IHttpActionResult Get([FromUri]QueryExerciseRequest request)
    {
      _exercises = _exercises.Skip(request.Skip.GetValueOrDefault()).ToList();

      return Ok(request.Take.HasValue ? _exercises.Take(request.Take.Value) : _exercises);
    }
  }
}