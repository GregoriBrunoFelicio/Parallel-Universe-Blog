using System.ComponentModel.DataAnnotations;

namespace Parallel.Universe.Blog.Api.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "The E-mail is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Password is required.")]
        public string Senha { get; set; }
    }
}
