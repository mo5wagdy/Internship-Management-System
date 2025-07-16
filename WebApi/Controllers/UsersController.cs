using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userservice.GetByIdAsync(id);
            return Ok(user);
        }
    }
}
