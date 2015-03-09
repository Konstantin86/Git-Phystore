using Keepfit.DAL.Entities;

namespace Keepfit.DAL.Repositories
{
  public class ExerciseRepository : RepositoryBase<Exercise>
  {
    public ExerciseRepository(AppDbContext context)
      : base(context)
    {
    }

    protected override void OnUpdate(Exercise entity, Exercise newEntity, AppDbContext context)
    {
      entity.Name = newEntity.Name;
      entity.Description = newEntity.Description;
      entity.Category = newEntity.Category;
    }
  }
}