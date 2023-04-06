using AutoMapper;

namespace NZWalks.API.Profiles
{
    public class WalksProfile : Profile
    {
        public WalksProfile()
        {
            CreateMap<Models.Domain.Walk, Models.DTO.Walk>()
            //**** If the column name mismatch.....
            //.ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id));
            //**** Or you can map DTO to Domain model by using reverse map
            .ReverseMap();

            CreateMap<Models.Domain.WalkDifficulty, Models.DTO.WalkDifficulty>()
                .ReverseMap();
        }
        
    }
}
