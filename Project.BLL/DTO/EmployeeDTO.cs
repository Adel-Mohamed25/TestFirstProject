using Microsoft.AspNetCore.Http;

namespace Project.BLL.DTO
{
    public class EmployeeDTO
    {
        public string? Employee_Fname { get; set; }

        public string? Employee_Lname { get; set; }

        public string? Employee_Email { get; set; }

        public string? Employee_Phone { get; set; }

        public string? Address { get; set; }

        public double Employee_Salary { get; set; }

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
