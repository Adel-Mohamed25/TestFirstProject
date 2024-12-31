using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project.BLL.Model
{
    public class ResetPasswordVM
    {
        [Required(ErrorMessage = "Password Is Required")]
        [MinLength(8, ErrorMessage = "MinLength 8 char")]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must contain at least one letter, one number, and one special character.")]
        [PasswordPropertyText()]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password Is Required")]
        [MinLength(8, ErrorMessage = "MinLength 8 char")]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must contain at least one letter, one number, and one special character.")]
        [Compare("Password", ErrorMessage = "Password Not Matching")]
        [PasswordPropertyText()]
        public required string ConfirmPassword { get; set; }

        public string? Email { get; set; }

        public string? Token { get; set; }
    }
}
