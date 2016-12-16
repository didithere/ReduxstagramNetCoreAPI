using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReduxstagramAPI.Interfaces
{
    public interface BaseRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(string id);
        void Insert(T entity);
        Task<bool> DeleteById(string id);
        void UpdateById(string id, T entity);
    }
}
