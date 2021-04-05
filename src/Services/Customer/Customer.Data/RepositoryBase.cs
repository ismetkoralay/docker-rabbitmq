using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Customer.Data
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public RepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task InsertAsync(T entity)
        {
            EntityEntry<T> entityEntry = await _dbSet.AddAsync(entity);
        }

        public void Delete(T entity) => this._dbSet.Remove(entity);

        public async Task SaveAllAsync(bool acceptAllChangesOnSuccess = true)
        {
            int num = await _dbContext.SaveChangesAsync(acceptAllChangesOnSuccess);
        }

        public IQueryable<T> Table => this._dbContext.Set<T>();

        public async Task<IEnumerable<string>> ExecuteQuery(string command) => (IEnumerable<string>) new List<string>()
        {
            (await this._dbContext.Database.ExecuteSqlRawAsync(command, new CancellationToken())).ToString()
        };
    }
}