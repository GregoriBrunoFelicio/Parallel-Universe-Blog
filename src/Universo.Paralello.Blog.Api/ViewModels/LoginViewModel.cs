using System.ComponentModel.DataAnnotations;

namespace Universo.Paralello.Blog.Api.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O E-mail deve ser informado.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A Senha deve ser informada.")]
        public string Senha { get; set; }
    }
}
