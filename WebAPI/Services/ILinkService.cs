using WebAPI.Models;

namespace WebAPI.Services;

public interface ILinkService
{
    public Task<LinkWithNavigationDto> CreateLinkAsync(CreateLinkDto dto);
}
