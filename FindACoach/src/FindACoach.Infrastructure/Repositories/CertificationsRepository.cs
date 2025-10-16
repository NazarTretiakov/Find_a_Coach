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
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
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

        public async Task DeleteCertification(string certificationId, string activeUserId)
        {
            Certification certification = await _db.Certifications.FirstOrDefaultAsync(s => s.Id == Guid.Parse(certificationId));

            if (certification == null)
            {
                throw new ArgumentException("Certification with specified id doesn't exist.");
            }

            if (certification.UserId.ToString() != activeUserId)
            {
                throw new UnauthorizedAccessException("Only creator of certification can edit the position.");
            }

            _db.Certifications.Remove(certification);

            await _db.SaveChangesAsync();
        }

        public async Task EditCertification(EditCertificationDTO dto, string editorId)
        {
            Certification certification = await _db.Certifications
                .Where(s => s.Id == Guid.Parse(dto.CertificationId))
                .Include(s => s.Skills)
                .FirstOrDefaultAsync(s => s.Id == Guid.Parse(dto.CertificationId));

            if (certification == null)
            {
                throw new ArgumentException("Certification with specified id doesn't exist.");
            }

            if (certification.UserId.ToString() != editorId)
            {
                throw new UnauthorizedAccessException("Only user which added that certification can edit it.");
            }

            certification.Title = dto.Title;
            certification.IssuingOrganization = dto.IssuingOrganization;
            certification.IssueDate = dto.IssueDate;
            certification.CredentialId = dto.CredentialId;
            certification.CredentialUrl = dto.CredentialUrl;
            certification.ImagePath = await ChangeCertificationImage(certification, dto.Image);

            certification.Skills.Clear();

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

            await _db.SaveChangesAsync();
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

        private async Task<string> ChangeCertificationImage(Certification certification, IFormFile image)
        {
            string certificationImagesFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Certifications");
            string oldProfileImagePath = Path.Combine(certificationImagesFolder, certification.ImagePath);

            if (File.Exists(oldProfileImagePath))
            {
                File.Delete(oldProfileImagePath);
            }

            if (image == null || image.Length == 0)
            {
                return "";
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
                    Quality = 100
                });
            }

            return uniqueFileName;
        }
    }
}
