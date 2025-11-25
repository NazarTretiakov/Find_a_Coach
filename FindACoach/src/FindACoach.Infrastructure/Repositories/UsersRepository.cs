using FindACoach.Core.Domain.Entities;
using FindACoach.Core.Domain.IdentityEntities;
using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.Admin;
using FindACoach.Core.DTO.MyProfile;
using FindACoach.Core.DTO.MyProfile.Settings;
using FindACoach.Core.DTO.Network;
using FindACoach.Core.Enums;
using FindACoach.Core.ServiceContracts.Network;
using FindACoach.Infrastructure.DbContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System.Security.Claims;

namespace FindACoach.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIsUsersConnectedService _isUsersConnectedService;

        public UsersRepository(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, IIsUsersConnectedService isUsersConnectedService)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _isUsersConnectedService = isUsersConnectedService;
        }

        public async Task ChangeCompleteProfileWindowState(string userId, bool isVisible, string title)
        {
            User activeUser = await _userManager.Users.FirstAsync(u => u.Id == Guid.Parse(userId));

            activeUser.IsCompleteProfileWindowVisible = isVisible;
            activeUser.CompleteProfileWindowTitle = title;

            await _db.SaveChangesAsync();
        }

        public async Task EditPersonalInformation(string userId, EditPersonalInformationDTO dto)
        {
            User activeUser = await _userManager.Users.FirstAsync(u => u.Id == Guid.Parse(userId));

            if (activeUser == null)
            {
                throw new UnauthorizedAccessException("User with supplied id donesn't exist.");
            }

            activeUser.FirstName = dto.Firstname;
            activeUser.LastName = dto.Lastname;
            activeUser.ImagePath = await ChangeProfileImage(activeUser, dto.ProfileImage);
            activeUser.PrimaryOccupation = dto.PrimaryOccupation;
            activeUser.Headline = dto.Headline;
            activeUser.Location = dto.Location;
            activeUser.Phone = dto.Phone;


            _db.Websites.RemoveRange(_db.Websites.Where(w => w.UserId == activeUser.Id));

            foreach (WebsiteDTO website in dto.Websites)
            {
                await _db.Websites.AddAsync(new Website()
                {
                    Id = Guid.NewGuid(),
                    UserId = activeUser.Id,
                    Url = website.Url,
                    Type = website.Type
                });
            }

            await _db.SaveChangesAsync();
        }

        private async Task<string> ChangeProfileImage(User activeUser, IFormFile image)
        {
            string profileImagesFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "UserProfiles");
            string oldProfileImagePath = Path.Combine(profileImagesFolder, activeUser.ImagePath);

            if (File.Exists(oldProfileImagePath) && activeUser.ImagePath != "default-profile-image.png")
            {
                File.Delete(oldProfileImagePath);
            }

            if (image == null || image.Length == 0)
            {
                return "default-profile-image.png";
            }

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
            string imagePathToCreateFile = Path.Combine(profileImagesFolder, uniqueFileName);

            using (var imageStream = image.OpenReadStream())
            using (var imageResult = Image.Load(imageStream))
            {
                imageResult.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(400, 400),
                    Mode = ResizeMode.Crop
                }));

                await imageResult.SaveAsync(imagePathToCreateFile, new JpegEncoder
                {
                    Quality = 90
                });
            }

            return uniqueFileName;
        }

        public async Task<CompleteProfileWindowStateDTO> GetCompleteProfileWindowState(string userId)
        {
            User activeUser = await _userManager.Users.FirstAsync(u => u.Id == Guid.Parse(userId));

            if (activeUser == null)
            {
                throw new UnauthorizedAccessException("User with supplied id donesn't exist.");
            }

            CompleteProfileWindowStateDTO completeProfileWindowStateDTO = new CompleteProfileWindowStateDTO()
            {
                IsVisible = activeUser.IsCompleteProfileWindowVisible,
                Title = activeUser.CompleteProfileWindowTitle
            };

            return completeProfileWindowStateDTO;
        }

        public async Task<PersonalInformationToResponse> GetPersonalInformation(string userId)
        {
            User user = await _userManager.Users.FirstAsync(u => u.Id == Guid.Parse(userId));

            var principal = _httpContextAccessor.HttpContext?.User;
            if (principal == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }

            string? activeUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (activeUserId == null)
            {
                throw new UnauthorizedAccessException("Cannot resolve user id from claims");
            }

            IsUsersConnectedInfoToResponse isUsersConnectedInfo;

            if (userId == activeUserId)
            {
                isUsersConnectedInfo = new IsUsersConnectedInfoToResponse()
                {
                    IsUsersConnected = false,
                    Status = null
                };
            }
            else
            {
                isUsersConnectedInfo = await _isUsersConnectedService.IsUsersConnected(userId, activeUserId);
            }

            string serverUrl = _configuration.GetValue<string>("ServerUrl");

            PersonalInformationToResponse personalInformation = new PersonalInformationToResponse()
            {
                ProfileImageUrl = $"{serverUrl}/Images/UserProfiles/{user.ImagePath}",
                FirstName = user.FirstName,
                LastName = user.LastName,
                PrimaryOccupation = user.PrimaryOccupation,
                Headline = user.Headline,
                Location = user.Location,
                ConnectionsAmount = _db.Connections.Where(c => c.UserId == Guid.Parse(userId) || c.ConnectedUserId == Guid.Parse(userId)).Count(),
                IsConnected = isUsersConnectedInfo.IsUsersConnected,
                ConnectionStatus = isUsersConnectedInfo.Status
            };

            return personalInformation;
        }

        public async Task<PersonalAndContactInformationToResponse> GetPersonalAndContactInformation(string userId)
        {
            var activeUser = await _userManager.Users.Where(u => u.Id == Guid.Parse(userId))
                                            .Select(u => new User
                                            {
                                                Id = u.Id,
                                                FirstName = u.FirstName,
                                                LastName = u.LastName,
                                                PrimaryOccupation = u.PrimaryOccupation,
                                                Headline = u.Headline,
                                                Location = u.Location,
                                                Phone = u.Phone,
                                                ImagePath = u.ImagePath,
                                                Websites = u.Websites.Select(w => new Website
                                                                     {
                                                                        Url = w.Url,
                                                                        Type = w.Type
                                                                     })
                                                                     .ToList()
                                            })
                                            .AsNoTracking()
                                            .SingleOrDefaultAsync();

            string serverUrl = _configuration.GetValue<string>("ServerUrl");

            PersonalAndContactInformationToResponse personalInformation = new PersonalAndContactInformationToResponse()
            {
                ProfileImageUrl = $"{serverUrl}/Images/UserProfiles/{activeUser.ImagePath}",
                FirstName = activeUser.FirstName,
                LastName = activeUser.LastName,
                PrimaryOccupation = activeUser.PrimaryOccupation,
                Headline = activeUser.Headline,
                Location = activeUser.Location,
                Phone = activeUser.Phone,
                Websites = activeUser.Websites.Select(w => new WebsiteDTO()
                {
                    Url = w.Url,
                    Type = w.Type
                }).ToList()
            };

            return personalInformation;
        }

        public async Task<AboutMeDTO> GetAboutMe(string userId)
        {
            User user =  await _userManager.Users.SingleOrDefaultAsync(u => u.Id == Guid.Parse(userId));
            return new AboutMeDTO() { AboutMe = user.AboutMe };
        }

        public async Task EditAboutMe(string userId, AboutMeDTO dto)
        {
            User user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == Guid.Parse(userId));

            user.AboutMe = dto.AboutMe;

            await _db.SaveChangesAsync();
        }

        public async Task<IsProfileSectionsCompletedToResponse> IsProfileSectionsCompleted(string activeUserId)
        {
            var isProfileSectionsCompleted = new IsProfileSectionsCompletedToResponse()
            {
                IsCoreInfo = true,
                IsDescription = true,
                IsActivities = true,
                IsEducation = true,
                IsExperience = true,
                IsProjects = true,
                IsCertifications = true,
                IsLanguages = true,
                IsSkills = true
            };

            User activeUser = await _userManager.Users
                .Where(u => u.Id == Guid.Parse(activeUserId))
                .Include(u => u.Activities)
                .Include(u => u.Positions)
                .Include(u => u.Schools)
                .Include(u => u.Projects)
                .Include(u => u.Certifications)
                .Include(u => u.Skills)
                .Include(u => u.Languages)
                .FirstOrDefaultAsync(u => u.Id == Guid.Parse(activeUserId));
            if (activeUser == null)
            {
                throw new UnauthorizedAccessException("User with supplied id donesn't exist.");
            }

            if (activeUser.FirstName.IsNullOrEmpty() ||
                activeUser.LastName.IsNullOrEmpty() ||
                activeUser.Headline.IsNullOrEmpty() ||
                activeUser.PrimaryOccupation.IsNullOrEmpty() ||
                activeUser.Phone.IsNullOrEmpty() ||
                activeUser.Location.IsNullOrEmpty())
            {
                isProfileSectionsCompleted.IsCoreInfo = false;
            }
            if (activeUser.AboutMe.IsNullOrEmpty())
            {
                isProfileSectionsCompleted.IsDescription = false;
            }
            if (activeUser.Activities.IsNullOrEmpty())
            {
                isProfileSectionsCompleted.IsActivities = false;
            }
            if (activeUser.Positions.IsNullOrEmpty())
            {
                isProfileSectionsCompleted.IsExperience = false;
            }
            if (activeUser.Schools.IsNullOrEmpty())
            {
                isProfileSectionsCompleted.IsEducation = false;
            }
            if (activeUser.Projects.IsNullOrEmpty())
            {
                isProfileSectionsCompleted.IsProjects = false;
            }
            if (activeUser.Certifications.IsNullOrEmpty())
            {
                isProfileSectionsCompleted.IsCertifications = false;
            }
            if (activeUser.Skills.IsNullOrEmpty())
            {
                isProfileSectionsCompleted.IsSkills = false;
            }
            if (activeUser.Languages.IsNullOrEmpty())
            {
                isProfileSectionsCompleted.IsLanguages = false;
            }

            return isProfileSectionsCompleted;
        }

        public async Task<ContactInformationToResponse> GetContactInformation(string userId)
        {
            var contactInformation = await _userManager.Users
                .Where(u => u.Id == Guid.Parse(userId))
                .Select(u => new ContactInformationToResponse()
                {
                    Email = u.Email,
                    Phone = u.Phone,
                    Websites = u.Websites.Select(w => new WebsiteDTO()
                    {
                        Url = w.Url,
                        Type = w.Type
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return contactInformation;
        }

        public async Task<ProfileImagePathToResponse> GetProfileImagePath(string userId)
        {
            string serverUrl = _configuration.GetValue<string>("ServerUrl");

            return await _userManager.Users
                .Where(u => u.Id == Guid.Parse(userId))
                .Select(u => new ProfileImagePathToResponse()
                {
                    ProfileImagePath = $"{serverUrl}/Images/UserProfiles/{u.ImagePath}"
                })
                .FirstAsync();
        }

        public async Task<ContactInformationVisibilityToResponse> GetContactInformationVisibility(string userId)
        {
            return await _userManager.Users
                .Where(u => u.Id == Guid.Parse(userId))
                .Select(u => new ContactInformationVisibilityToResponse()
                {
                    ContactInformationVisibilityType = u.ContactInformationVisibility.ToString()
                })
                .FirstAsync();
        }

        public async Task<ContactInformationVisibilityToResponse> EditContactInformationVisibility(string userId, string contactInformationVisibilityType)
        {
            var user = await _userManager.Users
                .Where(u => u.Id == Guid.Parse(userId))
                .FirstAsync();
            
            user.ContactInformationVisibility = Enum.Parse<ContactInfomationVisibility>(contactInformationVisibilityType);

            await _db.SaveChangesAsync();

            return new ContactInformationVisibilityToResponse() 
            { 
                ContactInformationVisibilityType = contactInformationVisibilityType
            };
        }

        public async Task<List<ConnectionToResponse>> GetFilteredUsers(string searchString, int page, int pageSize)
        {
            var loweredSearch = $"%{searchString.ToLower()}%";

            var users = await _userManager.Users
                .Where(u =>
                    EF.Functions.Like((u.FirstName + " " + u.LastName).ToLower(), loweredSearch)
                )
                .OrderByDescending(u => u.FirstName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var connectionsToResponse = new List<ConnectionToResponse>();

            var serverUrl = _configuration.GetValue<string>("ServerUrl");

            var principal = _httpContextAccessor.HttpContext?.User;
            if (principal == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }

            string? activeUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (activeUserId == null)
            {
                throw new UnauthorizedAccessException("Cannot resolve user id from claims");
            }

            foreach (var user in users)
            {
                if (user.Id != Guid.Parse(activeUserId))
                {
                    connectionsToResponse.Add(new ConnectionToResponse()
                    {
                        ConnectedUserId = user.Id,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Headline = user.Headline,
                        ImagePath = $"{serverUrl}/Images/UserProfiles/{user.ImagePath}",
                    });
                }
            }

            return connectionsToResponse;
        }

        public async Task<List<ConnectionToResponse>> GetRecommendedUsers(string userId, int page, int pageSize)
        {
            User activeUser = await _userManager.Users
                .Where(u => u.Id == Guid.Parse(userId))
                .Include(u => u.Skills)
                .FirstAsync();

            var locationPart = activeUser.Location.Split(',')[0].Trim().ToLower();
            var skillTitles = activeUser.Skills.Select(s => s.Title.ToLower()).ToList();

            var users = await _userManager.Users
                .Where(u => EF.Functions.Like(u.Location.ToLower(), $"%{locationPart}%") ||
                            u.Skills.Any(s => skillTitles.Contains(s.Title.ToLower())))
                .OrderByDescending(u => u.FirstName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var connectionsToResponse = new List<ConnectionToResponse>();

            var serverUrl = _configuration.GetValue<string>("ServerUrl");

            foreach (var user in users)
            {
                if (user.Id != activeUser.Id)
                {
                    connectionsToResponse.Add(new ConnectionToResponse()
                    {
                        ConnectedUserId = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Headline = user.Headline,
                        ImagePath = $"{serverUrl}/Images/UserProfiles/{user.ImagePath}",
                    });
                }
            }

            return connectionsToResponse;
        }

        public async Task<IsLoginNotificationEnabledDTO> GetIsLoginNotificationEnabled(string userId)
        {
            User activeUser = await _userManager.Users
                .Where(u => u.Id == Guid.Parse(userId))
                .Include(u => u.Skills)
                .FirstAsync();

            return new IsLoginNotificationEnabledDTO 
            {  
                IsLoginNotificationEnabled = activeUser.IsLoginNotificationEnabled 
            };
        }

        public async Task<IsLoginNotificationEnabledDTO> EditIsLoginNotificationEnabled(string userId, IsLoginNotificationEnabledDTO dto)
        {
            User activeUser = await _userManager.Users
                .Where(u => u.Id == Guid.Parse(userId))
                .Include(u => u.Skills)
                .FirstAsync();

            activeUser.IsLoginNotificationEnabled = dto.IsLoginNotificationEnabled;

            await _db.SaveChangesAsync();

            return new IsLoginNotificationEnabledDTO
            {
                IsLoginNotificationEnabled = activeUser.IsLoginNotificationEnabled
            };
        }

        public async Task<IsLoginNotificationEnabledDTO> EditSecuritySettings(string userId, EditSecuritySettingsDTO dto)
        {
            User activeUser = await _userManager.Users
                .Where(u => u.Id == Guid.Parse(userId))
                .Include(u => u.Skills)
                .FirstAsync();

            activeUser.IsLoginNotificationEnabled = dto.IsLoginNotificationEnabled;

            var isOldPasswordCorrect = await _userManager.CheckPasswordAsync(activeUser, dto.OldPassword);

            if (!isOldPasswordCorrect)
            {
                throw new UnauthorizedAccessException("Old password is incorrect.");
            }

            var result = await _userManager.ChangePasswordAsync(activeUser, dto.OldPassword, dto.NewPassword);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new ApplicationException($"Password change failed: {errors}");
            }

            await _db.SaveChangesAsync();

            return new IsLoginNotificationEnabledDTO
            {
                IsLoginNotificationEnabled = activeUser.IsLoginNotificationEnabled
            };
        }

        public async Task<List<UserToResponse>> GetAllUsers(int page, int pageSize)
        {
            var users = await _userManager.Users
                .OrderByDescending(u => u.Email)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var usersToResponse = new List<UserToResponse>();

            var serverUrl = _configuration.GetValue<string>("ServerUrl");

            var admins = await _userManager.GetUsersInRoleAsync(UserRoleOptions.Admin.ToString());

            foreach (var user in users)
            {
                if (!admins.Any(a => a.Id == user.Id))
                {
                    usersToResponse.Add(new UserToResponse()
                    {
                        UserId = user.Id,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        ImagePath = $"{serverUrl}/Images/UserProfiles/{user.ImagePath}",
                        IsBlocked = user.IsBlocked
                    });
                }
            }

            return usersToResponse;
        }

        public async Task<bool> ToggleBlock(string userId)
        {
            User user = await _userManager.Users.FirstAsync(u => u.Id == Guid.Parse(userId));

            user.IsBlocked = !user.IsBlocked;

            await _db.SaveChangesAsync();

            return user.IsBlocked;
        }
    }   
}