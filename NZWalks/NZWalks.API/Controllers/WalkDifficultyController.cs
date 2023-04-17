using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkDifficultyController : ControllerBase
    {
        private readonly IWalkDifficultyRepository WalkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultyController(IWalkDifficultyRepository WalkDifficultyRepository, IMapper mapper)
        {
            this.WalkDifficultyRepository = WalkDifficultyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficultyAsync()
        {
            // Fetch data from database - Domain WalkDifficulty
            var WalkDifficulty = await WalkDifficultyRepository.GetAllWalkDifficultiesAsync();

            // Convert domain WalkDifficulty to DTO WalkDifficulty
            var WalkDifficultyDTO = mapper.Map<List<Models.DTO.WalkDifficulty>>(WalkDifficulty);

            // return response
            return Ok(WalkDifficultyDTO);
        }

        [HttpGet]
        [Route("{id:int}")]
        [ActionName("GetWalkDifficultyByIdAsync")]
        public async Task<IActionResult> GetWalkDifficultyByIdAsync(int id)
        {
            // Get walkDifficulty domain object from database
            var WalkDifficultyDomain = await WalkDifficultyRepository.GetWalkDifficultyByIdAsync(id);

            if (WalkDifficultyDomain == null)
            {
                return NotFound();
            }

            // Conver domain object to DTOI    
            var WalkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(WalkDifficultyDomain);

            // Return response
            return Ok(WalkDifficultyDTO);

        }

        [HttpPost]
        public async Task<IActionResult> AddWalkDifficultyAsync(Models.DTO.AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            // Validate the request
            if (!ValidateAddWalkDifficultyAsync(addWalkDifficultyRequest))
            {
                return BadRequest(ModelState);
            }

            // Request (DTO) to Domain Model
            var walkDifficultyDomain = new Models.Domain.WalkDifficulty()
            {
                Code = addWalkDifficultyRequest.Code     
            };

            // Pass details to repository
            walkDifficultyDomain = await WalkDifficultyRepository.AddWalkDifficultyAsync(walkDifficultyDomain);

            // Convert back to DTO
            var WalkDifficultyDTO = new Models.DTO.WalkDifficulty
            {
                Id = walkDifficultyDomain.Id,
                Code = walkDifficultyDomain.Code
            };

            return CreatedAtAction(nameof(GetWalkDifficultyByIdAsync), new {id = WalkDifficultyDTO.Id }, WalkDifficultyDTO);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteWalkDifficultyByIdAsync(int id)
        {
            // Call repository to delete Walk - Get Walk from database
            var walkDifficultyDomain = await WalkDifficultyRepository.DeleteWalkDifficultyByIdAsync(id);

            // if null then return NotFound
            if (walkDifficultyDomain == null)
            {
                return NotFound();
            }

            // Convert response back to DTO
            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);

            // Return OK response
            return Ok(walkDifficultyDTO);

        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync([FromRoute] int id, [FromBody] Models.DTO.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            // Validate the request
            if (!ValidateUpdateWalkDifficultyAsync(updateWalkDifficultyRequest))
            {
                return BadRequest(ModelState);
            }

            // Convert (DTO) to Domain Model
            var walkDifficultyDomain = new Models.Domain.WalkDifficulty()
            {
                Code = updateWalkDifficultyRequest.Code
            };

            // Pass details to repository

            walkDifficultyDomain = await WalkDifficultyRepository.UpdateWalkDifficultyAsync(id, walkDifficultyDomain);

            //If null then pass NotFound
            if (walkDifficultyDomain == null)
            {
                return NotFound();
            }

            // Convert back domain to DTO
            var WalkDifficultyDTO = new Models.DTO.WalkDifficulty
            {
                Code = walkDifficultyDomain.Code

            };

            // Return Ok response
            return Ok(WalkDifficultyDTO);
        }

        #region Private Methods

        private bool ValidateAddWalkDifficultyAsync(Models.DTO.AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            if (addWalkDifficultyRequest == null)
            {
                ModelState.AddModelError(nameof(addWalkDifficultyRequest),
                    $"Add WalkDifficulty Data is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(addWalkDifficultyRequest.Code))
            {
                ModelState.AddModelError(nameof(addWalkDifficultyRequest.Code),
                    $"{nameof(addWalkDifficultyRequest.Code)} cannot be null or empty or white space.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private bool ValidateUpdateWalkDifficultyAsync(Models.DTO.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            if (updateWalkDifficultyRequest == null)
            {
                ModelState.AddModelError(nameof(updateWalkDifficultyRequest),
                    $"Add WalkDifficulty Data is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(updateWalkDifficultyRequest.Code))
            {
                ModelState.AddModelError(nameof(updateWalkDifficultyRequest.Code),
                    $"{nameof(updateWalkDifficultyRequest.Code)} cannot be null or empty or white space.");
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
