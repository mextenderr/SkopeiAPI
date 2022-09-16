using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkopeiAPI.DbService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkopeiAPI.Repositories.Generic
{
    // A generic repository is used which implements shared functionality of DbSets.
    // The virtual methods are implemented in the Model specific repositories.
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly SkopeiDbContext _context;
        internal readonly DbSet<T> _dbSet;
        protected readonly ILogger _logger;

        public GenericRepository(SkopeiDbContext context, ILogger logger)
        {
            _context = context;
            _dbSet = _context.Set<T>(); // The dbSet is set to the inserted model types db table
            _logger = logger;
        }

        public async Task<bool> Add(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "{GenericRepository} Error on 'Add' method", typeof(T));
                return false;
            }
        }

        public async Task<IEnumerable<T>> All()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "{GenericRepository} Error on 'All' method", typeof(T));
                return new List<T>();
            }
        }

        public async Task<T> GetById(int id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "{GenericRepository} Error on 'GetById' method", typeof(T));
                return null;
            }
        }

        public virtual Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}