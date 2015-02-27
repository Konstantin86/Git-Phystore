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
      return exercises.Any() ? Ok(request.Take.HasValue ? exercises.Take(request.Take.Value) : exercises) : (IHttpActionResult)NotFound();
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