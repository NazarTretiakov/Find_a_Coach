using FindACoach.Core.Domain.Entities;
using FindACoach.Core.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FindACoach.Infrastructure.DbContext
{
    public class ApplicationDbContext: IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Website> Websites { get; set; }

        public ApplicationDbContext(DbContextOptions options): base(options) {  }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Website>().ToTable("Websites");


            builder.Entity<User>()
                .HasMany(u => u.Websites)
                .WithOne(w => w.User)
                .HasForeignKey(u => u.UserId);
        }
    }
}
