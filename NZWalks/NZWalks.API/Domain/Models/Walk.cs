namespace NZWalks.API.Domain.Models
{
    public class Walk
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double  Length { get; set; }        
        public int RegionId { get; set; }
        public int WalkDifficultyId { get; set; }

        // Navigation property
        public Region Region { get; set; }
        public WalkDifficulty WalkDifficulty { get; set; }

    }
}
