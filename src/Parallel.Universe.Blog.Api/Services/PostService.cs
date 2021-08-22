using AutoMapper;
using Parallel.Universe.Blog.Api.Data;
using Parallel.Universe.Blog.Api.Data.Repositories;
using Parallel.Universe.Blog.Api.Entities;
using Parallel.Universe.Blog.Api.Services.Results;
using Parallel.Universe.Blog.Api.ViewModels;
using System;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> Create(PostViewModel model)
        {
            try
            {
                var post = _mapper.Map<Post>(model);

                var user = await _userRepository.GetByIdAsync(post.UserId);

                if (user == null) return new Result("User not found.", false);

                if (!user.Active) return new Result("Inactive account.", false);

                await _postRepository.AddAsync(post);

                return await _unitOfWork.CommitAsync()
                    ? new Result("Post created successfully.", true)
                    : new Result("Error to create post.", false);
            }
            catch (Exception exception)
            {
                await _unitOfWork.RollBackAsync();
                return new Result(exception.Message, false);
            }
        }

        public async Task<IResult> Update(PostViewModel model)
        {
            try
            {
                var post = _mapper.Map<Post>(model);

                var user = await _userRepository.GetByIdAsync(model.UserId);
                var postFromDb = await _postRepository.GetByIdAsync(model.Id);

                if (user == null) return new Result("User not found.", false);

                if (!user.Active) return new Result("Inactive account.", false);

                if (model.UserId != postFromDb.UserId) return new Result("Post does not belong to user.", false);

                await _postRepository.UpdateAsync(post);

                return await _unitOfWork.CommitAsync()
                 ? new Result("Post updated successfully.", true)
                 : new Result("Error to update post.", false);
            }
            catch (Exception exception)
            {
                await _unitOfWork.RollBackAsync();
                return new Result(exception.Message, false);
            }
        }
    }
}
