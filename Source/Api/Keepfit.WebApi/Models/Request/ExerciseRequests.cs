using Keepfit.WebApi.Models.Request.Base;

namespace Keepfit.WebApi.Models.Request
{
  public class ExerciseQueryRequest : QueryRequestBase
  {
    public string Name { get; set; }
  }
}