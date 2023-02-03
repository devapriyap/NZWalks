using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task <IEnumerable<Region>> GetAllRegionsAsync();
        Task<Region> GetRegionByIdAsync(int id);
        Task<Region> AddRegionAsync(Region region);
        Task<Region> DeleteRegionByIdAsync(int id);
        Task<Region> UpdateRegionAsync(int id, Region region);
    }
}
