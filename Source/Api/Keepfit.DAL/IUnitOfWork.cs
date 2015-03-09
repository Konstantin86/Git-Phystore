using System;
using Keepfit.DAL.Repositories;

namespace Keepfit.DAL
{
  public interface IUnitOfWork : IDisposable
  {
    ExerciseRepository ExerciseRepository { get; }

    void SaveChanges();
  }
}