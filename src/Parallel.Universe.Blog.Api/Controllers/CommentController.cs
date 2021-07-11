using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Parallel.Universe.Blog.Api.Data.Repositories;
using Parallel.Universe.Blog.Api.Entities;
using Parallel.Universe.Blog.Api.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parallel.Universe.Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentController(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostViewModel model)
        {
            var post = _mapper.Map<Comment>(model);
            await _commentRepository.AddAsync(post);
            return Ok();
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id) =>
        //    (await _commentRepository.DeleteAsync(id)) ? (IActionResult)Ok() : BadRequest();

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) => Ok(_mapper.Map<IReadOnlyCollection<CommentViewModel>>(await _commentRepository.GetAllByPostId(id)));
    }
}
