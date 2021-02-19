using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Universo.Paralello.Blog.Api.Data.Repositories;
using Universo.Paralello.Blog.Api.Entities;
using Universo.Paralello.Blog.Api.ViewModels;

namespace Universo.Paralello.Blog.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody]UsuarioViewModel model) => Ok(await _usuarioRepository.UpdateAsync( _mapper.Map<UsuarioViewModel, Usuario>(model)));
    }
}
