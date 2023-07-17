using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebAPI.Models;

public class Link : Entity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? Id { get; set; }

    public required string Title { get; set; }

    public required Uri LinkURL { get; set; }

    public int? PersonId { get; set; }
    public Person? Person { get; set; }

    public int? InterestId { get; set; }
    public Interest? Interest { get; set; }
}

public class LinkDto
{
    [JsonPropertyOrder(int.MinValue)]
    public required int Id { get; set; }

    [JsonPropertyOrder(int.MinValue)]
    public required string Title { get; set; }

    [JsonPropertyOrder(int.MinValue)]
    public required string LinkURL { get; set; }
}

public class LinkWithNavigationDto : LinkDto
{
    public PersonDto Person { get; set; }
    public InterestDto Interest { get; set; }
}

public class CreateLinkDto
{
    public required string Title { get; set; }
    public required string LinkURL { get; set; }
    public required int InterestId { get; set; }
}
