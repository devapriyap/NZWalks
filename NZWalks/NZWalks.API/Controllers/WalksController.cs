using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository WalkRepository;
        private readonly IMapper mapper;
        private readonly IRegionRepository regionRepository;
        private readonly IWalkDifficultyRepository walkDifficultyRepository;

        public WalksController(IWalkRepository WalkRepository, IMapper mapper, IRegionRepository regionRepository, IWalkDifficultyRepository walkDifficultyRepository)
        {
            this.WalkRepository = WalkRepository;
            this.mapper = mapper;
            this.regionRepository = regionRepository;
            this.walkDifficultyRepository = walkDifficultyRepository;
            
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

            // Conver domain object to DTO  
            var WalkDTO = mapper.Map<Models.DTO.Walk>(WalkDomain);

            // Return response
            return Ok(WalkDTO);

        }

        [HttpPost]
        public async Task<IActionResult> AddWalkAsync(Models.DTO.AddWalkRequest addWalkRequest)
        {
            // Validate the request
            if (!await ValidateAddWalkAsync(addWalkRequest))
            {
                return BadRequest(ModelState);
            }

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
            // Validate the request
            if (! await ValidateUpdateWalkAsync(updateWalkRequest))
            {
                return BadRequest(ModelState);
            }

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

        #region Private Methods

        private async Task<bool> ValidateAddWalkAsync(Models.DTO.AddWalkRequest addWalkRequest)
        {
            if (addWalkRequest == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest),
                    $"Add Walk Data is required.");
                return false;
            }            

            if (string.IsNullOrWhiteSpace(addWalkRequest.Name))
            {
                ModelState.AddModelError(nameof(addWalkRequest.Name),
                    $"{nameof(addWalkRequest.Name)} cannot be null or empty or white space.");
            }

            if (addWalkRequest.Length < 0)
            {
                ModelState.AddModelError(nameof(addWalkRequest.Length),
                    $"{nameof(addWalkRequest.Length)} cannot be less than zero.");
            }

            var region = await regionRepository.GetRegionByIdAsync(addWalkRequest.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.RegionId),
                    $"{nameof(addWalkRequest.RegionId)} is invalid.");
            }

            var walkDifficulty = await walkDifficultyRepository.GetWalkDifficultyByIdAsync(addWalkRequest.WalkDifficultyId);
            if (walkDifficulty == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.WalkDifficultyId),
                    $"{nameof(addWalkRequest.WalkDifficultyId)} is invalid.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> ValidateUpdateWalkAsync(Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            if (updateWalkRequest == null)
            {
                ModelState.AddModelError(nameof(UpdateWalkRequest),
                    $"Update Walk Data is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(updateWalkRequest.Name))
            {
                ModelState.AddModelError(nameof(UpdateWalkRequest.Name),
                    $"{nameof(UpdateWalkRequest.Name)} cannot be null or empty or white space.");
            }

            if (updateWalkRequest.Length < 0)
            {
                ModelState.AddModelError(nameof(UpdateWalkRequest.Length),
                    $"{nameof(UpdateWalkRequest.Length)} cannot be less than zero.");
            }

            var region = await regionRepository.GetRegionByIdAsync(updateWalkRequest.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(UpdateWalkRequest.RegionId),
                    $"{nameof(UpdateWalkRequest.RegionId)} is invalid.");
            }

            var walkDifficulty = await walkDifficultyRepository.GetWalkDifficultyByIdAsync(updateWalkRequest.WalkDifficultyId);
            if (walkDifficulty == null)
            {
                ModelState.AddModelError(nameof(UpdateWalkRequest.WalkDifficultyId),
                    $"{nameof(updateWalkRequest.WalkDifficultyId)} is invalid.");
            }


            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }
        #endregion

    }
}
