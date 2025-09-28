using FindACoach.Core.Domain.Entities;
using FindACoach.Core.Domain.Entities.Activity;
using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile.Activities;
using FindACoach.Infrastructure.DbContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace FindACoach.Infrastructure.Repositories
{
    public class ActivitiesRepository : IActivitiesRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public ActivitiesRepository(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

        public async Task AddEvent(string userId, EventDTO dto)
        {
            Event userEvent = new Event()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse(userId),
                Title = dto.Title,
                BeginningDate = (DateTime)dto.BeginningDate,
                ImagePath = await AddActivityImage(dto.Image),
                Description = dto.Description,
                CreatedAt = DateTime.Now
            };

            foreach (SubjectDTO subjectDTO in dto.Subjects)
            {
                userEvent.Subjects.Add(new Subject()
                {
                    Id = Guid.NewGuid(),
                    Title = subjectDTO.Title,
                    ActivityId = userEvent.Id
                });
            }

            foreach (SearchPersonPanelDTO panelDTO in dto.SearchPersonPanels)
            {
                userEvent.SearchPersonPanels.Add(new SearchPersonPanel()
                {
                    Id = Guid.NewGuid(),
                    PositionName = panelDTO.PositionName,
                    Description = panelDTO.Description,
                    Payment = panelDTO.Payment,
                    EventId = userEvent.Id
                });
            }

            foreach (var (panelDTO, panelEntity) in dto.SearchPersonPanels.Zip(userEvent.SearchPersonPanels))
            {
                if (string.IsNullOrEmpty(panelDTO.PreferredSkills)) 
                {
                    continue;
                }

                string[] panelPreferredSkills = panelDTO.PreferredSkills.Split(',', StringSplitOptions.RemoveEmptyEntries);

                foreach (var skill in panelPreferredSkills.Select(s => s.Trim()))
                {
                    panelEntity.PreferredSkills.Add(new Skill
                    {
                        Id = Guid.NewGuid(),
                        Title = skill
                    });
                }
            }

            await _db.Events.AddAsync(userEvent);

            await _db.SaveChangesAsync();
        }

        public async Task AddPost(string userId, PostDTO dto)
        {
            Post userPost = new Post()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse(userId),
                Title = dto.Title,
                ImagePath = await AddActivityImage(dto.Image),
                Description = dto.Description,
                CreatedAt = DateTime.Now
            };

            foreach (SubjectDTO subjectDTO in dto.Subjects)
            {
                userPost.Subjects.Add(new Subject()
                {
                    Id = Guid.NewGuid(),
                    Title = subjectDTO.Title,
                    ActivityId = userPost.Id
                });
            }

            await _db.Posts.AddAsync(userPost);

            await _db.SaveChangesAsync();
        }

        public async Task AddQA(string userId, QADTO dto)
        {
            QA userQA = new QA()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse(userId),
                Title = dto.Title,
                ImagePath = await AddActivityImage(dto.Image),
                Description = dto.Description,
                CreatedAt = DateTime.Now
            };

            foreach (SubjectDTO subjectDTO in dto.Subjects)
            {
                userQA.Subjects.Add(new Subject()
                {
                    Id = Guid.NewGuid(),
                    Title = subjectDTO.Title,
                    ActivityId = userQA.Id
                });
            }

            await _db.QAs.AddAsync(userQA);

            await _db.SaveChangesAsync();
        }

        public async Task AddSurvey(string userId, SurveyDTO dto)
        {
            Survey userSurvey = new Survey()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse(userId),
                Title = dto.Title,
                ImagePath = await AddActivityImage(dto.Image),
                Description = dto.Description,
                CreatedAt = DateTime.Now
            };

            foreach (SubjectDTO subjectDTO in dto.Subjects)
            {
                userSurvey.Subjects.Add(new Subject()
                {
                    Id = Guid.NewGuid(),
                    Title = subjectDTO.Title,
                    ActivityId = userSurvey.Id
                });
            }

            foreach (SurveyOptionDTO optionDTO in dto.SurveyOptions)
            {
                userSurvey.Options.Add(new SurveyOption()
                {
                    Id = Guid.NewGuid(),
                    Inscription = optionDTO.Inscription,
                    SurveyId = userSurvey.Id
                });
            }

            await _db.Surveys.AddAsync(userSurvey);

            await _db.SaveChangesAsync();
        }

        private async Task<string> AddActivityImage(IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return null;
            }

            string profileImagesFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Activities");

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
            string imagePathToCreateFile = Path.Combine(profileImagesFolder, uniqueFileName);

            using (var imageStream = image.OpenReadStream())
            using (var imageResult = Image.Load(imageStream))
            {
                imageResult.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(560, 560),
                    Mode = ResizeMode.Crop
                }));

                await imageResult.SaveAsync(imagePathToCreateFile, new JpegEncoder
                {
                    Quality = 90
                });
            }

            return uniqueFileName;
        }
    }
}
