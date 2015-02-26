using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Phystore.DAL;
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
        new Exercise { Id = 55, Name = "Pullups", Description = "Pullups description", Category = "Back" },
        new Exercise { Id = 53, Name = "Pullups", Description = "Pullups description", Category = "Back" },
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

    private IUnitOfWork _unitOfWork;

    public ExerciseController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

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

    [Route("")]
    [HttpPost]
    public IHttpActionResult Post(Exercise exercise)
    {
      if (exercise.Id > 0)
      {
        // TODO update
      }
      else
      {
        _unitOfWork.ExerciseRepository.Add(exercise);
        // TODO create
      }

      return Ok();
    }
  }
}