using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project.BLL.Model
{
    public class SignInVM
    {
        [Required(ErrorMessage = "Email Is Required")]
        [MinLength(20, ErrorMessage = "MinLength 20 char")]
        [EmailAddress(ErrorMessage = "Not Vaild Email")]
        //[CustomAutoComplete("on")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        [MinLength(8, ErrorMessage = "MinLength 8 char")]
        [PasswordPropertyText(true)]
        public required string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
