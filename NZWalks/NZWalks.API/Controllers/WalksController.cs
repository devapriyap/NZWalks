using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository WalkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository WalkRepository, IMapper mapper)
        {
            this.WalkRepository = WalkRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            // Fetch data from database - Domain walks
            var Walks = await WalkRepository.GetAllWalksAsync();

            // Convert domain walks to DTO walks
            var WalksDTO = mapper.Map<List<Models.DTO.Walk>>(Walks);

            // return response
            return Ok(WalksDTO);
        }

        [HttpGet]
        [Route("{id:int}")]
        [ActionName("GetWalkByIdAsync")]
        public async Task<IActionResult> GetWalkByIdAsync(int id)
        {
            // Get walk domain object from database
            var WalkDomain = await WalkRepository.GetWalkByIdAsync(id);

            if (WalkDomain == null)
            {
                return NotFound();
            }

            // Conver domain object to DTOI    
            var WalkDTO = mapper.Map<Models.DTO.Walk>(WalkDomain);

            // Return response
            return Ok(WalkDTO);

        }

        [HttpPost]
        public async Task<IActionResult> AddWalkAsync(Models.DTO.AddWalkRequest addWalkRequest)
        {
            // Request (DTO) to Domain Model
            var walkDomain = new Models.Domain.Walk()
            {
                Name = addWalkRequest.Name,
                Length = addWalkRequest.Length,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId      
            };

            // Pass details to repository
            walkDomain = await WalkRepository.AddWalkAsync(walkDomain);

            // Convert back to DTO
            var WalkDTO = new Models.DTO.Walk
            {
                Id = walkDomain.Id,
                Name = walkDomain.Name,
                Length = walkDomain.Length,
                RegionId = walkDomain.RegionId,
                WalkDifficultyId = walkDomain.WalkDifficultyId 
            };

            return CreatedAtAction(nameof(GetWalkByIdAsync), new {id = WalkDTO.Id }, WalkDTO);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteWalkByIdAsync(int id)
        {
            // Call repository to delete Walk - Get Walk from database
            var walkDomain = await WalkRepository.DeleteWalkByIdAsync(id);

            // if null then return NotFound
            if (walkDomain == null)
            {
                return NotFound();
            }

            // Convert response back to DTO
            var walkDTO = mapper.Map<Models.DTO.Walk>(walkDomain);

            // Return OK response
            return Ok(walkDTO);

        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] int id, [FromBody] Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            // Convert (DTO) to Domain Model
            var walkDomain = new Models.Domain.Walk()
            {
                Name = updateWalkRequest.Name,
                Length = updateWalkRequest.Length,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId
            };

            // Pass details to repository
            // ... reused same var Walk above
            walkDomain = await WalkRepository.UpdateWalkAsync(id, walkDomain);

            //If null then pass NotFound
            if (walkDomain == null)
            {
                return NotFound();
            }

            // Convert back domain to DTO
            var WalkDTO = new Models.DTO.Walk
            {
                //Id = Walk.Id,
                Name = walkDomain.Name,
                Length = walkDomain.Length,
                RegionId = walkDomain.RegionId,
                WalkDifficultyId = walkDomain.WalkDifficultyId

            };

            // Return Ok response
            return Ok(WalkDTO);
        }

    }
}
