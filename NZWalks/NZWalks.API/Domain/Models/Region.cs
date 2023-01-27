namespace NZWalks.API.Domain.Models
{
    public class Region
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public double  Area { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public double Population { get; set; }

        // Navigation property
        public IEnumerable<Walk> Walks { get; set; }
    }
}
