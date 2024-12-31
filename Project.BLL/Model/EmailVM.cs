using System.ComponentModel.DataAnnotations;

namespace Project.BLL.Model
{
    public class EmailVM
    {
        [Required(ErrorMessage = "Email Is Required")]
        [MinLength(20, ErrorMessage = "MinLength 20 char")]
        [EmailAddress(ErrorMessage = "Not Vaild Email")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(50, ErrorMessage = "Max Length 50 char")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "Message is required")]
        public string?
            Message
        { get; set; }
    }
}
