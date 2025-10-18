using FindACoach.Core.Domain.Entities;
using FindACoach.Core.Domain.Entities.User;
using FindACoach.Core.Domain.IdentityEntities;
using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile.Education;
using FindACoach.Core.Enums;
using FindACoach.Infrastructure.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FindACoach.Infrastructure.Repositories
{
    public class SchoolsRepository : ISchoolsRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;

        public SchoolsRepository(ApplicationDbContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task AddSchool(string userId, AddSchoolDTO dto)
        {
            User user = await _userManager.Users
                .Where(u => u.Id == Guid.Parse(userId))
                .Include(u => u.Skills)
                .FirstOrDefaultAsync(u => u.Id == Guid.Parse(userId));

            if (user == null)
            {
                throw new ArgumentException("User with specified id doesn't exist.");
            }

            DegreeOptions degree = Enum.Parse<DegreeOptions>(dto.Degree, ignoreCase: true);

            if (degree != DegreeOptions.Bachelor && degree != DegreeOptions.Master && degree != DegreeOptions.Doctor && degree != DegreeOptions.Habilitation && degree != DegreeOptions.Professor)
            {
                throw new ArgumentException("Given degree is incorrect.");
            }

            School school = new School()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse(userId),
                SchoolName = dto.SchoolName,
                Degree = degree,
                FieldOfStudy = dto.FieldOfStudy,
                Location = dto.Location,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate
            };

            foreach (var skillTitle in dto.SkillTitles)
            {
                Skill skill = await _db.Skills.FirstOrDefaultAsync(s => s.Title == skillTitle);

                if (skill != null)
                {
                    if (!user.Skills.Any(s => s.Id == skill.Id))
                    {
                        user.Skills.Add(skill);
                    }
                    school.Skills.Add(skill);
                }
                else
                {
                    skill = new Skill
                    {
                        Id = Guid.NewGuid(),
                        Title = skillTitle
                    };
                    _db.Skills.Add(skill);
                    if (!user.Skills.Any(s => s.Id == skill.Id))
                    {
                        user.Skills.Add(skill);
                    }
                    school.Skills.Add(skill);
                }
            }

            await _db.AddAsync(school);

            await _db.SaveChangesAsync();
        }

        public async Task DeleteSchool(string schoolId, string activeUserId)
        {
            School school = await _db.Schools
                .Where(s => s.Id == Guid.Parse(schoolId))
                .Include(s => s.Skills)
                .FirstOrDefaultAsync(s => s.Id == Guid.Parse(schoolId));

            if (school == null)
            {
                throw new ArgumentException("School with specified id doesn't exist.");
            }

            User user = await _userManager.Users
                .Where(u => u.Id == school.UserId)
                .Include(u => u.Skills)
                .FirstOrDefaultAsync(u => u.Id == school.UserId);
            if (user == null)
            {
                throw new ArgumentException("User with specified id doesn't exist.");
            }

            if (school.UserId.ToString() != activeUserId)
            {
                throw new UnauthorizedAccessException("Only creator of school can edit the position.");
            }

            foreach (var schoolSkill in school.Skills.ToList())
            {
                foreach (var userSkill in user.Skills.ToList())
                {
                    bool isSkillUsedElsewhere =
                        user.Positions.Any(p => p.Skills.Any(s => s.Id == userSkill.Id && p.Id != school.Id)) ||
                        user.Certifications.Any(c => c.Skills.Any(s => s.Id == userSkill.Id)) ||
                        user.Projects.Any(pr => pr.Skills.Any(s => s.Id == userSkill.Id)) ||
                        user.Schools.Any(sc => sc.Skills.Any(s => s.Id == userSkill.Id));

                    if (!isSkillUsedElsewhere && userSkill.Id == schoolSkill.Id)
                    {
                        user.Skills.Remove(userSkill);
                    }
                }
            }

            _db.Schools.Remove(school);

            await _db.SaveChangesAsync();
        }

        public async Task EditSchool(EditSchoolDTO dto, string editorId)
        {
            DegreeOptions degree = Enum.Parse<DegreeOptions>(dto.Degree, ignoreCase: true);

            if (degree != DegreeOptions.Bachelor && degree != DegreeOptions.Master && degree != DegreeOptions.Doctor && degree != DegreeOptions.Habilitation && degree != DegreeOptions.Professor)
            {
                throw new ArgumentException("Given degree is incorrect.");
            }

            School school = await _db.Schools
                .Where(s => s.Id == Guid.Parse(dto.SchoolId))
                .Include(s => s.Skills)
                .FirstOrDefaultAsync(s => s.Id == Guid.Parse(dto.SchoolId));

            if (school == null)
            {
                throw new ArgumentException("School with specified id doesn't exist.");
            }

            User user = await _userManager.Users
                .Where(u => u.Id == school.UserId)
                .Include(u => u.Skills)
                .FirstOrDefaultAsync(u => u.Id == school.UserId);
            if (user == null)
            {
                throw new ArgumentException("User with specified id doesn't exist.");
            }

            if (school.UserId.ToString() != editorId)
            {
                throw new UnauthorizedAccessException("Only user which added that school can edit it.");
            }

            school.SchoolName = dto.SchoolName;
            school.Degree = degree;
            school.FieldOfStudy = dto.FieldOfStudy;
            school.StartDate = dto.StartDate;
            school.EndDate = dto.EndDate;
            school.Location = dto.Location;

            foreach (var schoolSkill in school.Skills.ToList())
            {
                foreach (var userSkill in user.Skills.ToList())
                {
                    bool isSkillUsedElsewhere =
                    user.Positions.Any(p => p.Skills.Any(s => s.Id == userSkill.Id && p.Id != school.Id)) ||
                    user.Certifications.Any(c => c.Skills.Any(s => s.Id == userSkill.Id)) ||
                    user.Projects.Any(pr => pr.Skills.Any(s => s.Id == userSkill.Id)) ||
                    user.Schools.Any(sc => sc.Skills.Any(s => s.Id == userSkill.Id));

                    if (!isSkillUsedElsewhere && userSkill.Id == schoolSkill.Id)
                    {
                        user.Skills.Remove(userSkill);
                    }
                }
            }

            school.Skills.Clear();

            foreach (var skillTitle in dto.SkillTitles)
            {
                Skill skill = await _db.Skills.FirstOrDefaultAsync(s => s.Title == skillTitle);
                if (skill != null)
                {
                    if (!user.Skills.Any(s => s.Id == skill.Id))
                    {
                        user.Skills.Add(skill);
                    }
                    school.Skills.Add(skill);
                }
                else
                {
                    skill = new Skill
                    {
                        Id = Guid.NewGuid(),
                        Title = skillTitle
                    };
                    _db.Skills.Add(skill);
                    if (!user.Skills.Any(s => s.Id == skill.Id))
                    {
                        user.Skills.Add(skill);
                    }
                    school.Skills.Add(skill);
                }
            }

            await _db.SaveChangesAsync();
        }

        public async Task<List<SchoolToResponse>> GetAllSchools(string userId)
        {
            var schools = await _db.Schools
                .Where(s => s.UserId == Guid.Parse(userId))
                .OrderByDescending(s => s.EndDate)
                .Select(s => new SchoolToResponse()
                {
                    SchoolId = s.Id,
                    SchoolName = s.SchoolName,
                    FieldOfStudy = s.FieldOfStudy,
                    Degree = s.Degree.ToString(),
                    Location = s.Location,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    SkillTitles = s.Skills.Select(s => s.Title).ToList()
                })
                .ToListAsync();

            return schools;
        }

        public async Task<List<SchoolToResponse>> GetLastTwoSchools(string userId)
        {
            var schools = await _db.Schools
                .Where(s => s.UserId == Guid.Parse(userId))
                .OrderByDescending(s => s.EndDate)
                .Take(2)
                .Select(s => new SchoolToResponse()
                {
                    SchoolId = s.Id,
                    SchoolName = s.SchoolName,
                    FieldOfStudy = s.FieldOfStudy,
                    Degree = s.Degree.ToString(),
                    Location = s.Location,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    SkillTitles = s.Skills.Select(s => s.Title).ToList()
                })
                .ToListAsync();

            return schools;
        }

        public async Task<SchoolToResponse> GetSchool(string schoolId)
        {
            var school = await _db.Schools
                .Where(s => s.Id == Guid.Parse(schoolId))
                .Select(s => new SchoolToResponse()
                {
                    SchoolId = s.Id,
                    SchoolName = s.SchoolName,
                    FieldOfStudy = s.FieldOfStudy,
                    Degree = s.Degree.ToString(),
                    Location = s.Location,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    SkillTitles = s.Skills.Select(s => s.Title).ToList()
                })
                .FirstOrDefaultAsync(s => s.SchoolId == Guid.Parse(schoolId));

            return school;
        }
    }
}
