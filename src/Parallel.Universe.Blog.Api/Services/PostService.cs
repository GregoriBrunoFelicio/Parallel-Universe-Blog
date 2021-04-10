using AutoMapper;
using Parallel.Universe.Blog.Api.Data.Repositories;
using Parallel.Universe.Blog.Api.Entities;
using Parallel.Universe.Blog.Api.Services.Results;
using Parallel.Universe.Blog.Api.ViewModels;
using System.Threading.Tasks;

namespace Parallel.Universe.Blog.Api.Services
{
    public interface IPostService
    {
        Task<IResult> Create(PostViewModel model);
        Task<IResult> Update(PostViewModel model);
    }

    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper mapper;

        public PostService(IPostRepository postRepository, IUserRepository userRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<IResult> Create(PostViewModel model)
        {
            var post = mapper.Map<Post>(model);

            var user = await _userRepository.GetByIdAsync(post.UserId);

            if (user == null) return new Result("User not found.", false);

            if (!user.Active) return new Result("Inactive account.", false);

            await _postRepository.AddAsync(post);

            return new Result("Post created successfully.", true);
        }

        public async Task<IResult> Update(PostViewModel model)
        {
            var post = mapper.Map<Post>(model);

            var user = await _userRepository.GetByIdAsync(model.UserId);

            if (user == null) return new Result("User not found.", false);

            if (!user.Active) return new Result("Inactive account.", false);

            await _postRepository.UpdateAsync(post);

            return new Result("Post updated successfully.", true);
        }
    }
}
