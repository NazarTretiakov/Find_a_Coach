using FindACoach.Core.Domain.Entities;
using FindACoach.Core.Domain.Entities.Activity;
using FindACoach.Core.Domain.Entities.User;
using FindACoach.Core.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FindACoach.Infrastructure.DbContext
{
    public class ApplicationDbContext: IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Website> Websites { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Certification> Certifications { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
        public DbSet<Core.Domain.Entities.Activity.Activity> Activities { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<QA> QAs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SearchPersonPanel> SearchPeoplePanels { get; set; }
        public DbSet<SurveyOption> SurveyOptions { get; set; }
        public DbSet<QAAnswer> QAAnswers { get; set; }
        public DbSet<EventApplication> EventApplications { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Save> Saves { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public ApplicationDbContext(DbContextOptions options): base(options) {  }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Website>().ToTable("Websites");
            builder.Entity<Connection>().ToTable("Connections");
            builder.Entity<Position>().ToTable("Positions");
            builder.Entity<School>().ToTable("Schools");
            builder.Entity<Project>().ToTable("Projects");
            builder.Entity<Certification>().ToTable("Certifications");
            builder.Entity<Language>().ToTable("Languages");
            builder.Entity<Recommendation>().ToTable("Recommendations");

            builder.Entity<Core.Domain.Entities.Activity.Activity>().ToTable("Activities");
            builder.Entity<Event>().ToTable("Events");
            builder.Entity<Survey>().ToTable("Surveys");
            builder.Entity<QA>().ToTable("QAs");
            builder.Entity<Post>().ToTable("Posts");
            builder.Entity<Subject>().ToTable("Subjects");
            builder.Entity<SearchPersonPanel>().ToTable("SearchPeoplePanels");
            builder.Entity<SurveyOption>().ToTable("SurveyOptions");
            builder.Entity<QAAnswer>().ToTable("QAAnswers");
            builder.Entity<EventApplication>().ToTable("EventApplications");
            builder.Entity<Skill>().ToTable("Skills");
            builder.Entity<Like>().ToTable("Likes");
            builder.Entity<Save>().ToTable("Saves");
            builder.Entity<Comment>().ToTable("Comments");


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

            builder.Entity<Position>()
                .HasOne(p => p.User)
                .WithMany(u => u.Positions)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<School>()
                .HasOne(s => s.User)
                .WithMany(u => u.Schools)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Project>()
                .HasOne(p => p.User)
                .WithMany(u => u.Projects)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Certification>()
                .HasOne(c => c.User)
                .WithMany(u => u.Certifications)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Language>()
                .HasOne(l => l.User)
                .WithMany(u => u.Languages)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Recommendation>()
                .HasOne(r => r.RecommenderUser)
                .WithMany(u => u.RecommendationsGiven)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Recommendation>()
                .HasOne(r => r.RecommendedUser)
                .WithMany(u => u.RecommendationsReceived)
                .OnDelete(DeleteBehavior.NoAction);

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

            builder.Entity<QAAnswer>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<EventApplication>()
                .HasOne(a => a.SearchPersonPanel)
                .WithMany()
                .HasForeignKey(a => a.SearchPersonPanelId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<EventApplication>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Skill>()
                .HasMany(s => s.Users)
                .WithMany(u => u.Skills);

            builder.Entity<Skill>()
                .HasMany(s => s.Panels)
                .WithMany(p => p.PreferredSkills);

            builder.Entity<Skill>()
                .HasMany(s => s.Positions)
                .WithMany(p => p.Skills);

            builder.Entity<Skill>()
                .HasMany(s => s.Schools)
                .WithMany(s => s.Skills);

            builder.Entity<Skill>()
                .HasMany(s => s.Projects)
                .WithMany(p => p.Skills);

            builder.Entity<Skill>()
                .HasMany(s => s.Certifications)
                .WithMany(c => c.Skills);

            builder.Entity<Like>()
                .HasOne(l => l.Activity)
                .WithMany(a => a.Likes)
                .HasForeignKey(l => l.ActivityId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Like>()
                .HasOne(l => l.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Save>()
                .HasOne(s => s.Activity)
                .WithMany(a => a.Saves)
                .HasForeignKey(s => s.ActivityId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Save>()
                .HasOne(s => s.User)
                .WithMany(u => u.Saves)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Comment>()
                .HasOne(c => c.Activity)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.ActivityId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}