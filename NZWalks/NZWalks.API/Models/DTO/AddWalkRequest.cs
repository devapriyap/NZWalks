namespace NZWalks.API.Models.DTO
{
    public class AddWalkRequest
    {
        public string Name { get; set; }
        public double  Length { get; set; }        
        public int RegionId { get; set; }
        public int WalkDifficultyId { get; set; }      

    }
}
