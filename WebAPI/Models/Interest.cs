using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    public required int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required int PersonId { get; set; }
}
