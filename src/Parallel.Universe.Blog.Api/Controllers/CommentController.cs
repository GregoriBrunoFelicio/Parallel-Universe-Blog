using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Parallel.Universe.Blog.Api.Data;
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
        private readonly ICommentRepository commentRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public CommentController(ICommentRepository commentRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.commentRepository = commentRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostViewModel model)
        {
            var post = mapper.Map<Comment>(model);
            await commentRepository.AddAsync(post);
            return await unitOfWork.CommitAsync() ? Ok() : BadRequest("Error to create comment.");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await commentRepository.DeleteAsync(id);
            return await unitOfWork.CommitAsync() ? Ok() : BadRequest("Error to delete comment.");
        }

        [HttpGet("Post/{id:int}")]
        public async Task<IActionResult> Get(int id) => Ok(mapper.Map<IReadOnlyCollection<CommentViewModel>>(await commentRepository.GetAllByPostId(id)));
    }
}
