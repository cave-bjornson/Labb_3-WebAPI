using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.Primitives;

namespace WebAPI.Models;

public class Person
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? Id { get; set; }

    public required string Name { get; set; }

    public required string PhoneNumber { get; set; }

    public ICollection<Interest>? Interests { get; set; }

    public ICollection<Link>? Links { get; set; }
}

public class PersonDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string PhoneNumber { get; set; }

    public InterestDto[] Interests { get; set; }

    public LinkDto[] Links { get; set; }
}

public class CreatePersonDto
{
    public required string Name { get; set; }
    public required string PhoneNumber { get; set; }
}
