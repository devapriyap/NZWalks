using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalkRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

       
        public async Task<IEnumerable<Walk>> GetAllWalksAsync()
        {
            //return await nZWalksDbContext.Walks.ToListAsync();
            return await nZWalksDbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .ToListAsync();

        }

        public async Task<Walk> GetWalkByIdAsync(int id)
        {
            return await nZWalksDbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk> AddWalkAsync(Walk Walk)
        {
            await nZWalksDbContext.AddAsync(Walk);
            await nZWalksDbContext.SaveChangesAsync();
            return Walk;
        }

        public async Task<Walk> DeleteWalkByIdAsync(int id)
        {
            var existingWalk = await nZWalksDbContext.Walks.FindAsync(id);

            if (existingWalk == null)
            {
                return null;
            }

            // Delete the Walk
            nZWalksDbContext.Walks.Remove(existingWalk);
            await nZWalksDbContext.SaveChangesAsync();
            return existingWalk;
        }

        public async Task<Walk> UpdateWalkAsync(int id, Walk Walk)
        {
            var existingWalk = await nZWalksDbContext.Walks.FindAsync(id);

            if (existingWalk == null)
            {
                return null;
            }

            // Move user inputs to existing Walk record
            //existingWalk.Id = Walk.Id;
            existingWalk.Name = Walk.Name;
            existingWalk.Length= Walk.Length;
            existingWalk.RegionId = Walk.RegionId;
            existingWalk.WalkDifficultyId = Walk.WalkDifficultyId;

            // Update the Walk            
            await nZWalksDbContext.SaveChangesAsync();
            return existingWalk;
        }
    }
}
