using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkDifficultyRepository
    {
        Task <IEnumerable<WalkDifficulty>> GetAllWalkDifficultiesAsync();
        Task<WalkDifficulty> GetWalkDifficultyByIdAsync(int id);
        Task<WalkDifficulty> AddWalkDifficultyAsync(WalkDifficulty WalkDifficulty);
        Task<WalkDifficulty> DeleteWalkDifficultyByIdAsync(int id);
        Task<WalkDifficulty> UpdateWalkDifficultyAsync(int id, WalkDifficulty WalkDifficulty);
    }
}
