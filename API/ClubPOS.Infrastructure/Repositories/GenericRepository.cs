using System.Linq.Expressions;
using ClubPOS.Core.Entities;
using ClubPOS.Infrastructure.Data;
using ClubPOS.Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ClubPOS.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly DataContext _context;
    public GenericRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> ListAllWhereAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().Where(predicate).ToListAsync();
    }

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public void AddRange(List<T> entities)
    {
        _context.Set<T>().AddRange(entities);
    }

    public void Update(T entity)
    {
        _context.Set<T>().Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
}