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



        // this is the point we can configure Database. Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // we say to entity framework core ; we want to configure the Entity related to Order.
            modelBuilder.Entity<Order>()
                // we tell that each order is associated with a User.
                .HasOne(order => order.User)
                // we tell that a User can have multiple Orders.
                .WithMany(user => user.Orders)
                // we specify which property acts as the Foreign key.
                .HasForeignKey(Order => Order.UserId)
                // we tell this relationship is compulsory.
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasMany(user => user.Roles)
                .WithMany(role => role.Users);


        }
    }
}
