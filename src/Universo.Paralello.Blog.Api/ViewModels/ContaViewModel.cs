using System.ComponentModel.DataAnnotations;

namespace Universo.Paralello.Blog.Api.ViewModels
{
    public class ContaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O Email deve ser informado.")]
        [EmailAddress(ErrorMessage = "O Email informado deve ser válido.")]
        [StringLength(30, ErrorMessage = "O Email deve ter entre {2} e {1} caracteres.", MinimumLength = 4)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "A Senha deve ser informada.")]
        [StringLength(40, ErrorMessage = "A Senha deve ter entre {2} e {1} caracteres.", MinimumLength = 5)]
        public string Senha { get; set; }


        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "A senha e a confirmação da senha devem ser iguais.")]
        public string SenhaConfirmacao { get; set; }
    }
}
