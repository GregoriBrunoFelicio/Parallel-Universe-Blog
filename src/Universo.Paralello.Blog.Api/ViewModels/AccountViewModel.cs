using System.ComponentModel.DataAnnotations;

namespace Parallel.Universe.Blog.Api.ViewModels
{
    public class AccountViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The E-mail cannot be null.")]
        [EmailAddress(ErrorMessage = "The E-mail is not valid.")]
        [StringLength(30, ErrorMessage = "The E-mail must be between {2} and {1} characters.", MinimumLength = 4)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "The Password cannot be null.")]
        [StringLength(40, ErrorMessage = "The Password must be between {2} and {1} characters.", MinimumLength = 5)]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password must be the same.")]
        public string PasswordConfirmation { get; set; }
    }
}
