using Microsoft.AspNetCore.Mvc;
using Parallel.Universe.Blog.Api.Services;
using Parallel.Universe.Blog.Api.ViewModels;
using System.Threading.Tasks;

namespace Parallel.Universe.Blog.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService) => _accountService = accountService;


        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] UserViewModel model)
        {
            var result = await _accountService.Create(model);
            return !result.Success
                ? (IActionResult)BadRequest(result.Message)
                : Ok(result.Message);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var result = await _accountService.Verify(model);
            return !result.Success
                ? (IActionResult)BadRequest(result.Message)
                : Ok(result);
        }
    }
}
