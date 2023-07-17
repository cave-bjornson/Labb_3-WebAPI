using Threenine.Data;
using WebAPI.Models;

namespace WebAPI.Services;

public class InterestService : BaseService<Interest>, IInterestService
{
    private readonly InterestMapper _mapper;

    /// <inheritdoc />
    public InterestService(IUnitOfWork unitOfWork, InterestMapper mapper)
        : base(unitOfWork)
    {
        _mapper = mapper;
    }

    public async Task<InterestWithNavigationDto> CreateInterestAsync(CreateInterestDto dto)
    {
        var newInterest = _mapper.CreateInterestDtoToInterest(dto);
        var created = await CreateAsync(newInterest);
        return _mapper.InterestToInterestWithNavigationDto(created);
    }
}
