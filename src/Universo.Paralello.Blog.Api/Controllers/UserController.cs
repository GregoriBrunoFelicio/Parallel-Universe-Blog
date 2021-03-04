using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parallel.Universe.Blog.Api.Data.Repositories;
using Parallel.Universe.Blog.Api.Entities;
using Parallel.Universe.Blog.Api.ViewModels;
using System.Threading.Tasks;

namespace Parallel.Universe.Blog.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] UserViewModel model) => Ok(await _usuarioRepository.UpdateAsync(_mapper.Map<UserViewModel, User>(model)));
    }
}
