using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Threenine.Data;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly ILogger<PersonController> _logger;

    private readonly PersonMapper _mapper;

    public PersonController(
        ILogger<PersonController> logger,
        IUnitOfWork unitOfWork,
        PersonMapper mapper
    )
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    // [Route("/persons/{id:int}")]
    public async Task<PersonDto> GetPerson(int id)
    {
        var person = await _unitOfWork
            .GetReadOnlyRepositoryAsync<Person>()
            .SingleOrDefaultAsync(
                predicate: p => p.Id == id,
                include: inc => inc.Include(p => p.Interests).Include(p => p.Links)
            );

        return _mapper.PersonToPersonDto(person);
    }

    [HttpGet]
    public IEnumerable<PersonDto> GetPersons()
    {
        return _unitOfWork
            .GetReadOnlyRepository<Person>()
            .GetList(include: inc => inc.Include(p => p.Interests).Include(p => p.Links))
            .Items.Select(p => _mapper.PersonToPersonDto(p));
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
