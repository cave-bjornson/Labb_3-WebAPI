using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Threenine.Data;
using WebAPI.Models;

namespace WebAPI.Services;

public class PersonService : BaseService<Person>, IPersonService
{
    private readonly PersonMapper _mapper;
    private readonly ILogger<PersonService> _logger;

    /// <inheritdoc />
    public PersonService(IUnitOfWork unitOfWork, PersonMapper mapper, ILogger<PersonService> logger)
        : base(unitOfWork)
    {
        _mapper = mapper;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<T?> GetPersonById<T>(int id)
        where T : class
    {
        var person = await GetOne(
            predicate: person => person.Id == id,
            include: GetIncludes(true, true)
        );

        return _mapper.PersonToPersonWithNavigationDto(person) as T;
    }

    public async IAsyncEnumerable<T?> GetAllPersons<T>(
        bool interests = false,
        bool links = false,
        int page = 1,
        int pageSize = 10
    )
        where T : class
    {
        var includes = GetIncludes(interests, links);

        var persons = await GetAllAsync(include: includes, page: page, pageSize: pageSize);

        foreach (var person in persons)
        {
            yield return _mapper.PersonToPersonWithNavigationDto(person) as T;
        }
    }

    /// <inheritdoc />
    public async Task<PersonWithInterestsDto> GetPersonWithTree(int id)
    {
        var person = await GetOne(
            predicate: person => person.Id == id,
            include: inc => inc.Include(p => p.Interests).ThenInclude(i => i.Links)
        );

        return _mapper.PersonToPersonWithInterestsDto(person);
    }

    private static Func<IQueryable<Person>, IIncludableQueryable<Person, object>?>? GetIncludes(
        bool interests = false,
        bool links = false
    )
    {
        if (interests || links)
        {
            return inc =>
            {
                if (interests)
                {
                    inc = inc.Include(person => person.Interests);
                }

                if (links)
                {
                    inc = inc.Include(person => person.Links);
                }

                return inc as IIncludableQueryable<Person, object>;
            };
        }

        return null;
    }
}
