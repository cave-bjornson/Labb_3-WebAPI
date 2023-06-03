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

    [HttpGet(Name = "GetPersons")]
    [Route("/person")]
    public IEnumerable<PersonDto> GetPersons()
    {
        return _unitOfWork
            .GetReadOnlyRepository<Person>()
            .GetList(include: inc => inc.Include(p => p.Interests).Include(p => p.Links))
            .Items.Select(p => _mapper.PersonToPersonDto(p));
    }
}
