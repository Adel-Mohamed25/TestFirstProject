using System.Text.Json.Serialization;

namespace Project.BLL.DTO
{
    public class DepartmentDTO
    {
        public string Department_Name { get; set; }

        public string Department_Code { get; set; }

        [JsonIgnore]
        public List<Employee> Employees { get; set; } = new();
    }
}
