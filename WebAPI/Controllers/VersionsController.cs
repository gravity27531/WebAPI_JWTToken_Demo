using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionsController : ControllerBase
    {
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Fversion()
        {
            return Ok(DateTime.Now.ToString());
        }
    }
}
