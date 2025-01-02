using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Project.BLL.Repository
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UnitOfWork> logger;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            this.logger = logger;
            _dbSet = _context.Set<T>();
        }


        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? filter = null)
        {
            if (filter == null)
            {
                return await _dbSet.AsNoTracking().ToListAsync();
            }
            return await _dbSet.AsNoTracking().Where(filter).ToListAsync();
        }

        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> filter)
        {
            var result = await _dbSet.AsNoTracking().Where(filter).FirstOrDefaultAsync();

            if (result == null)
                throw new ArgumentNullException("No Result");
            return result;
        }

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
        }

    }
}
