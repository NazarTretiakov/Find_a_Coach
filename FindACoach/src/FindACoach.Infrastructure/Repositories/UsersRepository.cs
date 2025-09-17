using FindACoach.Core.Domain.Entities;
using FindACoach.Core.Domain.IdentityEntities;
using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO;
using FindACoach.Infrastructure.DbContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FindACoach.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UsersRepository(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
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

            try
            {
                await _db.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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

            using (var fileStream = new FileStream(imagePathToCreateFile, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
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
    }
}
