using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FLL.Models;

namespace FLL.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<FLL.Models.Exhibit> Exhibit { get; set; } = default!;
        public DbSet<FLL.Models.Rating> Rating { get; set; } = default!;
        public DbSet<FLL.Models.User> User { get; set; } = default!;
    }
}
