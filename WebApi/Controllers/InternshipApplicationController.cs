using Application.DTOs.Applications;
using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extentions;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternshipApplicationController : ControllerBase
    {
        private readonly IInternshipApplicationService _applicationServices;
        private readonly IUnitOfWork _unitOfWork;

        public InternshipApplicationController(IInternshipApplicationService applicationServices, IUnitOfWork unitOfWork)
        {
            _applicationServices = applicationServices;
            _unitOfWork = unitOfWork;
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
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            if (dto.StudentId != userId.Value) return Forbid(); // Student May Only Apply Using Their Own ID

            //Ensure Internship Exists
            var internship = await _unitOfWork.Internships.GetByIdAsync(dto.InternshipId);
            if (internship == null) return NotFound(new { message = "Internship Not Found" });

            await _applicationServices.ApplyAsync(dto);
            return Created("", new { message = "Application Submitted Successfully" });
        }

        //Company Sees Applications for their Internship Postings
        [Authorize(Roles = "Company")]
        [HttpGet("internship/{internshipId}")]
        public async Task<IActionResult> GetByInternshipId([FromRoute] Guid internshipId)
        {
            var internship = await _unitOfWork.Internships.GetByIdAsync(internshipId);
            if (internship == null) return NotFound(new { message = "Internship Not Found" });

            var userId = User.GetUserId();
            if(userId == null) return Unauthorized();

            if (!User.IsInRole("Admin") && internship.CompanyId != userId.Value) return Forbid();

            var applications = await _applicationServices.GetByInternshipIdAsync(internshipId);
            return Ok(applications);
        }

        //Student Sees their Applications
        [Authorize(Roles = "Student")]
        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetByStudentId([FromRoute] Guid studentId)
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            if (!User.IsInRole("Admin") && userId.Value != studentId) return Forbid();

            var applications = await _applicationServices.GetByStudentIdAsync(studentId);
            return Ok(applications);
        }

        //Company Updates Application Status
        [Authorize(Roles = "Company")]
        [HttpPut("{applictionId}/status")]
        public async Task<IActionResult> UpdateStatus([FromRoute] Guid applictionId, [FromBody] UpdateInternshipApplicationStatusDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if (applictionId != dto.ApplicationId)
            {
                return BadRequest(new { message = "Application Id Mismatch" });
            }

            var application = await _unitOfWork.InternshipApplications.GetByIdAsync(dto.ApplicationId);
            if (application == null) return NotFound(new {message = "Application Not Found"});

            var internship = await _unitOfWork.Internships.GetByIdAsync(application.InternshipId);
            if (internship == null) return NotFound(new { message = "Internhip Not Found" });

            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            if (!User.IsInRole("Admin") && internship.CompanyId != userId.Value) return Forbid(); 

            await _applicationServices.UpdateStatusAsync(dto);
            return Ok(new { message = "Application Status Updated Successfully" });
        }

    }
}
