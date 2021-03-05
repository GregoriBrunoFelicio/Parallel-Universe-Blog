using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parallel.Universe.Blog.Api.Data.Repositories;
using Parallel.Universe.Blog.Api.ViewModels;
using System.Threading.Tasks;

namespace Parallel.Universe.Blog.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class PostController : ControllerBase
    {
        private readonly IPostRepository postRepository;

        public PostController(IPostRepository postRepository) => this.postRepository = postRepository;

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] PostViewModel model)
        {
            return Ok();
        }
    }
}
