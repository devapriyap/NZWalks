using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task <IEnumerable<Walk>> GetAllWalksAsync();
        Task<Walk> GetWalkByIdAsync(int id);
        Task<Walk> AddWalkAsync(Walk Walk);
        Task<Walk> DeleteWalkByIdAsync(int id);
        Task<Walk> UpdateWalkAsync(int id, Walk Walk);
    }
}
