using WebAPI.Models;

namespace WebAPI.Services;

public interface IInterestService
{
    public Task<InterestWithNavigationDto> CreateInterestAsync(CreateInterestDto dto);
}
