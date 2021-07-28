using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parallel.Universe.Blog.Api.Data.Repositories;
using Parallel.Universe.Blog.Api.Services;
using Parallel.Universe.Blog.Api.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parallel.Universe.Blog.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostController(IPostService postService, IPostRepository postRepository, IMapper mapper)
        {
            _postService = postService;
            _postRepository = postRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] PostViewModel model)
        {
            var result = await _postService.Create(model);
            return !result.Success
                ? (IActionResult)BadRequest(result)
                : Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put([FromBody] PostViewModel model)
        {
            var result = await _postService.Update(model);
            return !result.Success
                ? (IActionResult)BadRequest(result)
                : Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id) => Ok(_mapper.Map<PostViewModel>(await _postRepository.GetByIdAsync(id)));

        [HttpGet("AllActive")]
        public async Task<IActionResult> GetAllActive() => Ok(_mapper.Map<IEnumerable<PostViewModel>>(await _postRepository.GetAllActivePostsAsync()));
    }
}
