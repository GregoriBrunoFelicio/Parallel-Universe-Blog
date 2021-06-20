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

        public AccountInputModel Account { get; set; }

        public bool Active { get; set; }
    }

    public class UserInfoViewModel
    {
        public UserInfoViewModel(int id, string name, string about)
        {
            Id = id;
            Name = name;
            About = about;
        }

        public int Id { get; }
        public string Name { get; }
        public string About { get; }
    }
}
