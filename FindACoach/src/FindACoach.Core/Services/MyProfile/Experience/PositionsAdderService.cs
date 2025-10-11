using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile.Experience;
using FindACoach.Core.ServiceContracts.Forum.Activities;

namespace FindACoach.Core.Services.Forum.Activities
{
    public class PositionsAdderService : IPositionsAdderService
    {
        private readonly IPositionsRepository _positionsRepository;

        public PositionsAdderService(IPositionsRepository positionsRepository)
        {
            _positionsRepository = positionsRepository;
        }

        public async Task AddPosition(string userId, AddPositionDTO dto)
        {
            await _positionsRepository.AddPosition(userId, dto);
        }
    }
}
