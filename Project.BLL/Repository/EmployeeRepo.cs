using System.Linq.Expressions;

namespace Project.BLL.Repository
{
    public class EmployeeRepo : IServicesRepo<Employee>
    {
        ApplicationDbContext db;

        public EmployeeRepo(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Employee>> GetAsync(Expression<Func<Employee, bool>>? filter = null)
        {
            if (filter is null)
                return await db.Employees.AsNoTracking()
                                         .Include(emp => emp.Department)
                                         .Include(emp => emp.District)
                                         .ToListAsync();

            return await db.Employees.AsNoTracking()
                                     .Where(filter)
                                     .Include(emp => emp.Department)
                                     .Include(emp => emp.District)
                                     .ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(Expression<Func<Employee, bool>> filter)
        {
            var data = await db.Employees.AsNoTracking()
                                         .Where(filter)
                                         .Include(emp => emp.Department)
                                         .Include(emp => emp.District)
                                         .FirstOrDefaultAsync();

            if (data == null)
                throw new ArgumentNullException("No Result");
            return data;
        }

        public async Task CreateAsync(Employee employee)
        {
            employee.CreationDate = DateTime.Now;
            await db.Employees.AddAsync(employee);
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            employee.IsUpdated = true;
            employee.UpdatedDate = DateTime.Now;
            db.Entry(employee).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(Employee employee)
        {
            var data = await db.Employees.FindAsync(employee.Employee_Id);
            if (data != null)
            {
                data.IsDeleted = true;
                data.IsActive = false;
                data.DeletedDate = DateTime.Now;
                await db.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentNullException("Employee Is Null");
            }
        }

        public async Task ReturnAsync(Employee employee)
        {
            var data = await db.Employees.FindAsync(employee.Employee_Id);
            if (data != null)
            {
                data.IsDeleted = false;
                data.IsActive = true;
                await db.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentNullException("Employee Not Found");
            }
        }

        public async Task DeleteAsync(Employee employee)
        {
            db.Employees.Remove(employee);
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> SearchAsync(Expression<Func<Employee, bool>> filter)
        {
            return await db.Employees.AsNoTracking()
                                     .Where(filter)
                                     .Include(emp => emp.Department)
                                     .Include(emp => emp.District)
                                     .ToListAsync();
        }
    }
}
