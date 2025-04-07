using Microsoft.EntityFrameworkCore;
using My_Movie.Domain.Model;
using My_Movie.Model;

namespace My_Movie
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
         : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserBooks> UserBooks { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RandomID> RandomIds { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserBooks>()
                .HasOne(ub => ub.User)
                .WithMany(u => u.UserBooks)
                .HasForeignKey(ub => ub.UserId);

            modelBuilder.Entity<UserBooks>()
                .HasOne(ub => ub.Book)
                .WithMany(b => b.UserBooks)
                .HasForeignKey(ub => ub.BookId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ub => ub.User)
                .WithMany(u => u.UserRole)
                .HasForeignKey(ub => ub.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ub => ub.Role)
                .WithMany(b => b.UserRole)
                .HasForeignKey(ub => ub.RoleId);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    LoginName = "admin",
                    Password = "123",
                    Name = "Admin",
                    lastLogged = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    user_id = "Admin_00123"
                }
            );
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name = "Administrator",
                    createAt = DateTime.UtcNow,
                    updatedAt = DateTime.UtcNow
                }
            );
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 2,
                    Name = "User",
                    createAt = DateTime.UtcNow,
                    updatedAt = DateTime.UtcNow
                }
            );
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole
                {
                    Id = 1,
                    RoleId = 1,
                    UserId = 1,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            );
        }
    }

}
