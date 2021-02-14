using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Universo.Paralello.Blog.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Post()
        {
            return Ok();
        }
    }
}
