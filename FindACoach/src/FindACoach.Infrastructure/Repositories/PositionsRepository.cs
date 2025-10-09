using FindACoach.Core.Domain.Entities;
using FindACoach.Core.Domain.Entities.User;
using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile.Experience;
using FindACoach.Core.Enums;
using FindACoach.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace FindACoach.Infrastructure.Repositories
{
    public class PositionsRepository : IPositionsRepository
    {
        private readonly ApplicationDbContext _db;

        public PositionsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddPosition(string userId, AddPositionDTO dto)
        {
            EmploymentType employmentType;

            if (dto.EmploymentType == "full-time")
            {
                employmentType = EmploymentType.FullTime;
            }
            else if (dto.EmploymentType == "part-time")
            {
                employmentType = EmploymentType.PartTime;
            }
            else if (dto.EmploymentType == "self-employed")
            {
                employmentType = EmploymentType.SelfEmployed;
            }
            else
            {
                employmentType = EmploymentType.Internship;
            }

            Position position = new Position()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse(userId),
                Title = dto.Title,
                CompanyName = dto.CompanyName,
                EmploymentType = employmentType,
                IsCurrentlyWorkingHere = dto.IsCurrentlyWorkingHere,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Location = dto.Location,
                Description = dto.Description
            };

            foreach (var skillTitle in dto.SkillTitles)
            {
                Skill skill = await _db.Skills.FirstOrDefaultAsync(s => s.Title == skillTitle);

                if (skill != null)
                {
                    position.Skills.Add(skill);
                }
                else
                {
                    position.Skills.Add(new Skill
                    {
                        Id = Guid.NewGuid(),
                        Title = skillTitle
                    });
                }
            }

            await _db.AddAsync(position);

            await _db.SaveChangesAsync();
        }
    }
}
