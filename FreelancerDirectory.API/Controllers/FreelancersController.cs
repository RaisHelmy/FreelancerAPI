using Microsoft.AspNetCore.Mvc;
using FreelancerDirectory.Application.DTOs;
using FreelancerDirectory.Application.Interfaces;

namespace FreelancerDirectory.API.Controllers
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

        /// <summary>
        /// Get all freelancers
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FreelancerDto>>> GetAll()
        {
            try
            {
                var freelancers = await _freelancerService.GetAllAsync();
                return Ok(freelancers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving freelancers", error = ex.Message });
            }
        }

        /// <summary>
        /// Get freelancer by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<FreelancerDto>> GetById(int id)
        {
            try
            {
                var freelancer = await _freelancerService.GetByIdAsync(id);
                if (freelancer == null)
                    return NotFound(new { message = $"Freelancer with ID {id} not found" });

                return Ok(freelancer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the freelancer", error = ex.Message });
            }
        }

        /// <summary>
        /// Create a new freelancer
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<FreelancerDto>> Create([FromBody] CreateFreelancerDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var freelancer = await _freelancerService.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = freelancer.Id }, freelancer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the freelancer", error = ex.Message });
            }
        }

        /// <summary>
        /// Update freelancer (full update)
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<FreelancerDto>> Update(int id, [FromBody] UpdateFreelancerDto updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var freelancer = await _freelancerService.UpdateAsync(id, updateDto);
                return Ok(freelancer);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the freelancer", error = ex.Message });
            }
        }

        /// <summary>
        /// Patch freelancer (partial update)
        /// </summary>
        [HttpPatch("{id}")]
        public async Task<ActionResult<FreelancerDto>> Patch(int id, [FromBody] UpdateFreelancerDto patchDto)
        {
            try
            {
                var freelancer = await _freelancerService.PatchAsync(id, patchDto);
                return Ok(freelancer);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the freelancer", error = ex.Message });
            }
        }

        /// <summary>
        /// Delete freelancer
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _freelancerService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the freelancer", error = ex.Message });
            }
        }

        /// <summary>
        /// Search freelancers by username or email
        /// </summary>
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<FreelancerDto>>> Search([FromQuery] string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                    return BadRequest(new { message = "Search term is required" });

                var freelancers = await _freelancerService.SearchAsync(searchTerm);
                return Ok(freelancers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while searching freelancers", error = ex.Message });
            }
        }

        /// <summary>
        /// Archive a freelancer
        /// </summary>
        [HttpPost("{id}/archive")]
        public async Task<ActionResult<FreelancerDto>> Archive(int id)
        {
            try
            {
                var freelancer = await _freelancerService.ArchiveAsync(id);
                return Ok(freelancer);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while archiving the freelancer", error = ex.Message });
            }
        }

        /// <summary>
        /// Unarchive a freelancer
        /// </summary>
        [HttpPost("{id}/unarchive")]
        public async Task<ActionResult<FreelancerDto>> Unarchive(int id)
        {
            try
            {
                var freelancer = await _freelancerService.UnarchiveAsync(id);
                return Ok(freelancer);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while unarchiving the freelancer", error = ex.Message });
            }
        }
    }
}