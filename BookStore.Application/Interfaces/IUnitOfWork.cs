using System;
using System.Threading.Tasks;

namespace BookStore.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task<int> CompleteAsync(); // دي اللي بتعمل SaveChanges
    }
}