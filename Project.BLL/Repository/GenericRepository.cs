using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Project.BLL.Repository
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UnitOfWork> _logger;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
            _dbSet = _context.Set<T>();
        }


        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? filter = null)
        {
            try
            {
                if (filter == null)
                {
                    return await _dbSet.AsNoTracking().ToListAsync();
                }
                return await _dbSet.AsNoTracking().Where(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error : Data Not Found.");
                throw;
            }
        }

        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> filter)
        {
            try
            {
                var result = await _dbSet.AsNoTracking().Where(filter).FirstOrDefaultAsync();

                if (result == null)
                    throw new ArgumentNullException("No Result");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error : Object Not Found.");
                throw;
            }
        }

        public async Task CreateAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error : Object Not Created.");
                throw;
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                _dbSet.Update(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error : Object Not Updated.");
                throw;
            }
        }

        public async Task DeleteAsync(T entity)
        {
            try
            {
                _dbSet.Remove(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error : Object Not Deleted.");
                throw;
            }
        }

    }
}
