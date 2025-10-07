using FindACoach.Core.Domain.Entities.Activity;
using FindACoach.Core.Domain.IdentityEntities;
using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.Forum;
using FindACoach.Core.ServiceContracts.Forum.Activities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

namespace FindACoach.Core.Services.Forum.Activities
{
    public class ToggleLikeService : IToggleLikeService
    {
        private readonly IActivitiesGetterService _activitiesGetterService;
        private readonly UserManager<User> _userManager;
        private readonly ILikesRepository _likesRepository;

        public ToggleLikeService(IActivitiesGetterService activitiesGetterService, UserManager<User> userManager, ILikesRepository likesRepository)
        {
            _activitiesGetterService = activitiesGetterService;
            _userManager = userManager;
            _likesRepository = likesRepository;
        }

        public async Task<LikesInformationToResponse> ToggleLike(ToggleLikeDTO dto)
        {
            var activity = await _activitiesGetterService.GetActivity(dto.ActivityId);
            if (activity == null)
            {
                throw new ArgumentException("Given activity id is not correct.");
            }

            User? user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null)
            {
                throw new ArgumentException("Given user id is not correct.");
            }

            Like likeOfUser = await _likesRepository.GetLike(dto.UserId, dto.ActivityId);

            if (likeOfUser != null)
            {
                return await _likesRepository.RemoveLike(likeOfUser);
            }
            else
            {
                return await _likesRepository.AddLike(dto.UserId, dto.ActivityId);
            }
        }
    }
}
