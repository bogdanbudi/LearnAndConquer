using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Cart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IdentityController: ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

        //[Authorize("cartApi")]
        //[HttpGet]
        //public string Get2()
        //{
        //    var claims = User.Claims.ToList();
        //    return "secret message from ApiOne";
        //}
    }
}
