using FindACoach.Core.Domain.Entities.User;
using FindACoach.Core.Domain.IdentityEntities;
using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile.Languages;
using FindACoach.Core.Enums;
using FindACoach.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace FindACoach.Infrastructure.Repositories
{
    public class LanguagesRepository : ILanguagesRepository
    {
        private readonly ApplicationDbContext _db;

        public LanguagesRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddLanguage(string userId, AddLanguageDTO dto)
        {
            LanguageProficiency languageProficiency = Enum.Parse<LanguageProficiency>(dto.Proficiency);

            if (languageProficiency != LanguageProficiency.A1 && languageProficiency != LanguageProficiency.A2 && languageProficiency != LanguageProficiency.B1 && languageProficiency != LanguageProficiency.B2 && languageProficiency != LanguageProficiency.C1 && languageProficiency != LanguageProficiency.C2)
            {
                throw new ArgumentException("Given language proficiency is incorrect.");
            }

            Language language = new Language()
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                LanguageProficiency = languageProficiency,
                UserId = Guid.Parse(userId)
            };

            await _db.AddAsync(language);

            await _db.SaveChangesAsync();
        }

        public async Task DeleteLanguage(string languageId, string activeUserId)
        {
            Language language = await _db.Languages.FirstOrDefaultAsync(c => c.Id == Guid.Parse(languageId));

            if (language == null)
            {
                throw new ArgumentException("Language with specified id doesn't exist.");
            }

            if (language.UserId.ToString() != activeUserId)
            {
                throw new UnauthorizedAccessException("Only creator of language can delete the language.");
            }

            _db.Languages.Remove(language);

            await _db.SaveChangesAsync();
        }

        public async Task EditLanguage(EditLanguageDTO dto, string editorId)
        {
            Language language = await _db.Languages.FirstOrDefaultAsync(c => c.Id == Guid.Parse(dto.LanguageId));

            if (language == null)
            {
                throw new ArgumentException("Language with specified id doesn't exist.");
            }

            if (language.UserId.ToString() != editorId)
            {
                throw new UnauthorizedAccessException("Only creator of language can edit the language.");
            }

            LanguageProficiency languageProficiency = Enum.Parse<LanguageProficiency>(dto.Proficiency);

            if (languageProficiency != LanguageProficiency.A1 && languageProficiency != LanguageProficiency.A2 && languageProficiency != LanguageProficiency.B1 && languageProficiency != LanguageProficiency.B2 && languageProficiency != LanguageProficiency.C1 && languageProficiency != LanguageProficiency.C2)
            {
                throw new ArgumentException("Given language proficiency is incorrect.");
            }

            language.LanguageProficiency = languageProficiency;

            await _db.SaveChangesAsync();
        }

        public async Task<List<LanguageToResponse>> GetAllLanguages(string userId)
        {
            var languages = await _db.Languages
                .Where(l => l.UserId == Guid.Parse(userId))
                .OrderByDescending(l => l.LanguageProficiency)
                .Select(l => new LanguageToResponse()
                {
                    LanguageId = l.Id,
                    Title = l.Title,
                    Proficiency = l.LanguageProficiency.ToString()
                })
                .ToListAsync();

            return languages;
        }

        public async Task<LanguageToResponse> GetLanguage(string languageId)
        {
            var language = await _db.Languages
                .Where(l => l.Id == Guid.Parse(languageId))
                .Select(l => new LanguageToResponse()
                {
                    LanguageId = l.Id,
                    Title = l.Title,
                    Proficiency = l.LanguageProficiency.ToString()
                })
                .FirstOrDefaultAsync(l => l.LanguageId == Guid.Parse(languageId));

            return language;
        }

        public async Task<List<LanguageToResponse>> GetTwoLanguages(string userId)
        {
            var languages = await GetAllLanguages(userId);

            languages = languages.Take(2).ToList();

            return languages;
        }
    }
}
