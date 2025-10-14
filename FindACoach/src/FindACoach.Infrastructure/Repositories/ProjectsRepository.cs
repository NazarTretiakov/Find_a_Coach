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
                    skill = new Skill
                    {
                        Id = Guid.NewGuid(),
                        Title = skillTitle
                    };
                    _db.Skills.Add(skill);
                    project.Skills.Add(skill);
                }
            }

            await _db.AddAsync(project);

            await _db.SaveChangesAsync();
        }

        public async Task DeleteProject(string projectId, string activeUserId)
        {
            Project project = await _db.Projects.FirstOrDefaultAsync(s => s.Id == Guid.Parse(projectId));

            if (project == null)
            {
                throw new ArgumentException("Project with specified id doesn't exist.");
            }

            if (project.UserId.ToString() != activeUserId)
            {
                throw new UnauthorizedAccessException("Only creator of project can edit it.");
            }

            _db.Projects.Remove(project);

            await _db.SaveChangesAsync();
        }

        public async Task EditProject(EditProjectDTO dto, string editorId)
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

            Project project = await _db.Projects
                .Where(s => s.Id == Guid.Parse(dto.ProjectId))
                .Include(s => s.Skills)
                .FirstOrDefaultAsync(s => s.Id == Guid.Parse(dto.ProjectId));

            if (project == null)
            {
                throw new ArgumentException("Project with specified id doesn't exist.");
            }

            if (project.UserId.ToString() != editorId)
            {
                throw new UnauthorizedAccessException("Only user which added that project can edit it.");
            }

            project.Title = dto.Title;
            project.RelatedTo = relatedTo;
            project.StartDate = dto.StartDate;
            project.EndDate = dto.EndDate;
            project.Location = dto.Location;
            project.Description = dto.Description;
            project.Participants = string.Join(",", dto.Participants);

            project.Skills.Clear();

            foreach (var skillTitle in dto.SkillTitles)
            {
                Skill skill = await _db.Skills.FirstOrDefaultAsync(s => s.Title == skillTitle);
                if (skill != null)
                {
                    project.Skills.Add(skill);
                }
                else
                {
                    skill = new Skill
                    {
                        Id = Guid.NewGuid(),
                        Title = skillTitle
                    };
                    _db.Skills.Add(skill);
                    project.Skills.Add(skill);
                }
            }

            await _db.SaveChangesAsync();
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
