using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            var regions = await regionRepository.GetAllRegionsAsync();

            //// return DTO Regions

            //var regionsDTO = new List<Models.DTO.Region>(); 

            //regions.ToList().ForEach(region =>
            //{
            //    var regionDTO = new Models.DTO.Region()
            //    {
            //        Id = region.Id,
            //        Code = region.Code,
            //        Name = region.Name,
            //        Area = region.Area,
            //        Lat = region.Lat,
            //        Long = region.Long,
            //        Population = region.Population,
            //    };
            //    regionsDTO.Add(regionDTO);
            //}); 

            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);

            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:int}")]
        [ActionName("GetRegionByIdAsync")]
        public async Task<IActionResult> GetRegionByIdAsync(int id)
        {
            var region = await regionRepository.GetRegionByIdAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);

        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            // Request (DTO) to Domain Model
            var region = new Models.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population                    
            };

            // Pass details to repository
            // ... reused same var region above
            region = await regionRepository.AddRegionAsync(region);

            // Convert back to DTO
            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat =  region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population

            };

            return CreatedAtAction(nameof(GetRegionByIdAsync), new {id = regionDTO.Id }, regionDTO);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteRegionByIdAsync(int id)
        {
            // Get region from database
            var region = await regionRepository.DeleteRegionByIdAsync(id);

            // if null then return NotFound
            if (region == null)
            {
                return NotFound();
            }

            // Convert response back to DTO
            var regionDTO = mapper.Map<Models.DTO.Region>(region);

            // Return OK response
            return Ok(regionDTO);

        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] int id, [FromBody] Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            // Convert (DTO) to Domain Model
            var region = new Models.Domain.Region()
            {
                Code = updateRegionRequest.Code,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Name = updateRegionRequest.Name,
                Population = updateRegionRequest.Population
            };

            // Update region using repository
            // ... reused same var region above
            region = await regionRepository.UpdateRegionAsync(id, region);

            //If null then pass NotFound
            if (region == null)
            {
                return NotFound();
            }

            // Convert back to DTO
            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population

            };

            // Return Ok response
            return Ok(regionDTO);
        }

    }
}
