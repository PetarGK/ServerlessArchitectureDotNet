using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Minniowa.Users.Application;
using Minniowa.Users.Application.Users;
using Minniowa.Users.Application.Users.Requests;

namespace Minniowa.Users.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserAppService _userAppService;

        public UsersController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpGet("health")]
        public IActionResult HealthCheck()
        {
            return Ok(new { Message = "I am healthy" });
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserRequest request)
        {
            Result result = await _userAppService.CreateUser(request);

            if (result.Code == 400)
                return BadRequest(result.Data);

            if (result.Code == 404)
                return NotFound();

            return Ok(result.Data);
        }
    }
}