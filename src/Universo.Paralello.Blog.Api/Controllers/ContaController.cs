using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Universo.Paralello.Blog.Api.Services;
using Universo.Paralello.Blog.Api.ViewModels;

namespace Universo.Paralello.Blog.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaController : ControllerBase
    {
        private readonly IContaService _contaService;

        public ContaController(IContaService contaService)
        {
            _contaService = contaService;
        }

        [HttpPost("Criar")]
        public async Task<IActionResult> Criar([FromBody] CriacaoDeUsuarioViewModel model)
        {
            var resultado = await _contaService.Criar(model);
            return !resultado.Sucesso
                ? (IActionResult) BadRequest(resultado.Mensagem) 
                : Ok(resultado.Mensagem);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var resultado = await _contaService.Autenticar(model);
            return !resultado.Sucesso 
                ? (IActionResult) BadRequest(resultado.Mensagem)
                : Ok(resultado);
        }
    }
}
