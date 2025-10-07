using FindACoach.Core.Domain.Entities.Activity;
using FindACoach.Core.Domain.IdentityEntities;
using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.Forum;
using FindACoach.Core.ServiceContracts.Forum.Activities;
using Microsoft.AspNetCore.Identity;

namespace FindACoach.Core.Services.Forum.Activities
{
    public class ToggleSaveService : IToggleSaveService
    {
        private readonly IActivitiesGetterService _activitiesGetterService;
        private readonly UserManager<User> _userManager;
        private readonly ISavesRepository _savesRepository;

        public ToggleSaveService(IActivitiesGetterService activitiesGetterService, UserManager<User> userManager, ISavesRepository savesRepository)
        {
            _activitiesGetterService = activitiesGetterService;
            _userManager = userManager;
            _savesRepository = savesRepository;
        }

        public async Task<SaveInformationToResponse> ToggleSave(ToggleSaveDTO dto)
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

            Save saveOfUser = await _savesRepository.GetSave(dto.UserId, dto.ActivityId);

            if (saveOfUser != null)
            {
                return await _savesRepository.RemoveSave(saveOfUser);
            }
            else
            {
                return await _savesRepository.AddSave(dto.UserId, dto.ActivityId);
            }
        }
    }
}
