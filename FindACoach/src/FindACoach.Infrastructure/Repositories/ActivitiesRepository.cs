using FindACoach.Core.Domain.Entities;
using FindACoach.Core.Domain.Entities.Activity;
using FindACoach.Core.Domain.IdentityEntities;
using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.Forum;
using FindACoach.Core.DTO.MyProfile.Activities;
using FindACoach.Core.Enums;
using FindACoach.Infrastructure.DbContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System;
using System.Linq.Expressions;
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
        private readonly UserManager<User> _userManager;

        public ActivitiesRepository(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ILikesRepository likesRepository, ISavesRepository savesRepository, UserManager<User> userManager)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _likesRepository = likesRepository;
            _savesRepository = savesRepository;
            _userManager = userManager;
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
                        skill = new Skill
                        {
                            Id = Guid.NewGuid(),
                            Title = skillTitle
                        };
                        _db.Skills.Add(skill);
                        panelEntity.PreferredSkills.Add(skill);
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
            if (!Guid.TryParse(activityId, out var activityGuid))
            {
                throw new ArgumentException("Invalid activity id.");
            }

            if (!Guid.TryParse(userId, out var userGuid))
            {
                throw new ArgumentException("Invalid user id.");
            }

            var activity = await _db.Activities
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == activityGuid);

            if (activity == null)
            {
                throw new ArgumentNullException("Activity not found.");
            }

            var admins = await _userManager.GetUsersInRoleAsync(UserRoleOptions.Admin.ToString());

            var isAdmin = admins.Any(a => a.Id == userGuid);

            if (activity.User.Id != userGuid && !isAdmin)
            {
                throw new ArgumentException("Only creator or admin can delete this activity.");
            }

            if (activity is Event eventActivity)
            {
                await _db.Entry(eventActivity)
                    .Collection(e => e.SearchPersonPanels)
                    .LoadAsync();

                var eventApplications = await _db.EventApplications
                   .Where(a => eventActivity.SearchPersonPanels.Contains(a.SearchPersonPanel))
                   .ToListAsync(); 

                var notificationsRelatedToActivity = await _db.Notifications
                   .Where(n => eventApplications.Select(a => a.Id).Contains(n.NotifiedObjectId))
                   .ToListAsync();

                _db.Notifications.RemoveRange(notificationsRelatedToActivity);

            } else if (activity is QA qaActivity)
            {
                await _db.Entry(qaActivity)
                    .Collection(qa => qa.Answers)
                    .LoadAsync();

                var notificationsRelatedToActivity = await _db.Notifications
                   .Where(n => qaActivity.Answers.Select(a => a.Id).Contains(n.NotifiedObjectId))
                   .ToListAsync();

                _db.Notifications.RemoveRange(notificationsRelatedToActivity);
            }

            _db.Activities.Remove(activity);

            await _db.SaveChangesAsync();
        }

        public async Task<ActivitiesPagedToResponse> GetActivitiesPaged(string userId, int page, int pageSize)
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

            var activitiesPagedToResponse = new ActivitiesPagedToResponse();
            activitiesPagedToResponse.Activities = userActivities;
            activitiesPagedToResponse.IsMoreActivitiesLeft = _db.Activities
                .Where(a => a.UserId == Guid.Parse(userId))
                .Skip(page * pageSize)
                .Any();

            return activitiesPagedToResponse;
        }

        public async Task<ActivitiesPagedToResponse> GetFilteredActivitiesPaged(string userId, int page, int pageSize, string searchString)
        {
            string serverUrl = _configuration.GetValue<string>("ServerUrl");

            var lowerSearch = searchString.ToLower();

            var userActivities = await _db.Activities
                .Where(a => a.UserId == Guid.Parse(userId) && !a.User.IsBlocked)
                .Where(a =>
                    a.Title.ToLower().Contains(lowerSearch) ||
                    a.Subjects.Any(s => s.Title.ToLower().Contains(lowerSearch)) ||
                    _db.Activities
                        .OfType<Event>()
                        .Where(e => e.Id == a.Id)
                        .Any(e =>
                            e.SearchPersonPanels.Any(p =>
                                p.PositionName.ToLower().Contains(lowerSearch) ||
                                p.PreferredSkills.Any(ps =>
                                    ps.Title.ToLower().Contains(lowerSearch)
                                )
                            )
                        )
                )
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
                    ImagePath = string.IsNullOrEmpty(a.ImagePath)
                        ? null
                        : $"{serverUrl}/Images/Activities/{a.ImagePath}",
                    Description = a.Description,
                    ActivityType =
                        a is Event ? "Event" :
                        a is Survey ? "Survey" :
                        a is QA ? "QA" :
                        a is Post ? "Post" :
                        "Unknown"
                })
                .ToListAsync();

            var activitiesPagedToResponse = new ActivitiesPagedToResponse();
            activitiesPagedToResponse.Activities = userActivities;
            activitiesPagedToResponse.IsMoreActivitiesLeft = _db.Activities
                .Where(a => a.UserId == Guid.Parse(userId) && !a.User.IsBlocked)
                .Where(a =>
                    a.Title.ToLower().Contains(lowerSearch) ||
                    a.Subjects.Any(s => s.Title.ToLower().Contains(lowerSearch)) ||
                    _db.Activities
                        .OfType<Event>()
                        .Where(e => e.Id == a.Id)
                        .Any(e =>
                            e.SearchPersonPanels.Any(p =>
                                p.PositionName.ToLower().Contains(lowerSearch) ||
                                p.PreferredSkills.Any(ps =>
                                    ps.Title.ToLower().Contains(lowerSearch)
                                )
                            )
                        )
                )
                .Skip(page * pageSize)
                .Any();

            return activitiesPagedToResponse;
        }

        public async Task<ActivitiesPagedToResponse> GetRecommendedActivitiesPaged(string userId, int page, int pageSize)
        {
            string serverUrl = _configuration.GetValue<string>("ServerUrl");

            User user = await _userManager.Users
                .Where(u => u.Id == Guid.Parse(userId))
                .Select(u => new User()
                {
                    Id = u.Id,
                    PrimaryOccupation = u.PrimaryOccupation,
                    Skills = u.Skills
                })
                .FirstOrDefaultAsync(u => u.Id == Guid.Parse(userId));

            if (user == null)
            {
                throw new UnauthorizedAccessException("User with supplied id donesn't exist.");
            }

            var recommendedActivities = await _db.Activities
                .Where(a => (
                    a.Title.ToLower().Contains(user.PrimaryOccupation.ToLower()) ||
                    a.Subjects.Any(s => s.Title.ToLower().Contains(user.PrimaryOccupation.ToLower())) ||
                    a.Description.ToLower().Contains(user.PrimaryOccupation.ToLower()) ||
                    user.Skills
                        .Select(skill => skill.Title)
                        .Any(skillTitle => a.Title.ToLower().Contains(skillTitle.ToLower())) ||
                    user.Skills
                        .Select(skill => skill.Title)
                        .Any(skillTitle => a.Subjects.Any(s => s.Title.ToLower().Contains(skillTitle.ToLower()))) ) &&
                   !a.User.IsBlocked &&
                   a.UserId != Guid.Parse(userId)
                )
                .OrderByDescending(a => a.Likes.Count)
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

            var activitiesPagedToResponse = new ActivitiesPagedToResponse();
            activitiesPagedToResponse.Activities = recommendedActivities;
            activitiesPagedToResponse.IsMoreActivitiesLeft = _db.Activities
                .Where(a => (
                    a.Title.ToLower().Contains(user.PrimaryOccupation.ToLower()) ||
                    a.Subjects.Any(s => s.Title.ToLower().Contains(user.PrimaryOccupation.ToLower())) ||
                    a.Description.ToLower().Contains(user.PrimaryOccupation.ToLower()) ||
                    user.Skills
                        .Select(skill => skill.Title)
                        .Any(skillTitle => a.Title.ToLower().Contains(skillTitle.ToLower())) ||
                    user.Skills
                        .Select(skill => skill.Title)
                        .Any(skillTitle => a.Subjects.Any(s => s.Title.ToLower().Contains(skillTitle.ToLower())))) &&
                   !a.User.IsBlocked &&
                   a.UserId != Guid.Parse(userId)
                )
                .Skip(page * pageSize)
                .Any();

            if (recommendedActivities.Count < 6)
            {
                var recommendedIds = recommendedActivities.Select(u => u.Id).ToList();

                var activities = await _db.Activities
                    .Where(a => !recommendedIds.Contains(a.Id) && !a.User.IsBlocked && a.UserId != Guid.Parse(userId))
                    .OrderByDescending(a => a.Likes.Count)
                    .OrderByDescending(a => a.CreatedAt)
                    .Take(6 - recommendedActivities.Count)
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

                recommendedActivities.AddRange(activities);
            }

            return activitiesPagedToResponse;
        }

        public async Task<ActivitiesPagedToResponse> GetFilteredRecommendedActivitiesPaged(int page, int pageSize, string searchString)
        {
            string serverUrl = _configuration.GetValue<string>("ServerUrl");

            string lowerSearch = searchString.ToLower();

            var userActivities = await _db.Activities
                .Where(a =>
                    a.Title.ToLower().Contains(lowerSearch) ||
                    a.Subjects.Any(s => s.Title.ToLower().Contains(lowerSearch)) ||
                    _db.Activities
                        .OfType<Event>()
                        .Where(e => e.Id == a.Id)
                        .Any(e =>
                            e.SearchPersonPanels.Any(p =>
                                p.PositionName.ToLower().Contains(lowerSearch) ||
                                p.PreferredSkills.Any(ps =>
                                    ps.Title.ToLower().Contains(lowerSearch)
                                )
                            )
                        )
                )
                .OrderByDescending(a => a.Likes.Count)
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

            var activitiesPagedToResponse = new ActivitiesPagedToResponse();
            activitiesPagedToResponse.Activities = userActivities;
            activitiesPagedToResponse.IsMoreActivitiesLeft = _db.Activities
                .Where(a =>
                    a.Title.ToLower().Contains(lowerSearch) ||
                    a.Subjects.Any(s => s.Title.ToLower().Contains(lowerSearch)) ||
                    _db.Activities
                        .OfType<Event>()
                        .Where(e => e.Id == a.Id)
                        .Any(e =>
                            e.SearchPersonPanels.Any(p =>
                                p.PositionName.ToLower().Contains(lowerSearch) ||
                                p.PreferredSkills.Any(ps =>
                                    ps.Title.ToLower().Contains(lowerSearch)
                                )
                            )
                        )
                )
                .Skip(page * pageSize)
                .Any();

            return activitiesPagedToResponse;
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
                                                                        .Where(c => c.ActivityId == e.Id && !c.User.IsBlocked)
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
                                                                        }).ToList(),
                                                                      IsMoreCommentsLeft = e.Comments.Count(c => c.ActivityId == e.Id) > 3
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
                                                                            .Where(c => c.ActivityId == s.Id && !s.User.IsBlocked)
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
                                                                 .Where(c => c.ActivityId == qa.Id && !qa.User.IsBlocked)
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
                                                .Where(c => c.ActivityId == p.Id && !c.User.IsBlocked)
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

        public async Task<ActivitiesPagedToResponse> GetAllActivities(int page, int pageSize)
        {
            string serverUrl = _configuration.GetValue<string>("ServerUrl");

            var userActivities = await _db.Activities
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

            var activitiesPagedToResponse = new ActivitiesPagedToResponse();
            activitiesPagedToResponse.Activities = userActivities;
            activitiesPagedToResponse.IsMoreActivitiesLeft = _db.Activities
                .Skip(page * pageSize)
                .Any();

            return activitiesPagedToResponse;
        }

        public async Task<ActivitiesPagedToResponse> GetSavedActivitiesPaged(string userId, int page, int pageSize)
        {
            string serverUrl = _configuration.GetValue<string>("ServerUrl");
            var userGuid = Guid.Parse(userId);

            var result = await _db.Saves
                .Where(s => s.UserId == userGuid && !s.Activity.User.IsBlocked)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new ActivityForActivitiesListToResponse
                {
                    Id = s.Activity.Id,
                    ImagePathOfCreator = $"{serverUrl}/Images/UserProfiles/{s.Activity.User.ImagePath}",
                    FirstNameOfCreator = s.Activity.User.FirstName,
                    LastNameOfCreator = s.Activity.User.LastName,
                    PublicationDate = s.Activity.CreatedAt,
                    Title = s.Activity.Title,
                    Subjects = s.Activity.Subjects.Select(sub => sub.Title).ToList(),
                    ImagePath = string.IsNullOrEmpty(s.Activity.ImagePath)
                        ? null
                        : $"{serverUrl}/Images/Activities/{s.Activity.ImagePath}",
                    Description = s.Activity.Description,
                    ActivityType =
                        s.Activity is Event ? "Event" :
                        s.Activity is Survey ? "Survey" :
                        s.Activity is QA ? "QA" :
                        s.Activity is Post ? "Post" :
                        "Unknown"
                })
                .ToListAsync();

            var activitiesPagedToResponse = new ActivitiesPagedToResponse();
            activitiesPagedToResponse.Activities = result;
            activitiesPagedToResponse.IsMoreActivitiesLeft = _db.Saves
                .Where(s => s.UserId == userGuid && !s.Activity.User.IsBlocked)
                .Skip(page * pageSize)
                .Any();

            return activitiesPagedToResponse;
        }
    }
}
