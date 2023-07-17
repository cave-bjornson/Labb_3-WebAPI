using WebAPI.Models;

namespace WebAPI.Services;

public interface IPersonService
{
    Task<T?> GetPersonById<T>(int id)
        where T : class;

    IAsyncEnumerable<T?> GetAllPersons<T>(
        bool interests = false,
        bool links = false,
        int page = 1,
        int pageSize = 10
    )
        where T : class;

    Task<PersonWithInterestsDto> GetPersonWithTree(int id);
}
