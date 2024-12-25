using System.Linq.Expressions;

namespace Project.BLL.Repository
{
    public class DepartmentRepo : IServicesRepo<Department>
    {
        private readonly ApplicationDbContext db;
        public DepartmentRepo(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Department>> GetAsync(Expression<Func<Department, bool>>? filter = null)
        {
            if (filter is null)
                return await db.Departments.AsNoTracking()
                                           .ToListAsync();

            return await db.Departments.AsNoTracking()
                                     .Where(filter)
                                     .ToListAsync();
        }

        public async Task<Department> GetByIdAsync(Expression<Func<Department, bool>> filter)
        {
            var data = await db.Departments.AsNoTracking()
                                         .Where(filter)
                                         .FirstOrDefaultAsync();

            if (data == null)
                throw new ArgumentNullException("No Result");
            return data;
        }

        public async Task CreateAsync(Department department)
        {
            await db.Departments.AddAsync(department);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Department department)
        {
            db.Departments.Remove(department);
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Department department)
        {
            db.Entry(department).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

    }
}
