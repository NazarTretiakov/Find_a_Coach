using FindACoach.Core.Domain.Entities;
using FindACoach.Core.Domain.Entities.User;
using FindACoach.Core.Domain.IdentityEntities;
using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile.Certifications;
using FindACoach.Infrastructure.DbContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace FindACoach.Infrastructure.Repositories
{
    public class CertificationsRepository : ICertificationsRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public CertificationsRepository(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

        public async Task AddCertification(string userId, AddCertificationDTO dto)
        {
            Certification certification = new Certification()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse(userId),
                Title = dto.Title,
                IssuingOrganization = dto.IssuingOrganization,
                IssueDate = dto.IssueDate,
                CredentialId = dto.CredentialId,
                CredentialUrl = dto.CredentialUrl
            };

            certification.ImagePath = await ChangeCertificationImage(certification, dto.Image);

            foreach (var skillTitle in dto.SkillTitles)
            {
                Skill skill = await _db.Skills.FirstOrDefaultAsync(s => s.Title == skillTitle);

                if (skill != null)
                {
                    certification.Skills.Add(skill);
                }
                else
                {
                    skill = new Skill
                    {
                        Id = Guid.NewGuid(),
                        Title = skillTitle
                    };
                    _db.Skills.Add(skill);
                    certification.Skills.Add(skill);
                }
            }

            await _db.AddAsync(certification);

            await _db.SaveChangesAsync();
        }

        private async Task<string> ChangeCertificationImage(Certification certification, IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return "";
            }

            string certificationImagesFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Certifications");
            string oldProfileImagePath = Path.Combine(certificationImagesFolder, certification.ImagePath);

            if (File.Exists(oldProfileImagePath))
            {
                File.Delete(oldProfileImagePath);
            }

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
            string imagePathToCreateFile = Path.Combine(certificationImagesFolder, uniqueFileName);

            using (var imageStream = image.OpenReadStream())
            using (var imageResult = Image.Load(imageStream))
            {
                imageResult.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(400, 280),
                    Mode = ResizeMode.Crop
                }));

                await imageResult.SaveAsync(imagePathToCreateFile, new JpegEncoder
                {
                    Quality = 90
                });
            }

            return uniqueFileName;
        }

        public Task DeleteCertification(string certificationId, string activeUserId)
        {
            throw new NotImplementedException();
        }

        public Task EditCertification(EditCertificationDTO dto, string editorId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CertificationToResponse>> GetAllCertifications(string userId)
        {
            string serverUrl = _configuration.GetValue<string>("ServerUrl");

            List<CertificationToResponse> certifications = await _db.Certifications
                .Where(c => c.UserId == Guid.Parse(userId))
                .OrderByDescending(s => s.IssueDate)
                .Select(c => new CertificationToResponse()
                {
                    CertificationId = c.Id.ToString(),
                    Title = c.Title,
                    IssuingOrganization = c.IssuingOrganization,
                    IssueDate = c.IssueDate,
                    CredentialId = c.CredentialId,
                    CredentialUrl = c.CredentialUrl,
                    ImagePath = string.IsNullOrEmpty(c.ImagePath) ? null : $"{serverUrl}/Images/Certifications/{c.ImagePath}",
                    SkillTitles = c.Skills.Select(s => s.Title).ToList()
                })
                .ToListAsync();

            return certifications;
        }

        public async Task<CertificationToResponse> GetCertification(string certificationId)
        {
            string serverUrl = _configuration.GetValue<string>("ServerUrl");

            CertificationToResponse certification = await _db.Certifications
                .Where(c => c.Id == Guid.Parse(certificationId))
                .OrderByDescending(s => s.IssueDate)
                .Select(c => new CertificationToResponse()
                {
                    CertificationId = c.Id.ToString(),
                    Title = c.Title,
                    IssuingOrganization = c.IssuingOrganization,
                    IssueDate = c.IssueDate,
                    CredentialId = c.CredentialId,
                    CredentialUrl = c.CredentialUrl,
                    ImagePath = string.IsNullOrEmpty(c.ImagePath) ? null : $"{serverUrl}/Images/Certifications/{c.ImagePath}",
                    SkillTitles = c.Skills.Select(s => s.Title).ToList()
                })
                .FirstOrDefaultAsync(c => c.CertificationId == certificationId);

            return certification;
        }

        public async Task<List<CertificationToResponse>> GetLastTwoCertifications(string userId)
        {
            string serverUrl = _configuration.GetValue<string>("ServerUrl");

            List<CertificationToResponse> certifications = await _db.Certifications
                .Where(c => c.UserId == Guid.Parse(userId))
                .OrderByDescending(s => s.IssueDate)
                .Take(2)
                .Select(c => new CertificationToResponse()
                {
                    CertificationId = c.Id.ToString(),
                    Title = c.Title,
                    IssuingOrganization = c.IssuingOrganization,
                    IssueDate = c.IssueDate,
                    CredentialId = c.CredentialId,
                    CredentialUrl = c.CredentialUrl,
                    ImagePath = string.IsNullOrEmpty(c.ImagePath) ? null : $"{serverUrl}/Images/Certifications/{c.ImagePath}",
                    SkillTitles = c.Skills.Select(s => s.Title).ToList()
                })
                .ToListAsync();

            return certifications;
        }
    }
}
