using System.Linq.Expressions;
using ClubPOS.Core.Entities;

namespace ClubPOS.Infrastructure.IRepositories;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<IReadOnlyList<T>> ListAllWhereAsync(Expression<Func<T, bool>> predicate);
    void Add(T entity);
    void AddRange(List<T> entities);
    void Update(T entity);
    void Delete(T entity);
}