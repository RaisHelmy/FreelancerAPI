using FreelancerAPI.Application.DTOs;
using FreelancerAPI.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FreelancersController : ControllerBase
    {
        private readonly IFreelancerService _freelancerService;

        public FreelancersController(IFreelancerService freelancerService)
        {
            _freelancerService = freelancerService;
        }

        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<FreelancerDto>>> GetAll()
        // {
        //     var freelancers = await _freelancerService.GetAllAsync();
        //     return Ok(freelancers);
        // }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FreelancerDto>>> GetAllPagedAsync([FromQuery] int pageNumber = 1,[FromQuery] int pageSize = 10)
        {
            var freelancers = await _freelancerService.GetAllPagedAsync(pageNumber, pageSize);
            return Ok(freelancers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FreelancerDto>> GetById(int id)
        {
            var freelancer = await _freelancerService.GetByIdAsync(id);
            if (freelancer == null) return NotFound();
            return Ok(freelancer);
        }

        [HttpPost]
        public async Task<ActionResult<FreelancerDto>> Create(CreateFreelancerDto dto)
        {
            var created = await _freelancerService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FreelancerDto>> Update(int id, UpdateFreelancerDto dto)
        {
            var updated = await _freelancerService.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<FreelancerDto>> Patch(int id, UpdateFreelancerDto dto)
        {
            // Same as PUT for this implementation
            return await Update(id, dto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _freelancerService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<FreelancerDto>>> Search([FromQuery] string term)
        {
            var results = await _freelancerService.SearchAsync(term);
            return Ok(results);
        }

        [HttpPost("{id}/archive")]
        public async Task<IActionResult> Archive(int id)
        {
            await _freelancerService.ArchiveAsync(id);
            return NoContent();
        }

        [HttpPost("{id}/unarchive")]
        public async Task<IActionResult> Unarchive(int id)
        {
            await _freelancerService.UnarchiveAsync(id);
            return NoContent();
        }
    }
}