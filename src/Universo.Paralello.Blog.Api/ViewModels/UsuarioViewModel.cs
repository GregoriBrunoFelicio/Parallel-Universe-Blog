using System.ComponentModel.DataAnnotations;

namespace Universo.Paralello.Blog.Api.ViewModels
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O Nome deve ser informado.")]
        [StringLength(50, ErrorMessage = "O Nome deve ter entre {2} e {1} caracteres.", MinimumLength = 5)]
        public string Nome { get; set; }

        [StringLength(50, ErrorMessage = "O Nome deve ter entre {2} e {1} caracteres.", MinimumLength = 5)]
        public string Sobre { get; set; }

        public ContaViewModel Conta { get; set; }
    }
}
