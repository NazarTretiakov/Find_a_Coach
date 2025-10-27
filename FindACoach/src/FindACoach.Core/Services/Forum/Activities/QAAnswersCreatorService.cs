using FindACoach.Core.Domain.Entities.Activity;
using FindACoach.Core.Domain.IdentityEntities;
using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.Forum;
using FindACoach.Core.ServiceContracts.Forum.Activities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace FindACoach.Core.Services.Forum.Activities
{
    public class QAAnswersCreatorService: IQAAnswersCreatorService
    {
        private readonly IQAAnswersRepository _QAAnswersRepository;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public QAAnswersCreatorService(IQAAnswersRepository QAAnswersRepository, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _QAAnswersRepository = QAAnswersRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task LeaveQAAnswer(LeaveQAAnswerDTO dto)
        {
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
            if (activeUserId != dto.CreatorId)
            {
                throw new UnauthorizedAccessException("You can't add answer's on QA for another users.");
            }

            QAAnswer answer = new QAAnswer()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse(dto.CreatorId),
                QAId = Guid.Parse(dto.QAId),
                Content = dto.Content,
                DateOfCreation = DateTime.Now
            };

            await _QAAnswersRepository.Add(answer);
        }
    }
}
