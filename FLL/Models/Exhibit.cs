using System.ComponentModel.DataAnnotations;

namespace FLL.Models;

public class Exhibit
{
    public Guid ExhibitId { get; set; }
    [Required]
    public string ExhibitName { get; set; } = default!;
    public string? ExhibitDescription { get; set; }
    [DataType(DataType.Url)]
    public string PhotoUrl1 { get; set; } = default!;
    [DataType(DataType.Url)]
    public string PhotoUrl2 { get; set; } = default!;
    public List<Rating> Ratings { get; set; } = default!;
}
