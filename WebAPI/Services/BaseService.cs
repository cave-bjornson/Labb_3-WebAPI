using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Threenine.Data;
using WebAPI.Models;

namespace WebAPI.Services;

public abstract class BaseService<T> : IService<T>
    where T : class, Entity
{
    private readonly IUnitOfWork _unitOfWork;

    protected BaseService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<T> GetOne(
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null
    )
    {
        return await _unitOfWork
            .GetReadOnlyRepositoryAsync<T>()
            .SingleOrDefaultAsync(predicate: predicate, include: include);
    }

    /// <inheritdoc />
    public async Task<T> GetOneById(int id)
    {
        return await GetOne(predicate: arg => arg.Id == id);
    }

    public async Task<IEnumerable<T>> GetAllAsync(
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        int page = 1,
        int pageSize = 10
    )
    {
        var uowIndex = (page - 1);

        var entities = await _unitOfWork
            .GetReadOnlyRepositoryAsync<T>()
            .GetListAsync(predicate: predicate, include: include, index: uowIndex, size: pageSize);

        return entities.Items;
    }

    /// <inheritdoc />
    public async Task<T> CreateAsync(T t)
    {
        var created = await _unitOfWork.GetRepositoryAsync<T>().InsertAsync(t);
        await _unitOfWork.CommitAsync();
        return created.Entity;
    }
}
