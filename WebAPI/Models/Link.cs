using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models;

public class Link
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
    public required int Id { get; set; }
    public required string Title { get; set; }
    public required string LinkURL { get; set; }
    public required int PersonId { get; set; }
    public required int InterestId { get; set; }
}
