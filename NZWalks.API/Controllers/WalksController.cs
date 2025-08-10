using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        public IWalkRepository walkRepository { get; }


        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWalkAsync([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);
            await walkRepository.CreateWalkAsync(walkDomainModel);

            var walkDto = mapper.Map<WalkDto>(walkDomainModel);
            return Ok(walkDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            var walksDomainModel = await walkRepository.GetAllWalksAsync();

            var walkDto = mapper.Map<List<WalkDto>>(walksDomainModel);

            return Ok(walkDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWalkByIdAsync([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetWalkByIdAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            var walkDto = mapper.Map<WalkDto>(walkDomainModel);
            return Ok(walkDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);
            var updatedWalk = await walkRepository.UpdateWalkAsync(id, walkDomainModel);
            if (updatedWalk == null)
            {
                return NotFound();
            }
            var walkDto = mapper.Map<WalkDto>(updatedWalk);
            return Ok(walkDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteWalkAsync([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.DeleteWalkAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            var walkDto = mapper.Map<WalkDto>(walkDomainModel);
            return Ok(walkDto);
        }
    }
}
