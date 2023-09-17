using BookAppServer.Models;
using BookAppServer.Repositories.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace BookAppServer.Repositories
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserBook>()
                .HasIndex(ub => new { ub.UserId, ub.BookId })
                .IsUnique();
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }

        public DbSet<Author>? Authors { get; set; }
        public DbSet<Book>? Books { get; set; }
        public DbSet<UserBook>? UserBooks { get; set; }
        public DbSet<Comment>? Comments { get; set; }
    }
}
