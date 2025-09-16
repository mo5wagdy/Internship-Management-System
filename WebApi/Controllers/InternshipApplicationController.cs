using Application.DTOs.Applications;
using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternshipApplicationController : ControllerBase
    {
        private readonly IInternshipApplicationService _applicationServices;

        public InternshipApplicationController(IInternshipApplicationService applicationServices)
        {
            _applicationServices = applicationServices;
        }

        //Admin Can List Everything
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var applications = await _applicationServices.GetAllAsync();
            return Ok(applications);
        }

        //Student Applies for Internship
        [Authorize(Roles = "Student")]
        [HttpPost]
        public async Task<IActionResult> Apply([FromBody] CreateInternshipApplicationDto dto)
        {
            await _applicationServices.ApplyAsync(dto);
            return Created("", new { message = "Application Submitted Successfully" });
        }

        //Company Sees Applications for their Internship Postings
        [Authorize(Roles = "Company")]
        [HttpGet("internship/{internshipId}")]
        public async Task<IActionResult> GetByInternshipId([FromRoute] Guid internshipId)
        {
            var applications = await _applicationServices.GetByInternshipIdAsync(internshipId);
            return Ok(applications);
        }

        //Student Sees their Applications
        [Authorize(Roles = "Student")]
        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetByStudentId([FromRoute] Guid studentId)
        {
            var applications = await _applicationServices.GetByStudentIdAsync(studentId);
            return Ok(applications);
        }

        //Company Updates Application Status
        [Authorize(Roles = "Company")]
        [HttpPut("{applictionId}/status")]
        public async Task<IActionResult> UpdateStatus([FromRoute] Guid applictionId, [FromBody] UpdateInternshipApplicationStatusDto dto)
        {
            if (applictionId != dto.ApplicationId)
            {
                return BadRequest(new { message = "Application Id Mismatch" });
            }
            await _applicationServices.UpdateStatusAsync(dto);
            return Ok(new { message = "Application Status Updated Successfully" });
        }

    }
}
