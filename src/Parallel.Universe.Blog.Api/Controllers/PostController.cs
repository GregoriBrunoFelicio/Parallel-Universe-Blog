using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parallel.Universe.Blog.Api.Services;
using Parallel.Universe.Blog.Api.ViewModels;
using System.Threading.Tasks;

namespace Parallel.Universe.Blog.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService) => _postService = postService;

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] PostViewModel model)
        {
            var result = await _postService.Create(model);
            return !result.Success
                ? (IActionResult)BadRequest(result.Message)
                : Ok(result.Message);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put([FromBody] PostViewModel model)
        {
            var result = await _postService.Update(model);
            return !result.Success
                ? (IActionResult)BadRequest(result.Message)
                : Ok(result.Message);
        }
    }
}
