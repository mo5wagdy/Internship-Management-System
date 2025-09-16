using Application.DTOs.Users;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extentions;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userservice;
        public UsersController(IUserService userservice)
        {
            _userservice = userservice;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userservice.RegisterAsync(dto);
            return Created("", result);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var token = await _userservice.LoginAsync(dto);
                return Ok(new {token});
            }

            catch (Exception Ex)
            {
                return Unauthorized(new { message = Ex.Message });
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userservice.GetAllAsync();
            return Ok(users);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            if(!User.IsInRole("Admin") && userId.Value != id)
                return Forbid();

            try
            {

                var user = await _userservice.GetByIdAsync(id);
                return Ok(user);
            }

            catch (Exception Ex)
            {
                return NotFound(new { message = Ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            if(!User.IsInRole("Admin") && userId.Value != id)
                return Forbid();

            try
            {
                await _userservice.DeleteAsync(id);
                return Ok(new { message = "User Deleted Successfully"});
            }
            catch (Exception Ex)
            {
                return NotFound(new { message = Ex.Message });
            }
        }
    }
}
