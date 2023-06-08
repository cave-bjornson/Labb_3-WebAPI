using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Threenine.Data;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class InterestController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly ILogger<InterestController> _logger;

    private readonly InterestMapper _mapper;

    public InterestController(
        ILogger<InterestController> logger,
        IUnitOfWork unitOfWork,
        InterestMapper mapper
    )
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<InterestWithLinksDto> GetInterestAsync(int id)
    {
        var interest = await _unitOfWork
            .GetReadOnlyRepositoryAsync<Interest>()
            .SingleOrDefaultAsync(
                predicate: i => i.Id == id,
                include: inc => inc.Include(i => i.Person).Include(i => i.Links)
            );

        return _mapper.InterestToInterestWithLinksDto(interest); 
    }

    [HttpGet]
    public async Task<IEnumerable<InterestWithLinksDto>> GetInterestsAsync(int? personId = null)
    {
        Expression<Func<Interest, bool>>? predicate = personId is null
            ? null
            : interest => interest.PersonId == personId;

        var interests = await _unitOfWork
            .GetReadOnlyRepositoryAsync<Interest>()
            .GetListAsync(
                predicate: predicate,
                include: inc => inc.Include(i => i.Person).Include(i => i.Links)
            );
        return interests.Items.Select(i => _mapper.InterestToInterestWithLinksDto(i));
    }

    // [HttpPost]
    // public async Task<InterestDto> CreatePerson(CreatePersonDto dto)
    // {
    //     var newPerson = _mapper.CreatePersonDtoToPerson(dto);
    //     var created = await _unitOfWork.GetRepositoryAsync<Person>().InsertAsync(newPerson);
    //     await _unitOfWork.CommitAsync();
    //     return _mapper.PersonToPersonDto(created.Entity);
    // }
}
