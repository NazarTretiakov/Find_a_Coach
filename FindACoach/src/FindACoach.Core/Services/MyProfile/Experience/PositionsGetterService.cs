using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile.Experience;
using FindACoach.Core.ServiceContracts.MyProfile.Experience;

namespace FindACoach.Core.Services.MyProfile.Experience
{
    public class PositionsGetterService : IPositionsGetterService
    {
        private readonly IPositionsRepository _positionsRepository;

        public PositionsGetterService(IPositionsRepository positionsRepository)
        {
            _positionsRepository = positionsRepository;
        }
        public async Task<List<PositionToResponse>> GetAllPositions(string userId)
        {
            var positions = await _positionsRepository.GetAllPositions(userId);

            return positions;
        }

        public async Task<List<PositionToResponse>> GetLastTwoPositions(string userId)
        {
            var positions = await _positionsRepository.GetLastTwoPositions(userId);

            return positions;
        }

        public async Task<PositionToResponse> GetPosition(string positionId)
        {
            var position = await _positionsRepository.GetPosition(positionId);

            return position;
        }
    }
}
