using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalkDifficultyRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

       
        public async Task<IEnumerable<WalkDifficulty>> GetAllWalkDifficultiesAsync()
        {
            return await nZWalksDbContext.WalkDifficulty.ToListAsync();
           
        }

        public async Task<WalkDifficulty> GetWalkDifficultyByIdAsync(int id)
        {
            return await nZWalksDbContext.WalkDifficulty.FindAsync(id);
        }

        public async Task<WalkDifficulty> AddWalkDifficultyAsync(WalkDifficulty WalkDifficulty)
        {
            await nZWalksDbContext.AddAsync(WalkDifficulty);
            await nZWalksDbContext.SaveChangesAsync();
            return WalkDifficulty;
        }

        public async Task<WalkDifficulty> DeleteWalkDifficultyByIdAsync(int id)
        {
            var WalkDifficulty = await nZWalksDbContext.WalkDifficulty.FindAsync(id);

            if (WalkDifficulty == null)
            {
                return null;
            }

            // Delete the WalkDifficulty
            nZWalksDbContext.WalkDifficulty.Remove(WalkDifficulty);
            await nZWalksDbContext.SaveChangesAsync();
            return WalkDifficulty;
        }

        public async Task<WalkDifficulty> UpdateWalkDifficultyAsync(int id, WalkDifficulty WalkDifficulty)
        {
            var existingWalkDifficulty = await nZWalksDbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);

            if (existingWalkDifficulty == null)
            {
                return null;
            }

            // Move user inputs to existing WalkDifficulty record
            existingWalkDifficulty.Code = WalkDifficulty.Code;

            // Update the WalkDifficulty            
            await nZWalksDbContext.SaveChangesAsync();
            return existingWalkDifficulty;
        }
    }
}
