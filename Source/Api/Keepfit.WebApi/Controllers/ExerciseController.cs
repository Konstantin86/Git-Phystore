using System.Linq;
using System.Web.Http;
using Keepfit.DAL;
using Keepfit.DAL.Entities;
using Keepfit.WebApi.Controllers.Base;
using Keepfit.WebApi.Models.Request;

namespace Keepfit.WebApi.Controllers
{
  [RoutePrefix("api/exercise")]
  public class ExerciseController : BaseApiController
  {
    private readonly IUnitOfWork _unitOfWork;

    public ExerciseController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    [Route("")]
    [HttpGet]
    public IHttpActionResult Get(int id)
    {
      var exercise = _unitOfWork.ExerciseRepository.Get(r => r.Id == id);
      return exercise != null ? Ok(exercise) : (IHttpActionResult)NotFound();
    }

    [Route("")]
    [HttpGet]
    public IHttpActionResult Get([FromUri]QueryExerciseRequest request)
    {
      var exercises = _unitOfWork.ExerciseRepository.GetAll().Skip(request.Skip.GetValueOrDefault()).ToList();
      return Ok(exercises.Any() ? (request.Take.HasValue ? exercises.Take(request.Take.Value) : exercises) : null);
    }

    [Route("")]
    [HttpPost]
    public IHttpActionResult Post(Exercise exercise)
    {
      int id = _unitOfWork.ExerciseRepository.Update(exercise);
      return Ok(id);
    }
  }
}