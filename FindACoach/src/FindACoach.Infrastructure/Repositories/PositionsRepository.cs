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
                    skill = new Skill
                    {
                        Id = Guid.NewGuid(),
                        Title = skillTitle
                    };
                    _db.Skills.Add(skill);
                    position.Skills.Add(skill);
                }
            }

            await _db.AddAsync(position);

            await _db.SaveChangesAsync();
        }

        public async Task DeletePosition(string positionId, string activeUserId)
        {
            Position position = await _db.Positions.FirstOrDefaultAsync(p => p.Id == Guid.Parse(positionId));

            if (position == null)
            {
                throw new ArgumentException("Position with specified id doesn't exist.");
            }

            if (position.UserId.ToString() != activeUserId)
            {
                throw new UnauthorizedAccessException("Only creator of position can edit the position.");
            }

            _db.Positions.Remove(position);

            await _db.SaveChangesAsync();
        }

        public async Task EditPosition(EditPositionDTO dto, string editorId)
        {
            Position position = await _db.Positions
                .Where(p => p.Id == Guid.Parse(dto.PositionId))
                .Include(p => p.Skills)
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(dto.PositionId));

            if (position == null)
            {
                throw new ArgumentException("Position with specified id doesn't exist.");
            }

            if (position.UserId.ToString() != editorId)
            {
                throw new UnauthorizedAccessException("Only creator of position can edit the position.");
            }

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

            position.Title = dto.Title;
            position.CompanyName = dto.CompanyName;
            position.EmploymentType = employmentType;
            position.IsCurrentlyWorkingHere = dto.IsCurrentlyWorkingHere;
            position.StartDate = dto.StartDate;
            position.EndDate = dto.EndDate;
            position.Description = dto.Description;
            position.Location = dto.Location;

            position.Skills.Clear();

            foreach (var skillTitle in dto.SkillTitles)
            {
                Skill skill = await _db.Skills.FirstOrDefaultAsync(s => s.Title == skillTitle);
                if (skill != null)
                {
                    position.Skills.Add(skill);
                }
                else
                {
                    skill = new Skill
                    {
                        Id = Guid.NewGuid(),
                        Title = skillTitle
                    };
                    _db.Skills.Add(skill);
                    position.Skills.Add(skill);
                }
            }

            await _db.SaveChangesAsync();
        }

        public async Task<List<PositionToResponse>> GetAllPositions(string userId)
        {
            var positions = await _db.Positions
                .Where(p => p.UserId == Guid.Parse(userId))
                .OrderByDescending(p => p.StartDate)
                .Select(p => new PositionToResponse()
                {
                    PositionId = p.Id,
                    Title = p.Title,
                    CompanyName = p.CompanyName,
                    EmploymentType = p.EmploymentType.ToString(),
                    IsCurrentlyWorkingHere = p.IsCurrentlyWorkingHere,
                    Location = p.Location,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    Description = p.Description,
                    SkillTitles = p.Skills
                        .Select(s => s.Title)
                        .ToList()
                })
                .ToListAsync();

            return positions;
        }

        public async Task<List<PositionToResponse>> GetLastTwoPositions(string userId)
        {
            var positions = await _db.Positions
                .Where(p => p.UserId == Guid.Parse(userId))
                .OrderByDescending(p => p.StartDate)
                .Take(2)
                .Select(p => new PositionToResponse()
                {
                    PositionId = p.Id,
                    Title = p.Title,
                    CompanyName = p.CompanyName,
                    EmploymentType = p.EmploymentType.ToString(),
                    IsCurrentlyWorkingHere = p.IsCurrentlyWorkingHere,
                    Location = p.Location,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    Description = p.Description,
                    SkillTitles = p.Skills
                        .Select(s => s.Title)
                        .ToList()
                })
                .ToListAsync();

            return positions;
        }

        public async Task<PositionToResponse> GetPosition(string positionId)
        {
            var position = await _db.Positions
                .Where(p => p.Id == Guid.Parse(positionId))
                .Select(p => new PositionToResponse()
                {
                    PositionId = p.Id,
                    Title = p.Title,
                    CompanyName = p.CompanyName,
                    EmploymentType = p.EmploymentType.ToString(),
                    IsCurrentlyWorkingHere = p.IsCurrentlyWorkingHere,
                    Location = p.Location,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    Description = p.Description,
                    SkillTitles = p.Skills
                        .Select(s => s.Title)
                        .ToList()
                })
                .FirstOrDefaultAsync(p => p.PositionId == Guid.Parse(positionId));

            return position;
        }
    }
}
