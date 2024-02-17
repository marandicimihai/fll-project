namespace FLL.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public List<Rating> Ratings { get; set; } = default!;
    }
}
