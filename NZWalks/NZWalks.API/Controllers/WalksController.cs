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
            var Walk = await WalkRepository.GetWalkByIdAsync(id);

            if (Walk == null)
            {
                return NotFound();
            }

            var WalkDTO = mapper.Map<Models.DTO.Walk>(Walk);
            return Ok(WalkDTO);

        }

        [HttpPost]
        public async Task<IActionResult> AddWalkAsync(Models.DTO.AddWalkRequest addWalkRequest)
        {
            // Request (DTO) to Domain Model
            var Walk = new Models.Domain.Walk()
            {
                Name = addWalkRequest.Name,
                Length = addWalkRequest.Length,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId      
            };

            // Pass details to repository
            Walk = await WalkRepository.AddWalkAsync(Walk);

            // Convert back to DTO
            var WalkDTO = new Models.DTO.Walk
            {
                Id = Walk.Id,
                Name = Walk.Name,
                Length = Walk.Length,
                RegionId =  Walk.RegionId,
                WalkDifficultyId = Walk.WalkDifficultyId            };

            return CreatedAtAction(nameof(GetWalkByIdAsync), new {id = WalkDTO.Id }, WalkDTO);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteWalkByIdAsync(int id)
        {
            // Get Walk from database
            var Walk = await WalkRepository.DeleteWalkByIdAsync(id);

            // if null then return NotFound
            if (Walk == null)
            {
                return NotFound();
            }

            // Convert response back to DTO
            var WalkDTO = mapper.Map<Models.DTO.Walk>(Walk);

            // Return OK response
            return Ok(WalkDTO);

        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] int id, [FromBody] Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            // Convert (DTO) to Domain Model
            var Walk = new Models.Domain.Walk()
            {
                Name = updateWalkRequest.Name,
                Length = updateWalkRequest.Length,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId
            };

            // Update Walk using repository
            // ... reused same var Walk above
            Walk = await WalkRepository.UpdateWalkAsync(id, Walk);

            //If null then pass NotFound
            if (Walk == null)
            {
                return NotFound();
            }

            // Convert back to DTO
            var WalkDTO = new Models.DTO.Walk
            {
                //Id = Walk.Id,
                Name = Walk.Name,
                Length = Walk.Length,
                RegionId = Walk.RegionId,
                WalkDifficultyId = Walk.WalkDifficultyId

            };

            // Return Ok response
            return Ok(WalkDTO);
        }

    }
}
