using AutoMapper;

namespace NZWalks.API.Profiles
{
    public class RegionsProfile : Profile
    {
        public RegionsProfile()
        {
            CreateMap<Models.Domain.Region, Models.DTO.Region>();
            //**** If the column name mismatch.....
            //.ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id));
            //**** Or you can map DTO to Domain model by using reverse map
            // .ReverseMap()
        }
        
    }
}
