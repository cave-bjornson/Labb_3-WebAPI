using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Threenine.Data;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class InterestController : ControllerBase
{
    private readonly IInterestService _service;

    private readonly IUnitOfWork _unitOfWork;

    private readonly ILogger<InterestController> _logger;

    private readonly InterestMapper _mapper;

    public InterestController(
        ILogger<InterestController> logger,
        IUnitOfWork unitOfWork,
        InterestMapper mapper,
        IInterestService service
    )
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<InterestWithNavigationDto> GetInterestAsync(int id)
    {
        var interest = await _unitOfWork
            .GetReadOnlyRepositoryAsync<Interest>()
            .SingleOrDefaultAsync(
                predicate: i => i.Id == id,
                include: inc => inc.Include(i => i.Person).Include(i => i.Links)
            );

        return _mapper.InterestToInterestWithNavigationDto(interest);
    }

    [HttpGet]
    public async Task<IEnumerable<InterestDto>> GetInterestsAsync(int? personId = null)
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
        return interests.Items.Select(i => _mapper.InterestToInterestDto(i));
    }

    [HttpPost]
    public async Task<InterestWithNavigationDto> CreateInterest(CreateInterestDto dto)
    {
        return await _service.CreateInterestAsync(dto);
    }
}
