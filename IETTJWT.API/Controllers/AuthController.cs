using IETTJWT.Core.DTOs;
using IETTJWT.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IETTJWT.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateToken(LoginDto login)
        {

            var result = await _authService.CreateTokenAsync(login);



            return new ObjectResult(result) { StatusCode = result.StatusCode };

        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto user)
        {
            var result = await _authService.CreateUser(user);

            return new ObjectResult(result) { StatusCode = result.StatusCode };
        }



    }
}
