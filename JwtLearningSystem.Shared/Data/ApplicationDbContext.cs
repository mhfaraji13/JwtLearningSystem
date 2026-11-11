using JwtLearningSystem.Shared.Entities;
using Microsoft.EntityFrameworkCore;


namespace JwtLearningSystem.Shared.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
