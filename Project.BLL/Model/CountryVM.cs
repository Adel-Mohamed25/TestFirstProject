
namespace Project.BLL.Model
{
    public class CountryVM
    {
        public int Country_Id { get; set; }

        public string? Country_Name { get; set; }

        public List<City>? Cities { get; set; }
    }
}
