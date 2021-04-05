using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.Data
{
    public interface IRepository<T> where T : class
    {
        Task InsertAsync(T entity);
        void Delete(T entity);
        Task SaveAllAsync(bool acceptAllChangesOnSuccess = true);
        IQueryable<T> Table { get; }
        Task<IEnumerable<string>> ExecuteQuery(string command);
    }
}