using FindACoach.Core.Domain.Entities.Activity;
using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.Forum;
using FindACoach.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FindACoach.Infrastructure.Repositories
{
    public class QAAnswersRepository : IQAAnswersRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _db;

        public QAAnswersRepository(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public async Task Add(QAAnswer QAAnswer)
        {
            _db.QAAnswers.Add(QAAnswer);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch(Exception e)
            {

            }
        }

        public Task<List<QAAnswerToResponse>> GetQAAnswers(string QAId)
        {
            string serverUrl = _configuration.GetValue<string>("ServerUrl");

            var QAAnswers = _db.QAAnswers
                .Where(a => a.QAId == Guid.Parse(QAId))
                .Select(a => new QAAnswerToResponse()
                {
                    Content = a.Content,
                    QAId = a.QAId,
                    CreatorId = a.UserId,
                    CreatorFirstName = a.User.FirstName,
                    CreatorLastName = a.User.LastName,
                    DateOfCreation = a.DateOfCreation,
                    CreatorProfileImagePath = $"{serverUrl}/Images/UserProfiles/{a.User.ImagePath}"
                })
                .ToListAsync();

            return QAAnswers;
        }
    }
}