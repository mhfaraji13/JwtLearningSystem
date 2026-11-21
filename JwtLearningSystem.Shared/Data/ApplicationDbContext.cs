using JwtLearningSystem.Shared.Constants;
using JwtLearningSystem.Shared.Entities;
using Microsoft.EntityFrameworkCore;


namespace JwtLearningSystem.Shared.Data
{
    public class ApplicationDbContext : DbContext
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

            modelBuilder.Entity<User>()
                .HasIndex(user => user.NormalizedEmail)
                .IsUnique();

            modelBuilder.Entity<Role>()
                .HasIndex(role => role.NormalizedName)
                .IsUnique();

            modelBuilder.Entity<Order>()
                .HasIndex(order => order.OrderNumber)
                .IsUnique();

            modelBuilder.Entity<Order>()
                .HasOne(order => order.User)
                .WithMany(user => user.Orders)
                .HasForeignKey(order => order.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(user => user.FirstName)
                .HasMaxLength(25);

                entity.Property(user => user.LastName)
                .HasMaxLength(25);

                entity.Property(user => user.Email)
                .HasMaxLength(70);

                entity.Property(user => user.NormalizedEmail)
                .HasMaxLength(70);

                entity.Property(user => user.PasswordHash)
                .IsRequired();

                entity.Property(user => user.PasswordSalt)
                .IsRequired();
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(role => role.Name)
                .HasMaxLength(50)
                .IsRequired();

                entity.Property(role => role.NormalizedName)
                .HasMaxLength(50)
                .IsRequired();
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(order => order.TotalAmount)
                .HasPrecision(18, 2);
            });

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = AppRoles.Admin,
                    NormalizedName = AppRoles.Admin.ToUpperInvariant(),
                    Description = "System Administrator",
                    CreatedAt = new DateTimeOffset(
            2026, 1, 1, 0, 0, 0, TimeSpan.Zero)

                },
                new Role
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = AppRoles.Customer,
                    NormalizedName = AppRoles.Customer.ToUpperInvariant(),
                    Description = "Regular Customer",
                    CreatedAt = new DateTimeOffset(
            2026, 1, 1, 0, 0, 0, TimeSpan.Zero)
                },
                new Role
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Name = AppRoles.Support,
                    NormalizedName = AppRoles.Support.ToUpperInvariant(),
                    Description = "Support Agent",
                    CreatedAt = new DateTimeOffset(
            2026, 1, 1, 0, 0, 0, TimeSpan.Zero)
                }
                );

        }
    }
}
