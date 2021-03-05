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
    }

    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper mapper;

        public PostService(IPostRepository postRepository, IMapper mapper, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<IResult> Create(PostViewModel model)
        {
            var post = mapper.Map<Post>(model);

            var user = await _userRepository.GetByIdAsync(post.UserId);

            if (user == null) return new Result("Use not found.", false);

            if (!user.Active) return new Result("Inactive account.", false);

            await _postRepository.AddAsync(post);

            return new Result("Post created successfully", true);
        }
    }
}
