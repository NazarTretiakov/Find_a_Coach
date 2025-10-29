using FindACoach.Core.Domain.Entities.Activity;
using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.Forum;
using FindACoach.Core.ServiceContracts.Forum.Activities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FindACoach.Core.Services.Forum.Activities
{
    public class EventApplicationsCreatorService: IEventApplicationsCreatorService
    {
        private readonly IEventApplicationsRepository _eventApplicationsRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EventApplicationsCreatorService(IEventApplicationsRepository eventApplicationsRepository, IHttpContextAccessor httpContextAccessor)
        {
            _eventApplicationsRepository = eventApplicationsRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task ApplyOnEvent(ApplyOnEventDTO dto)
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
            if (activeUserId != dto.UserId)
            {
                throw new UnauthorizedAccessException("You can't apply on event as an another user.");
            }

            EventApplication application = new EventApplication()
            {
                Id = Guid.NewGuid(),
                SearchPersonPanelId = Guid.Parse(dto.SearchPersonPanelId),
                UserId = Guid.Parse(dto.UserId),
                DateOfCreation = DateTime.Now
            };

            await _eventApplicationsRepository.Add(application);
        }
    }
}
