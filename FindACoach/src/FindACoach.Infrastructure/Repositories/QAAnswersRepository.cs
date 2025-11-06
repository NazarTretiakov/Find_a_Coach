using FindACoach.Core.Domain.Entities.Activity;
using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.Forum;
using FindACoach.Core.Enums;
using FindACoach.Core.ServiceContracts.Network;
using FindACoach.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FindACoach.Infrastructure.Repositories
{
    public class QAAnswersRepository : IQAAnswersRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly INotificationsAdderService _notificationsAdderService;

        public QAAnswersRepository(ApplicationDbContext db, IConfiguration configuration, INotificationsAdderService notificationsAdderService)
        {
            _db = db;
            _configuration = configuration;
            _notificationsAdderService = notificationsAdderService;
        }

        public async Task Add(QAAnswer QAAnswer)
        {
            await _db.QAAnswers.AddAsync(QAAnswer);

            await _db.SaveChangesAsync();

            QAAnswer = await _db.QAAnswers
                .Where(a => a.Id == QAAnswer.Id)
                .Include(a => a.QA)
                .Include(a => a.User)
                .FirstAsync();

            await _notificationsAdderService.AddNotification(
                QAAnswer.QA.UserId.ToString(),
                $"{QAAnswer.User.FirstName} added an answer on your QA.",
                QAAnswer.Id.ToString(),
                NotificationType.QAAnswer);
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
                .OrderByDescending(a => a.DateOfCreation)
                .ToListAsync();

            return QAAnswers;
        }
    }
}