using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using WebAPI.Models;

namespace WebAPI.Services;

public interface IService<T>
    where T : Entity
{
    public Task<T> GetOne(
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
    );

    public Task<T> GetOneById(int id);

    public Task<IEnumerable<T>> GetAllAsync(
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        int page = 1,
        int pageSize = 10
    );
}
