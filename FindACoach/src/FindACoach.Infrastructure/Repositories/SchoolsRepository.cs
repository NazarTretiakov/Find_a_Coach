using FindACoach.Core.Domain.Entities;
using FindACoach.Core.Domain.Entities.User;
using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile.Education;
using FindACoach.Core.Enums;
using FindACoach.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace FindACoach.Infrastructure.Repositories
{
    public class SchoolsRepository : ISchoolsRepository
    {
        private readonly ApplicationDbContext _db;

        public SchoolsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddSchool(string userId, AddSchoolDTO dto)
        {
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
                    school.Skills.Add(skill);
                }
            }

            await _db.AddAsync(school);

            await _db.SaveChangesAsync();
        }

        public async Task DeleteSchool(string schoolId, string activeUserId)
        {
            School school = await _db.Schools.FirstOrDefaultAsync(s => s.Id == Guid.Parse(schoolId));

            if (school == null)
            {
                throw new ArgumentException("Position with specified id doesn't exist.");
            }

            if (school.UserId.ToString() != activeUserId)
            {
                throw new UnauthorizedAccessException("Only creator of position can edit the position.");
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

            school.Skills.Clear();

            foreach (var skillTitle in dto.SkillTitles)
            {
                Skill skill = await _db.Skills.FirstOrDefaultAsync(s => s.Title == skillTitle);
                if (skill != null)
                {
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
