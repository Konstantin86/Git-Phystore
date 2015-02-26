using System;
using Phystore.DAL.Repositories;

namespace Phystore.DAL
{
  public interface IUnitOfWork : IDisposable
  {
    ExerciseRepository ExerciseRepository { get; }

    void SaveChanges();
  }
}