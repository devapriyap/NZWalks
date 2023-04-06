namespace NZWalks.API.Models.DTO
{
    public class UpdateWalkRequest
    {
        public string Name { get; set; }
        public double  Length { get; set; }        
        public int RegionId { get; set; }
        public int WalkDifficultyId { get; set; }      

    }
}
