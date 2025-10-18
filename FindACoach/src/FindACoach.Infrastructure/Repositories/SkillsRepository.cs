using FindACoach.Core.Domain.Entities;
using FindACoach.Core.Domain.Entities.User;
using FindACoach.Core.Domain.IdentityEntities;
using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile.Skills;
using FindACoach.Infrastructure.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FindACoach.Infrastructure.Repositories
{
    public class SkillsRepository : ISkillsRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;

        public SkillsRepository(ApplicationDbContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<List<SkillToResponse>> GetAllSkills(string userId)
        {
            User user = await _userManager.Users
                .Where(u => u.Id == Guid.Parse(userId))
                .Select(u => new User()
                {
                    Id = u.Id,
                    Skills = u.Skills.Select(s => new Skill()
                    {
                        Id = s.Id,
                        Title = s.Title,
                        Positions = s.Positions,
                        Schools = s.Schools,
                        Certifications = s.Certifications,
                        Projects = s.Projects
                    }).ToList()
                })
                .FirstOrDefaultAsync(u => u.Id == Guid.Parse(userId));

            if (user == null)
            {
                throw new ArgumentException("User with specified id doesn't exist.");
            }

            List<Skill> userSkillsFromDb = user.Skills
                .Select(s => new Skill()
                {
                    Id = s.Id,
                    Title = s.Title,
                    Positions = s.Positions,
                    Schools = s.Schools,
                    Certifications = s.Certifications,
                    Projects = s.Projects
                }).ToList();

            List<SkillToResponse> skillsToResponse = new List<SkillToResponse>();

            foreach (var skillFromDb in userSkillsFromDb)
            {
                SkillToResponse skillToResponse = new SkillToResponse()
                {
                    SkillId = skillFromDb.Id.ToString(),
                    Title = skillFromDb.Title
                };

                if (skillFromDb.Schools != null)
                {
                    foreach(School school in  skillFromDb.Schools)
                    {
                        skillToResponse.Usages.Add(new UsageOfSkill()
                        {
                            Title = school.SchoolName,
                            Type = "School"
                        });
                    }
                }
                if (skillFromDb.Positions != null)
                {
                    foreach (Position position in skillFromDb.Positions)
                    {
                        skillToResponse.Usages.Add(new UsageOfSkill()
                        {
                            Title = position.CompanyName,
                            Type = "Company"
                        });
                    }
                }
                if (skillFromDb.Projects != null)
                {
                    foreach (Project project in skillFromDb.Projects)
                    {
                        skillToResponse.Usages.Add(new UsageOfSkill()
                        {
                            Title = project.Title,
                            Type = "Project"
                        });
                    }
                }
                if (skillFromDb.Certifications != null)
                {
                    foreach (Certification certification in skillFromDb.Certifications)
                    {
                        skillToResponse.Usages.Add(new UsageOfSkill()
                        {
                            Title = certification.Title,
                            Type = "Certification"
                        });
                    }
                }

                skillsToResponse.Add(skillToResponse);
            }

            return skillsToResponse.OrderByDescending(s => s.Usages.Count).ToList();
        }

        public async Task<List<SkillToResponse>> GetTwoSkillsWithMostUsages(string userId)
        {
            List<SkillToResponse> allSkills = await GetAllSkills(userId);

            List<SkillToResponse> twoSkillsWithMostUsages = allSkills
                .OrderByDescending(s => s.Usages.Count)
                .Take(2)
                .ToList();

            return twoSkillsWithMostUsages;
        }
    }
}
