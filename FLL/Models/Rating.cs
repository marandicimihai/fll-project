using System.ComponentModel.DataAnnotations;

namespace FLL.Models;

public class Rating
{
    public Guid RatingId { get; set; }
    public Guid UserId { get; set; }
    public Guid ExhibitId { get; set; }
    [Range(1, 5)] public double RatingValue { get; set; }
    public string? Message { get; set; }
}
