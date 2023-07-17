using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Primitives;

namespace WebAPI.Models;

public class Person : Entity
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
    [JsonPropertyOrder(int.MinValue)]
    public required int Id { get; set; }

    [JsonPropertyOrder(int.MinValue)]
    public required string Name { get; set; }

    [JsonPropertyOrder(int.MinValue)]
    public required string PhoneNumber { get; set; }
}

public class PersonWithNavigationDto : PersonDto
{
    public InterestDto[]? Interests { get; set; }

    public LinkDto[]? Links { get; set; }
}

public class PersonWithInterestsDto : PersonDto
{
    public InterestWithLinksDto[]? Interests { get; set; }
}

public class CreatePersonDto
{
    public required string Name { get; set; }
    public required string PhoneNumber { get; set; }
}
