using Threenine.Data;
using WebAPI.Models;

namespace WebAPI.Services;

public class LinkService : BaseService<Link>, ILinkService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly LinkMapper _mapper;
    
    /// <inheritdoc />
    public LinkService(IUnitOfWork unitOfWork, LinkMapper mapper)
        : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<LinkWithNavigationDto> CreateLinkAsync(CreateLinkDto dto)
    {
        var projection = await _unitOfWork.GetReadOnlyRepositoryAsync<Interest>()
            .SingleOrDefaultAsync(selector: i => new { i.PersonId }, predicate: i => i.Id == dto.InterestId);
        
        var newLink = _mapper.CreateLinkDtoToLink(dto);
        newLink.PersonId = projection.PersonId;
        var created = await CreateAsync(newLink);
        
        return _mapper.LinkToLinkWithNavigationDto(created);
    }
}
