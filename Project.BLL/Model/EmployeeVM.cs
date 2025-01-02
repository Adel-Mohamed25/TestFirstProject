using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Project.BLL.Model
{
    public class EmployeeVM
    {
        public int Employee_Id { get; set; }

        [Required(ErrorMessage = "First Name Is Required")]
        public string? Employee_Fname { get; set; }

        [Required(ErrorMessage = "Last Name Is Required")]
        public string? Employee_Lname { get; set; }

        [EmailAddress(ErrorMessage = "Email not valid")]
        public string? Employee_Email { get; set; }

        // 01143254939
        [RegularExpression("[0-9]{11}", ErrorMessage = "Enter Like This 01143254939")]
        [Required(ErrorMessage = "Phone Number Is Required")]
        public string? Employee_Phone { get; set; }

        // 176-Streetname-City-Country
        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z\s]{1,30}-[a-zA-Z\s]{1,20}-[a-zA-Z\s]{1,20}$", ErrorMessage = "Enter Like This: 176-Streetname-City-Country")]

        public string? Address { get; set; }

        [Required(ErrorMessage = "Salary Is Required")]
        public double Employee_Salary { get; set; }

        [Required(ErrorMessage = "HireDate Is Required")]
        public DateTime HireDate { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsUpdated { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime DeletedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string? ImageName { get; set; }

        public string? CVName { get; set; }

        public IFormFile? Image { get; set; }

        public IFormFile? CV { get; set; }

        public int? Department_Id { get; set; }

        public Department? Department { get; set; }

        public int? District_Id { get; set; }

        public District? District { get; set; }
    }
}
