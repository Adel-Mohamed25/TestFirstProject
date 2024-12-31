using System.ComponentModel.DataAnnotations;

namespace Project.BLL.Model
{
    public class SignUpVM
    {
        [Required(ErrorMessage = "UserName Is Required")]
        [MinLength(3, ErrorMessage = "MinLength 3 char")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "Email Is Required")]
        [MinLength(20, ErrorMessage = "MinLength 20 char")]
        [EmailAddress(ErrorMessage = "Not Vaild Email")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        [MinLength(8, ErrorMessage = "MinLength 8 char")]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must contain at least one letter, one number, and one special character.")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password Is Required")]
        [MinLength(8, ErrorMessage = "MinLength 8 char")]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must contain at least one letter, one number, and one special character.")]
        [Compare("Password", ErrorMessage = "Password Not Matching")]
        public required string ConfirmPassword { get; set; }

        public bool IAgree { get; set; }
    }
}
