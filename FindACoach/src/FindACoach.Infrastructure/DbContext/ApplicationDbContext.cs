using FindACoach.Core.Domain.Entities;
using FindACoach.Core.Domain.Entities.Activity;
using FindACoach.Core.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection.Emit;

namespace FindACoach.Infrastructure.DbContext
{
    public class ApplicationDbContext: IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Website> Websites { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<Core.Domain.Entities.Activity.Activity> Activities { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<QA> QAs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SearchPersonPanel> SearchPeoplePanels { get; set; }
        public DbSet<SurveyOption> SurveyOptions { get; set; }
        public DbSet<QAAnswer> QAAnswers { get; set; }
        public DbSet<Skill> Skills { get; set; }

        public ApplicationDbContext(DbContextOptions options): base(options) {  }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Website>().ToTable("Websites");
            builder.Entity<Connection>().ToTable("Connections");

            builder.Entity<Core.Domain.Entities.Activity.Activity>().ToTable("Activities");
            builder.Entity<Event>().ToTable("Events");
            builder.Entity<Survey>().ToTable("Surveys");
            builder.Entity<QA>().ToTable("QAs");
            builder.Entity<Post>().ToTable("Posts");
            builder.Entity<Subject>().ToTable("Subjects");
            builder.Entity<SearchPersonPanel>().ToTable("SearchPeoplePanels");
            builder.Entity<SurveyOption>().ToTable("SurveyOptions");
            builder.Entity<QAAnswer>().ToTable("QAAnswers");
            builder.Entity<Skill>().ToTable("Skills");


            builder.Entity<User>()
                .HasMany(u => u.Websites)
                .WithOne(w => w.User)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Connection>()
                .HasOne(c => c.User)
                .WithMany(u => u.Connections)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Connection>()
                .HasOne(c => c.ConnectedUser)
                .WithMany()
                .HasForeignKey(c => c.ConnectedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Core.Domain.Entities.Activity.Activity>()
               .HasOne(a => a.User)
               .WithMany(u => u.Activities)
               .HasForeignKey(a => a.UserId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Subject>()
                .HasOne(s => s.Activity)
                .WithMany(a => a.Subjects)
                .HasForeignKey(s => s.ActivityId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SearchPersonPanel>()
                .HasOne(p => p.Event)
                .WithMany(e => e.SearchPersonPanels)
                .HasForeignKey(p => p.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SurveyOption>()
                .HasOne(o => o.Survey)
                .WithMany(s => s.Options)
                .HasForeignKey(o => o.SurveyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<QAAnswer>()
                .HasOne(a => a.QA)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QAId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Skill>()
                .HasMany(s => s.Users)
                .WithMany(u => u.Skills);

            builder.Entity<Skill>()
                .HasMany(s => s.Panels)
                .WithMany(p => p.PreferredSkills);
        }
    }
}
