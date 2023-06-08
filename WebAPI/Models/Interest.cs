using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebAPI.Models;

public class Interest
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? Id { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public int? PersonId { get; set; }
    public Person? Person { get; set; }

    public ICollection<Link>? Links { get; set; }
}

public class InterestDto
{
    [JsonPropertyOrder(int.MinValue)]
    public required int Id { get; set; }

    [JsonPropertyOrder(int.MinValue)]
    public required string Title { get; set; }

    [JsonPropertyOrder(int.MinValue)]
    public required string Description { get; set; }

    [JsonPropertyOrder(int.MinValue)]
    public required int PersonId { get; set; }
}

public class InterestWithLinksDto : InterestDto
{
    public LinkDto[] Links { get; set; }
}
