namespace Project.BLL.Model
{
    public class UserInRoleVM
    {
        public required string Id { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public bool IsSelected { get; set; }
    }
}
