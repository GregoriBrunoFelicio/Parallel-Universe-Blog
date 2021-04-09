using System.ComponentModel.DataAnnotations;

namespace Parallel.Universe.Blog.Api.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name is required.")]
        [StringLength(50, ErrorMessage = "The Name must be between {2} and {1} characters.", MinimumLength = 5)]
        public string Name { get; set; }

        [StringLength(50, ErrorMessage = "About must be between {2} and {1} characters.", MinimumLength = 5)]
        public string About { get; set; }

        public AccountViewModel Account { get; set; }

        public bool Active { get; set; }
    }
}
