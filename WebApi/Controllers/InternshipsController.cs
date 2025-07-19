using Application.DTOs.Internships;
using Application.DTOs.Users;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InternshipsController : ControllerBase
    {
        private readonly IInternshipService _internshipService;

        public InternshipsController(IInternshipService internshipService)
        {
            _internshipService = internshipService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var internships = await _internshipService.GetAllAsync();
            return Ok(internships);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var internship = await _internshipService.GetByIdAsync(id);
                return Ok(internship);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Company")]
        [HttpGet("company/{companyId}")]
        public async Task<IActionResult> GetByCompanyId(Guid companyId)
        {
            var internships = await _internshipService.GetByCompanyIdAsync(companyId);
            return Ok(internships);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchByTitle([FromQuery] string title)
        {
            var internships = await _internshipService.SearchByTitleAsync(title);
            return Ok(internships);
        }

        [Authorize(Roles = "Company")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateInternshipDto dto)
        {
            try
            {
                await _internshipService.CreateAsync(dto);
                return Ok(new { message = "Internship Created Successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Company")]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateInternshipDto dto)
        {
            await _internshipService.UpdateAsync(dto);
            return Ok(new { message = "Internship updated successfully" });
        }

        [Authorize(Roles = "Company")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _internshipService.DeleteAsync(id);
            return Ok(new { message = "Internship deleted successfully" });
        }
    }
}