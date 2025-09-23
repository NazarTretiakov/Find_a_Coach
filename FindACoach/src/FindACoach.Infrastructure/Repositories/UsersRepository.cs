using FindACoach.Core.Domain.Entities;
using FindACoach.Core.Domain.IdentityEntities;
using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile;
using FindACoach.Infrastructure.DbContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace FindACoach.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public UsersRepository(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

        public async Task ChangeCompleteProfileWindowState(string userId, bool isVisible, string title)
        {
            User activeUser = await _db.Users.FirstOrDefaultAsync(u => u.Id == Guid.Parse(userId));

            activeUser.IsCompleteProfileWindowVisible = isVisible;
            activeUser.CompleteProfileWindowTitle = title;

            await _db.SaveChangesAsync();
        }

        public async Task EditPersonalInformation(string userId, EditPersonalInformationDTO dto)
        {
            User activeUser = await _db.Users.SingleOrDefaultAsync(u => u.Id == Guid.Parse(userId));

            if (activeUser is null)
            {
                throw new Exception("User not found");
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
            if (image == null || image.Length == 0)
            {
                return "default-profile-image.png";
            }

            string profileImagesFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "ProfileImages");
            string oldProfileImagePath = Path.Combine(profileImagesFolder, activeUser.ImagePath);

            if (File.Exists(oldProfileImagePath) && activeUser.ImagePath != "default-profile-image.png")
            {
                File.Delete(oldProfileImagePath);
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
            User activeUser = await _db.Users.SingleOrDefaultAsync(u => u.Id == Guid.Parse(userId));

            if (activeUser is null)
            {
                throw new Exception("User not found");
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
            User activeUser = await _db.Users.SingleOrDefaultAsync(u => u.Id == Guid.Parse(userId));

            string serverUrl = _configuration.GetValue<string>("ServerUrl");

            PersonalInformationToResponse personalInformation = new PersonalInformationToResponse()
            {
                ProfileImageUrl = $"{serverUrl}/Images/ProfileImages/{activeUser.ImagePath}",
                FirstName = activeUser.FirstName,
                LastName = activeUser.LastName,
                PrimaryOccupation = activeUser.PrimaryOccupation,
                Headline = activeUser.Headline,
                Location = activeUser.Location,
                ConnectionsAmount = activeUser.Connections.Count()
            };

            return personalInformation;
        }

        public async Task<PersonalAndContactInformationToResponse> GetPersonalAndContactInformation(string userId)
        {
            var activeUser = await _db.Users.Where(u => u.Id == Guid.Parse(userId))
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
                ProfileImageUrl = $"{serverUrl}/Images/ProfileImages/{activeUser.ImagePath}",
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
            User user =  await _db.Users.SingleOrDefaultAsync(u => u.Id == Guid.Parse(userId));
            return new AboutMeDTO() { AboutMe = user.AboutMe };
        }

        public async Task EditAboutMe(string userId, AboutMeDTO dto)
        {
            User user = await _db.Users.SingleOrDefaultAsync(u => u.Id == Guid.Parse(userId));

            user.AboutMe = dto.AboutMe;

            await _db.SaveChangesAsync();
        }
    }   
}