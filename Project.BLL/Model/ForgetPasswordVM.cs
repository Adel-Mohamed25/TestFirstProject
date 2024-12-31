using System.ComponentModel.DataAnnotations;

namespace Project.BLL.Model
{
    public class ForgetPasswordVM
    {
        [Required(ErrorMessage = "Email Is Required")]
        [MinLength(20, ErrorMessage = "MinLength 20 char")]
        [EmailAddress(ErrorMessage = "Not Vaild Email")]
        public required string Email { get; set; }

    }
}
