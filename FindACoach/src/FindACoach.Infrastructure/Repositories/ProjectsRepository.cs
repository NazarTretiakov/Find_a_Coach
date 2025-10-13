using FindACoach.Core.Domain.Entities;
using FindACoach.Core.Domain.Entities.User;
using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile.Projects;
using FindACoach.Core.Enums;
using FindACoach.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace FindACoach.Infrastructure.Repositories
{
    public class ProjectsRepository : IProjectsRepository
    {
        private readonly ApplicationDbContext _db;

        public ProjectsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddProject(string userId, AddProjectDTO dto)
        {
            RelatedTo relatedTo = Enum.Parse<RelatedTo>(dto.RelatedTo);

            if (relatedTo != RelatedTo.Job && relatedTo != RelatedTo.Education && relatedTo != RelatedTo.Event && relatedTo != RelatedTo.Other)
            {
                throw new ArgumentException("Given \"related to\" information of project is incorrect.");
            }

            for (int i = 0; i < dto.Participants.Count; i++)
            {
                dto.Participants[i] = dto.Participants[i].Trim();
            }

            Project project = new Project()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse(userId),
                Title = dto.Title,
                RelatedTo = relatedTo,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Location = dto.Location,
                Description = dto.Description,
                Participants = string.Join(",", dto.Participants)
            };

            foreach (var skillTitle in dto.SkillTitles)
            {
                Skill skill = await _db.Skills.FirstOrDefaultAsync(s => s.Title == skillTitle);

                if (skill != null)
                {
                    project.Skills.Add(skill);
                }
                else
                {
                    project.Skills.Add(new Skill
                    {
                        Id = Guid.NewGuid(),
                        Title = skillTitle
                    });
                }
            }

            await _db.AddAsync(project);

            await _db.SaveChangesAsync();
        }

        public Task DeleteProject(string projectId, string activeUserId)
        {
            throw new NotImplementedException();
        }

        public Task EditProject(EditProjectDTO dto, string editorId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProjectToResponse>> GetAllProjects(string userId)
        {
            var projects = _db.Projects
                .Where(p => p.UserId == Guid.Parse(userId))
                .OrderByDescending(p => p.EndDate)
                .Include(p => p.Skills)
                .AsEnumerable()
                .Select(p => new ProjectToResponse()
                {
                    ProjectId = p.Id,
                    Title = p.Title,
                    RelatedTo = p.RelatedTo.ToString(),
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    Location = p.Location,
                    Description = p.Description,
                    SkillTitles = p.Skills.Select(s => s.Title).ToList(),
                    Participants = p.Participants
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => s.Trim())
                        .ToList()
                })
                .ToList();

            return projects;
        }

        public async Task<List<ProjectToResponse>> GetLastTwoProjects(string userId)
        {
            var projects = _db.Projects
                .Where(p => p.UserId == Guid.Parse(userId))
                .OrderByDescending(p => p.EndDate)
                .Take(2)
                .Include(p => p.Skills)
                .AsEnumerable()
                .Select(p => new ProjectToResponse()
                {
                    ProjectId = p.Id,
                    Title = p.Title,
                    RelatedTo = p.RelatedTo.ToString(),
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    Description = p.Description,
                    Location = p.Location,
                    SkillTitles = p.Skills.Select(s => s.Title).ToList(),
                    Participants = p.Participants
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => s.Trim())
                        .ToList()
                })
                .ToList();

            return projects;
        }

        public async Task<ProjectToResponse> GetProject(string projectId)
        {
            var projects = _db.Projects
                .Where(p => p.Id == Guid.Parse(projectId))
                .Include(p => p.Skills)
                .AsEnumerable()
                .Select(p => new ProjectToResponse()
                {
                    ProjectId = p.Id,
                    Title = p.Title,
                    RelatedTo = p.RelatedTo.ToString(),
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    Location = p.Location,
                    Description = p.Description,
                    SkillTitles = p.Skills.Select(s => s.Title).ToList(),
                    Participants = p.Participants
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => s.Trim())
                        .ToList()
                })
                .FirstOrDefault(p => p.ProjectId == Guid.Parse(projectId));

            return projects;
        }
    }
}
