using ClubPOS.Core.Entities;

namespace ClubPOS.Infrastructure.IRepositories;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
    Task<int> Complete();
}