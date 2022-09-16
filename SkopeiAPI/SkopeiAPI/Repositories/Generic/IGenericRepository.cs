using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkopeiAPI.Repositories.Generic
{
    public interface IGenericRepository<T> where T : class
    {
        Task<bool> Add(T entity);
        Task<IEnumerable<T>> All();
        Task<T> GetById(int id);
        Task<bool> Delete(int id);
    }
}
