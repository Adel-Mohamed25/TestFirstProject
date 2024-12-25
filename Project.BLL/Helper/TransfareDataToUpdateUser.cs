using Project.DAL.Extend;

namespace Project.BLL.Helper
{
    public static class TransfareDataToUpdateUser
    {
        public static void CopyProperties(this ApplicationUser source, ApplicationUser target)
        {
            foreach (var property in typeof(ApplicationUser).GetProperties())
            {
                if (property.CanWrite && property.Name != "Id")
                {
                    var newValue = property.GetValue(source);
                    property.SetValue(target, newValue);
                }
            }
        }
    }
}
