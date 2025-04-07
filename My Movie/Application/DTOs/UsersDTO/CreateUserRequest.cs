using System.ComponentModel.DataAnnotations;

namespace My_Movie.DTO
{
    public class CreateUserRequest
    {
        public string Name { get; set; }
        [Required(ErrorMessage = "LoginName is required")]
        [MaxLength(50, ErrorMessage = "LoginName cannot exceed 50 characters")]
        public string LoginName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MaxLength(100, ErrorMessage = "Password cannot exceed 100 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Password confirmation is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]

        public string RoleName { get; set; }
    }
}
