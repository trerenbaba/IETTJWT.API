using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IETTJWT.API.Controllers
{


    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("data başarılı");
        }

        [HttpGet]
        [Authorize(Policy = "CityIstanbul")]
        public IActionResult Get2()
        {
            return Ok("başarılı");
        }
        [HttpGet]
        [Authorize(Policy = "Exchange")]
        public IActionResult Get3()
        {
            return Ok("başarılı");
        }



    }
}
