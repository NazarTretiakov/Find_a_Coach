using FindACoach.Core.Domain.Entities;
using FindACoach.Core.Domain.Entities.Activity;
using FindACoach.Core.Domain.IdentityEntities;
using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.Forum;
using FindACoach.Core.DTO.MyProfile.Activities;
using FindACoach.Infrastructure.DbContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Security.Claims;

namespace FindACoach.Infrastructure.Repositories
{
    public class ActivitiesRepository : IActivitiesRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILikesRepository _likesRepository;
        private readonly ISavesRepository _savesRepository;

        public ActivitiesRepository(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ILikesRepository likesRepository, ISavesRepository savesRepository)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _likesRepository = likesRepository;
            _savesRepository = savesRepository;
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

                foreach (var skillTitle in panelPreferredSkills.Select(s => s.Trim()))
                {
                    Skill skill = await _db.Skills.FirstOrDefaultAsync(s => s.Title == skillTitle);

                    if (skill != null)
                    {
                        panelEntity.PreferredSkills.Add(skill);
                    }
                    else
                    {
                        panelEntity.PreferredSkills.Add(new Skill
                        {
                            Id = Guid.NewGuid(),
                            Title = skillTitle
                        });
                    }
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

        public async Task DeleteActivity(string activityId, string userId)
        {
            var informationToDeleteActivity = await _db.Activities
                .Where(c => c.Id == Guid.Parse(activityId))
                .Select(c => new 
                {
                    Activity = c,
                    UserId = c.User.Id
                })
                .FirstOrDefaultAsync(c => c.Activity.Id == Guid.Parse(activityId));

            if (informationToDeleteActivity == null)
            {
                throw new ArgumentNullException("Activity id is incorrect.");
            }
            if (informationToDeleteActivity.UserId != Guid.Parse(userId))
            {
                throw new ArgumentException("Only creator of the activity can delete it.");
            }

            _db.Activities.Remove(informationToDeleteActivity.Activity);
            await _db.SaveChangesAsync();
        }

        public async Task<List<ActivityForActivitiesListToResponse>> GetActivitiesPaged(string userId, int page, int pageSize)
        {
            string serverUrl = _configuration.GetValue<string>("ServerUrl");

            var userActivities = await _db.Activities
                .Where(a => a.UserId == Guid.Parse(userId))
                .OrderByDescending(a => a.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(a => new ActivityForActivitiesListToResponse
                {
                    Id = a.Id,
                    ImagePathOfCreator = $"{serverUrl}/Images/UserProfiles/{a.User.ImagePath}",
                    FirstNameOfCreator = a.User.FirstName,
                    LastNameOfCreator = a.User.LastName,
                    PublicationDate = a.CreatedAt,
                    Title = a.Title,
                    Subjects = a.Subjects.Select(s => s.Title).ToList(),
                    ImagePath = string.IsNullOrEmpty(a.ImagePath) ? null : $"{serverUrl}/Images/Activities/{a.ImagePath}",
                    Description = a.Description,
                    ActivityType = a is Event ? "Event" :
                                   a is Survey ? "Survey" :
                                   a is QA ? "QA" :
                                   a is Post ? "Post" :
                                   "Unknown"
                })
                .ToListAsync();

            return userActivities;
        }

        public async Task<ActivityToResponse> GetActivity(string id)
        {
            string serverUrl = _configuration.GetValue<string>("ServerUrl");

            var principal = _httpContextAccessor.HttpContext?.User;
            if (principal == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }

            string? actviveUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (actviveUserId == null)
            {
                throw new UnauthorizedAccessException("Cannot resolve user id from claims");
            }

            bool isActivityLikedByActiveUser = await _likesRepository.IsActivityLikedByUser(actviveUserId, id);
            bool isActivitySavedByActiveUser = await _savesRepository.IsActivitySavedByUser(actviveUserId, id);

            Guid guidIdOfActivity = Guid.Parse(id);
            Core.Domain.Entities.Activity.Activity activity = await _db.Activities.FirstOrDefaultAsync(a => a.Id == guidIdOfActivity);

            if (activity == null)
            {
                throw new ArgumentException("The activity with given id don't exist.");
            }
            else if (activity is Event)
            {
                EventToResponse? eventToResponse = await _db.Events.Where(e => e.Id == guidIdOfActivity)
                                                                  .Select(e => new EventToResponse
                                                                  {
                                                                      Id = e.Id,
                                                                      UserId = e.User.Id,
                                                                      UserImagePath = $"{serverUrl}/Images/UserProfiles/{e.User.ImagePath}",
                                                                      UserFirstName = e.User.FirstName,
                                                                      UserLastName = e.User.LastName,
                                                                      Title = e.Title,
                                                                      ImagePath = string.IsNullOrEmpty(e.ImagePath) ? null : $"{serverUrl}/Images/Activities/{e.ImagePath}",
                                                                      Description = e.Description,
                                                                      CreatedAt = e.CreatedAt,
                                                                      Subjects = e.Subjects.Select(s => s.Title).ToList(),
                                                                      BeginningDate = e.BeginningDate,
                                                                      SearchPersonPanels = (List<SearchPersonPanelToResponse>)e.SearchPersonPanels.Select(p => new SearchPersonPanelToResponse
                                                                      {
                                                                          Id = p.Id,
                                                                          PositionName = p.PositionName,
                                                                          Description = p.Description,
                                                                          Payment = p.Payment,
                                                                          PreferredSkills = p.PreferredSkills.Select(s => s.Title).ToList()
                                                                      }).ToList(),
                                                                      IsLiked = isActivityLikedByActiveUser,
                                                                      NumberOfLikes = e.Likes.Count,
                                                                      IsSaved = isActivitySavedByActiveUser,
                                                                      Comments = e.Comments
                                                                        .Where(c => c.ActivityId == e.Id)
                                                                        .OrderByDescending(c => c.DateOfCreation)
                                                                        .Take(3)
                                                                        .Select(c => new CommentToResponse()
                                                                        {
                                                                            CommentId = c.Id,
                                                                            ActivityId = c.ActivityId,
                                                                            UserId = c.UserId,
                                                                            UserEmail = c.User.Email,
                                                                            UserFirstName = c.User.FirstName,
                                                                            UserLastName = c.User.LastName,
                                                                            UserImagePath = $"{serverUrl}/Images/UserProfiles/{c.User.ImagePath}",
                                                                            DateOfCreation = c.DateOfCreation,
                                                                            Content = c.Content
                                                                        }).ToList()
                                                                  })
                                                                  .FirstOrDefaultAsync(e => e.Id == guidIdOfActivity);

                return eventToResponse;
            }
            else if (activity is Survey)
            {
                SurveyToResponse? surveyToResponse = await _db.Surveys.Where(s => s.Id == guidIdOfActivity)
                                                                       .Select(s => new SurveyToResponse
                                                                       {
                                                                           Id = s.Id,
                                                                           UserId = s.User.Id,
                                                                           UserImagePath = $"{serverUrl}/Images/UserProfiles/{s.User.ImagePath}",
                                                                           UserFirstName = s.User.FirstName,
                                                                           UserLastName = s.User.LastName,
                                                                           Title = s.Title,
                                                                           ImagePath = string.IsNullOrEmpty(s.ImagePath) ? null : $"{serverUrl}/Images/Activities/{s.ImagePath}",
                                                                           Description = s.Description,
                                                                           CreatedAt = s.CreatedAt,
                                                                           Subjects = s.Subjects.Select(s => s.Title).ToList(),
                                                                           Options = s.Options.Select(o => new SurveyOptionToResponse
                                                                           {
                                                                               Id = o.Id,
                                                                               Inscription = o.Inscription
                                                                           }).ToList(),
                                                                           IsLiked = isActivityLikedByActiveUser,
                                                                           NumberOfLikes = s.Likes.Count,
                                                                           IsSaved = isActivitySavedByActiveUser,
                                                                           Comments = s.Comments
                                                                            .Where(c => c.ActivityId == s.Id)
                                                                            .OrderByDescending(c => c.DateOfCreation)
                                                                            .Take(3)
                                                                            .Select(c => new CommentToResponse()
                                                                            {
                                                                                CommentId = c.Id,
                                                                                ActivityId = c.ActivityId,
                                                                                UserId = c.UserId,
                                                                                UserEmail = c.User.Email,
                                                                                UserFirstName = c.User.FirstName,
                                                                                UserLastName = c.User.LastName,
                                                                                UserImagePath = $"{serverUrl}/Images/UserProfiles/{c.User.ImagePath}",
                                                                                DateOfCreation = c.DateOfCreation,
                                                                                Content = c.Content
                                                                            }).ToList()
                                                                       })
                                                                       .FirstOrDefaultAsync(s => s.Id == guidIdOfActivity);

                return surveyToResponse;
            }
            else if (activity is QA)
            {
                QAToResponse? QAToResponse = await _db.QAs.Where(qa => qa.Id == guidIdOfActivity)
                                                          .Select(qa => new QAToResponse
                                                          {
                                                              Id = qa.Id,
                                                              UserId = qa.User.Id,
                                                              UserImagePath = $"{serverUrl}/Images/UserProfiles/{qa.User.ImagePath}",
                                                              UserFirstName = qa.User.FirstName,
                                                              UserLastName = qa.User.LastName,
                                                              Title = qa.Title,
                                                              ImagePath = string.IsNullOrEmpty(qa.ImagePath) ? null : $"{serverUrl}/Images/Activities/{qa.ImagePath}",
                                                              Description = qa.Description,
                                                              CreatedAt = qa.CreatedAt,
                                                              IsLiked = isActivityLikedByActiveUser,
                                                              NumberOfLikes = qa.Likes.Count,
                                                              IsSaved = isActivitySavedByActiveUser,
                                                              Comments = qa.Comments
                                                                 .Where(c => c.ActivityId == qa.Id)
                                                                 .OrderByDescending(c => c.DateOfCreation)
                                                                 .Take(3)
                                                                 .Select(c => new CommentToResponse()
                                                                 {
                                                                     CommentId = c.Id,
                                                                     ActivityId = c.ActivityId,
                                                                     UserId = c.UserId,
                                                                     UserEmail = c.User.Email,
                                                                     UserFirstName = c.User.FirstName,
                                                                     UserLastName = c.User.LastName,
                                                                     UserImagePath = $"{serverUrl}/Images/UserProfiles/{c.User.ImagePath}",
                                                                     DateOfCreation = c.DateOfCreation,
                                                                     Content = c.Content
                                                                 }).ToList()
                                                          })
                                                          .FirstOrDefaultAsync(qa => qa.Id == guidIdOfActivity);

                return QAToResponse;
            }
            else
            {
                PostToResponse? postToResponse = await _db.Posts.Where(p => p.Id == guidIdOfActivity)
                                          .Select(p => new PostToResponse
                                          {
                                              Id = p.Id,
                                              UserId = p.User.Id,
                                              UserImagePath = $"{serverUrl}/Images/UserProfiles/{p.User.ImagePath}",
                                              UserFirstName = p.User.FirstName,
                                              UserLastName = p.User.LastName,
                                              Title = p.Title,
                                              ImagePath = string.IsNullOrEmpty(p.ImagePath) ? null : $"{serverUrl}/Images/Activities/{p.ImagePath}",
                                              Description = p.Description,
                                              CreatedAt = p.CreatedAt,
                                              IsLiked = isActivityLikedByActiveUser,
                                              NumberOfLikes = p.Likes.Count,
                                              IsSaved = isActivitySavedByActiveUser,
                                              Comments = p.Comments
                                                .Where(c => c.ActivityId == p.Id)
                                                .OrderByDescending(c => c.DateOfCreation)
                                                .Take(3)
                                                .Select(c => new CommentToResponse()
                                                {
                                                    CommentId = c.Id,
                                                    ActivityId = c.ActivityId,
                                                    UserId = c.UserId,
                                                    UserEmail = c.User.Email,
                                                    UserFirstName = c.User.FirstName,
                                                    UserLastName = c.User.LastName,
                                                    UserImagePath = $"{serverUrl}/Images/UserProfiles/{c.User.ImagePath}",
                                                    DateOfCreation = c.DateOfCreation,
                                                    Content = c.Content
                                                }).ToList()
                                          })
                                          .FirstOrDefaultAsync(p => p.Id == guidIdOfActivity);

                return postToResponse;
            }
        }

        public async Task<List<ActivityCardToResponse>> GetLastTwoActivities(string userId)
        {
            string serverUrl = _configuration.GetValue<string>("ServerUrl");

            var lastTwoActivities = await _db.Activities
                .Where(a => a.UserId == Guid.Parse(userId))
                .OrderByDescending(a => a.CreatedAt)
                .Take(2)
                .Select(a => new ActivityCardToResponse
                {
                    Id = a.Id,
                    ImagePathOfCreator = $"{serverUrl}/Images/UserProfiles/{a.User.ImagePath}",
                    FirstNameOfCreator = a.User.FirstName,
                    LastNameOfCreator = a.User.LastName,
                    PublicationDate = a.CreatedAt,
                    Title = a.Title,
                    Description = a.Description,
                    ActivityType = a is Event ? "Event" :
                                   a is Survey ? "Survey" :
                                   a is QA ? "QA" :
                                   a is Post ? "Post" :
                                   "Unknown"
                                   })
                .ToListAsync();

            return lastTwoActivities;
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
                    Size = new Size(560, 280),
                    Mode = ResizeMode.Crop
                }));

                await imageResult.SaveAsync(imagePathToCreateFile, new JpegEncoder
                {
                    Quality = 95
                });
            }

            return uniqueFileName;
        }
    }
}
