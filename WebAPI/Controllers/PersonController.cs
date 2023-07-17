using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Threenine.Data;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IPersonService _service;

    private readonly ILogger<PersonController> _logger;

    private readonly PersonMapper _mapper;

    public PersonController(
        ILogger<PersonController> logger,
        IUnitOfWork unitOfWork,
        PersonMapper mapper,
        IPersonService service
    )
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPerson(int id, bool tree)
    {
        if (!tree)
        {
            return Ok(await _service.GetPersonById<PersonWithNavigationDto>(id));
        }

        return Ok(await _service.GetPersonWithTree(id));
    }

    [HttpGet]
    public async IAsyncEnumerable<PersonWithNavigationDto?> GetPersonsAsync(
        bool interests = false,
        bool links = false,
        int page = 1,
        int pageSize = 10
    )
    {
        await foreach (
            var dto in _service.GetAllPersons<PersonWithNavigationDto>(
                interests,
                links,
                page,
                pageSize
            )
        )
        {
            yield return dto;
        }
    }

    [HttpPost]
    public async Task<PersonDto> CreatePerson(CreatePersonDto dto)
    {
        var newPerson = _mapper.CreatePersonDtoToPerson(dto);
        var created = await _unitOfWork.GetRepositoryAsync<Person>().InsertAsync(newPerson);
        await _unitOfWork.CommitAsync();
        return _mapper.PersonToPersonDto(created.Entity);
    }
}
