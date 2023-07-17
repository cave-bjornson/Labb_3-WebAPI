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
public class LinkController : ControllerBase
{
    private readonly ILinkService _service;
    
    private readonly IUnitOfWork _unitOfWork;

    private readonly ILogger<LinkController> _logger;

    private readonly LinkMapper _mapper;

    public LinkController(ILogger<LinkController> logger, IUnitOfWork unitOfWork, LinkMapper mapper, ILinkService service)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<LinkWithNavigationDto> GetLinksAsync(int id)
    {
        var link = await _unitOfWork
            .GetReadOnlyRepositoryAsync<Link>()
            .SingleOrDefaultAsync(
                predicate: l => l.Id == id,
                include: inc => inc.Include(l => l.Person).Include(l => l.Interest)
            );

        return _mapper.LinkToLinkWithNavigationDto(link);
    }

    [HttpGet]
    public async Task<IEnumerable<LinkDto>> GetLinksAsync(int? personId = null)
    {
        Expression<Func<Link, bool>>? predicate = personId is null
            ? null
            : link => link.PersonId == personId;

        var interests = await _unitOfWork
            .GetReadOnlyRepositoryAsync<Link>()
            .GetListAsync(
                predicate: predicate,
                include: inc => inc.Include(l => l.Person).Include(l => l.Interest)
            );
        return interests.Items.Select(l => _mapper.LinkToLinkDto(l));
    }

    [HttpPost]
    public async Task<LinkWithNavigationDto> CreateLinkAsync(CreateLinkDto dto)
    {
        return await _service.CreateLinkAsync(dto);
    }
}
